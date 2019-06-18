using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

// Define the SFX types that can be referred to globally
//
public enum EGameSFX
{
	_SFX1,
}

public enum EGameMusic
{
	_MAIN_MENU,
	_INGAME,
}


public class GameAudioManager : MonoBehaviour
{
	// These are the AudioMixerGroups defined in the editor. One for each class of audio
	[Header("Mixer Groups")]
	[SerializeField] private AudioMixerGroup m_SFX = null;
	[SerializeField] private AudioMixerGroup m_Music = null;
	[SerializeField] private AudioMixer m_Master = null;

	//[Header("UI Audio")]
	//[SerializeField] private AudioClip m_acUI_ButtonSelect = null;
	//[SerializeField] private AudioClip m_acUI_ButtonClicked = null;

	// Audio SFX are attached to pooled GameObjects, held in this array. 
	private GameObject[] m_aSFXAudioPool;

	// Music is attached to a GameObject, stored here. 
	private GameObject m_goMusic = null;
	private AudioSource m_gcActiveMusicAudioSource = null;

	// Index of the next GameObject to grab from the pool. We're doing a
	// very simple circular buffer, it doesn't matter if the oldest 
	// audio effect clips out early. Will sound retro :D
	private int m_iSFXAudioPoolIndex = 0;

	// We want to limit instances of the same audio effects, so 
	// store in this list, each frame, every SFX ID that we've triggered.
	// If it's in the list, it ain't getting played again this frame...
	private List<EGameSFX> m_aEffectList = new List<EGameSFX>();
	
	// Co_Routine has no knowlegde of game state, so we need a 
	// flag we can set/clear in order for the music fade 
	// to cancel itself if th player short-circuits out of the
	// game over screen quickly...
	private bool m_bCanFadeMusic = false;


	public void Awake()
	{
		// Create the Audio pool for SFX and set defaults. 
		{
			m_aSFXAudioPool = new GameObject[Types.s_iPoolSize_AudioSFX];
			for (int i = 0; i < Types.s_iPoolSize_AudioSFX; ++i)
			{
				m_aSFXAudioPool[i] = new GameObject();
				m_aSFXAudioPool[i].name = "SFX_Pool_" + i.ToString();
				AudioSource gc = m_aSFXAudioPool[i].AddComponent<AudioSource>();
				if (null != gc)
				{
					gc.outputAudioMixerGroup = m_SFX;
					gc.maxDistance = 1.5f;                        // Small amount of attenuation across the width of the screen...
					gc.minDistance = 0.1f;
					gc.spread = 0.5f;
					gc.spatialBlend = 0.5f;                       // We never want full panning, it'll sound like an Amiga :D
					gc.rolloffMode = AudioRolloffMode.Linear;     // No need for log falloff, we're single screen.

				}

				// Don't ever want to destroy these!
				DontDestroyOnLoad(m_aSFXAudioPool[i]);
			}
			m_iSFXAudioPoolIndex = 0;
		}


		// Setup a GameObject that will hold the music clip
		{
			m_goMusic = new GameObject();
			m_goMusic.name = "AUDIO_Music";
			m_gcActiveMusicAudioSource = m_goMusic.AddComponent<AudioSource>();
			GAssert.Assert(null != m_gcActiveMusicAudioSource, "Unable to add Audio Source component to m_goMusic");
			m_gcActiveMusicAudioSource.outputAudioMixerGroup = m_Music;
			m_gcActiveMusicAudioSource.bypassEffects = true;
			m_gcActiveMusicAudioSource.bypassListenerEffects = true;
			m_gcActiveMusicAudioSource.bypassReverbZones = true;
			m_gcActiveMusicAudioSource.loop = true;
			m_gcActiveMusicAudioSource.spatialBlend = 0.0f;
			m_gcActiveMusicAudioSource.rolloffMode = AudioRolloffMode.Custom;       // Shouldn't need to set these, but just in case...
			m_gcActiveMusicAudioSource.minDistance = 0.1f;
			m_gcActiveMusicAudioSource.maxDistance = 100000000000.0f;

			// Again, don't want this disappearing somewhere...
			DontDestroyOnLoad(m_goMusic);
		}

		// Clear the effect list...
		NextFrame();
	}



	// Clear our list of SFX we're not playing again this frame...
	//
	public void NextFrame()
	{
		m_aEffectList.Clear();
	}



	// Will be called on first entry to the game!
	//
	public void SetDefaults()
	{
		if (null != m_Master)
		{
			// GNTODO: Load these params from persistent data
			// GNTODO: Set the sliders in the settings menu to the loaded values!
			// GNTODO: Save both when changed...
			m_Master.SetFloat("VOL_Master", GameGlobals.s_fVOL_Master);
			m_Master.SetFloat("VOL_Music", GameGlobals.s_fVOL_Music);
			m_Master.SetFloat("VOL_SFX", GameGlobals.s_fVOL_SFX);
		}
	}



	// Plays a given music track
	//
	public void PlayMusic(EGameMusic iTrackIndex)
	{
		GAssert.Assert(null != m_gcActiveMusicAudioSource, "Music Game Object is missing an audio source component");

		//switch (iTrackIndex) {
		//	case EGameMusic._CECCONOID_MAIN_MENU: m_gcActiveMusicAudioSource.clip = m_acCecconoidMusic_MainMenu; break;
		//	case EGameMusic._CECCONOID_INGAME: m_gcActiveMusicAudioSource.clip = m_acCecconoidMusic_InGame; break;
		//}

		GAssert.Assert(null != m_gcActiveMusicAudioSource.clip, "Audio source missing for music" );

		m_gcActiveMusicAudioSource.volume = 1.0f;
		m_gcActiveMusicAudioSource.Play();
	}



	public void StopMusic()
	{
		GAssert.Assert(null != m_gcActiveMusicAudioSource, "Music Game Object is missing an audio source component");
		m_gcActiveMusicAudioSource.Stop();
	}




	public void FadeMusicOutForGameOver()
	{
		StartCoroutine(FadeMusicOut(1.0f, true)); 
	}



	// Why not use the Mixer Group here?
	// Because the Mixer group is under the Player's control, and acts as a final scaler
	// to whatever we do with the Audio Sources. 
	//
	public System.Collections.IEnumerator FadeMusicOut(float fTime, bool bUseUITimer)
	{
		GAssert.Assert(null != m_gcActiveMusicAudioSource, "Music Game Object is missing an audio source component");
		m_bCanFadeMusic = true;

		float fInitVol = m_gcActiveMusicAudioSource.volume;
		while (m_gcActiveMusicAudioSource.volume > 0)
		{
			if(!bUseUITimer) m_gcActiveMusicAudioSource.volume -= TimerManager.fGameDeltaTime / fTime;
			else m_gcActiveMusicAudioSource.volume -= TimerManager.fUIDeltaTime / fTime;

			if(m_bCanFadeMusic)	yield return null;
			else break;
		}
		StopMusic();
	}

	public void FadeMusicOutInstant()
	{
		m_bCanFadeMusic = false;
		StopMusic();
	}



	public void PauseMusic()
	{
		GAssert.Assert(null != m_gcActiveMusicAudioSource, "Music Game Object is missing an audio source component");
		m_gcActiveMusicAudioSource.Pause();
	}




	public void UnPauseMusic()
	{
		GAssert.Assert(null != m_gcActiveMusicAudioSource, "Music Game Object is missing an audio source component");
		m_gcActiveMusicAudioSource.UnPause();
	}




	public void MuteMusic(bool bState)
	{
		if(bState) m_Master.SetFloat("VOL_Music", -80f); else m_Master.SetFloat("VOL_Music", GameGlobals.s_fVOL_Master);
	}


	
	public void MuteSFX(bool bState)
	{
		if (bState) m_Master.SetFloat("VOL_SFX", -80f); else m_Master.SetFloat("VOL_SFX", GameGlobals.s_fVOL_Master);
	}




	public void SetMusicVol(float fVal)
	{
		GameGlobals.s_fUI_SliderMusic = fVal;
		GameGlobals.s_fVOL_Music = ((1f - fVal) * -80f) - 2f;
		m_Master.SetFloat("VOL_Music", GameGlobals.s_fVOL_Music);
	}




	public void SetMasterVol(float fVal)
	{
		GameGlobals.s_fUI_SliderMaster = fVal;
		GameGlobals.s_fVOL_Master = ((1f - fVal) * -80f);
		m_Master.SetFloat("VOL_Master", GameGlobals.s_fVOL_Master);
	}



	public void SetSFXVol(float fVal)
	{
		GameGlobals.s_fUI_SliderSFX = fVal;
		GameGlobals.s_fVOL_SFX = ((1f - fVal) * -80f);
		m_Master.SetFloat("VOL_SFX", GameGlobals.s_fVOL_SFX);
	}


	// Grabs a new GameObject from the pool, set a new AudioClip and play the sting
	//
	public void PlayAudioAtLocation(Vector3 vPos, EGameSFX iSFX)
	{
		// Check that we've not already played this effect, this frame. Keeps sound levels sane...
		{
			if (m_aEffectList.Contains(iSFX)) return;
			else m_aEffectList.Add(iSFX);
		}

		AudioSource aSFX = m_aSFXAudioPool[m_iSFXAudioPoolIndex].GetComponent<AudioSource>();
		aSFX.Stop();

		switch (iSFX)
		{
			// GNTODO: Replace the clip, and change this enum!
			case EGameSFX._SFX1: aSFX.clip = null; break;
		}

		// Move the game object to the correct position so we get a smidge of spatial panning. 
		m_aSFXAudioPool[m_iSFXAudioPoolIndex].transform.position = vPos;
		aSFX.Play();

		// Circular buffer, so just rollover once we hit the end. 
		++m_iSFXAudioPoolIndex;
		if (m_iSFXAudioPoolIndex >= Types.s_iPoolSize_AudioSFX) m_iSFXAudioPoolIndex = 0;
	}



	// Helper function for UI objects, who don't care about location...
	//
	public void PlayAudio(EGameSFX iSFX)
	{
		PlayAudioAtLocation(GameInstance.Object.GetGameCamera().transform.position, iSFX);
	}
}
