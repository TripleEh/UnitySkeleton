using UnityEngine;


// GameInstance
//
// Global class that is set in editor to run before Default Time, ensuring that this is the first 
// class to be processed each frame... 
//


[RequireComponent(typeof(GameAudioManager))]
public class GameInstance : MonoBehaviour
{
	// Prefab References
	//
	[Header("Prefab References")]
	public GameObject m_goPlayerPrefab = null;


	// In-Game Spawned Object References
	//
	private GameObject m_goPlayerObjectReference = null;
	private PlayerState m_gcPlayerState = null;
	private PlayerController m_gcPlayerController = null;
	private PlayerInventory m_gcPlayerInventory = null;


	// Class References (Set these in the editor! They're attached to objects in the persistent scene)
	//
	[Header("Class References")]
	[SerializeField] private GameAudioManager m_gcAudioManager = null;
	[SerializeField] private GameCamera m_gcGameCamera = null;


	// Debug
	//
	[Header("Tweakables")]
	[SerializeField] private bool m_bDisplayDebugHud = true;
	private string m_sActiveRoom = "Room Name Not Set!";

	//------------------------------------------------------------------------------------------

	// For the FPS display in debug mode...
	private float m_fDeltaTime;



	// Singleton object.
	private static GameInstance obj;

	public static GameInstance Object
	{
		get
		{
			if (!obj)
			{
				obj = (GameInstance)GameObject.FindObjectOfType<GameInstance>();
#if !UNITY_EDITOR
				if (!obj) Debug.LogError("Unable to find GameInstance.Object! Awake function possibly calling for .Object before persistent scene loaded?");
#endif
			}

			return obj;
		}
	}

	//------------------------------------------------------------------------------------------


	public void Awake()
	{
		if (null == obj)
		{
			Debug.Log("GameInstance created! Welcome to " + Types.s_sGameName);
			obj = GetComponent<GameInstance>();
		}


		// Check that our class references have been set correctly. 
		{
		}
	}



	public void Start()
	{
		Cursor.visible = false;
		m_gcAudioManager.SetDefaults();
	}



	public void StartGame()
	{
		// Spawn the player offscreen. SetPlayerDefaults locks it and prevents movement.
		// Player Object remains in the persistent scene, so we don't need to care 
		// about what happens between scene loads...
		{
			m_goPlayerObjectReference = GameInstance.Instantiate(m_goPlayerPrefab, new Vector3(100,100,0), Quaternion.identity);
			GAssert.Assert(null != m_goPlayerObjectReference, "Unable to instantiate Player Object");
			m_gcPlayerState = m_goPlayerObjectReference.GetComponent<PlayerState>();
			GAssert.Assert(null != m_gcPlayerState, "Unable to get Player State");
			m_gcPlayerController = m_goPlayerObjectReference.GetComponent<PlayerController>();
			GAssert.Assert(null != m_gcPlayerController, "Unable to get Player Controller");
			m_gcPlayerInventory = m_goPlayerObjectReference.GetComponent<PlayerInventory>();
			GAssert.Assert(null != m_gcPlayerInventory, "Unable to get Player Inventory!");
		}

		// Set Game Defaults
		{
			Messenger.ClearAll();
			TimerManager.SetDefaults(1.0f, 1.0f);
			m_gcPlayerInventory.SetDefaults();
			m_gcPlayerState.SetDefaults();
			m_gcGameCamera.SetDefaults();
			GameGlobals.SetDefaults();

			// Put the player in the correct position! 
			// GNTODO: This needs to be loaded!
			//
			{
				// teleportPlayerToSpawnPoint...
			}
		}
	}




	public void EndGame()
	{
		Destroy(m_goPlayerObjectReference);
		m_gcPlayerState = null;
		m_gcPlayerController = null;
	}



	public void ExitToDesktop()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}



	void Update()	
	{
#if DEBUG
		// Display the Debug Hud!
		if (m_bDisplayDebugHud) m_fDeltaTime += (Time.unscaledDeltaTime - m_fDeltaTime) * 0.1f;
#endif

		// Process the TimerManager! This ensures all timer callbacks are processed
		// at the start of each frame, and our specific DeltaTimes are available and
		// accurate. GameInstance is the highest priority script, set in the editor...
		TimerManager.Update();

		// Audio manager needs to know we've moved on, and that it can reset the list
		// of played samples that it's tracking...
		m_gcAudioManager.NextFrame();
	}




	// Room Controllers can set the name of the active room to be shown in the Debug HUD
	public void SetDebugHudRoomName(string sRoomName)
	{
#if DEBUG
		if (m_bDisplayDebugHud) m_sActiveRoom = sRoomName;
#endif
	}

	// ------------------------------------------------------------------------------------------------------------------
	// Getters

	public GameAudioManager GetAudioManager() { return m_gcAudioManager; }
	public PlayerState GetPlayerState() { return m_gcPlayerState; }
	public GameObject GetPlayerObject() { return m_goPlayerObjectReference; }
	public PlayerController GetPlayerController() { return m_gcPlayerController; }
	public Vector3 GetPlayerPosition() { if (null == m_goPlayerObjectReference) return Vector3.zero; else return m_goPlayerObjectReference.transform.position; }
	public Vector2 GetPlayerTrajectory() { if (null == m_gcPlayerController) return Vector2.zero; else return m_gcPlayerController.GetMovementTrajectory(); }
	public GameCamera GetGameCamera() { return m_gcGameCamera; }
	public Camera GetGameCameraComponent() { return m_gcGameCamera.gameObject.GetComponent<Camera>(); }

	
	// ------------------------------------------------------------------------------------------------------------------
	// Debug

	void OnGUI()
	{
		if (!m_bDisplayDebugHud) return;

		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = 20;
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		float fMsec = m_fDeltaTime * 1000.0f;
		float fFPS = 1.0f / m_fDeltaTime;
		string sOutput = string.Format(m_sActiveRoom + ": {0:0.0} ms ({1:0.} fps)", fMsec, fFPS);

		Rect vRect = new Rect(50, 50, Screen.width, Screen.height / 50.0f);
		GUI.Label(vRect, sOutput, style);

		style.normal.textColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
		vRect.x += 1.0f;
		vRect.y += 1.0f;
		style.fontStyle = FontStyle.Normal;
		GUI.Label(vRect, sOutput, style);
	}
}
