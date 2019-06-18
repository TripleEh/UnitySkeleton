using UnityEngine;

// This class tracks the flags for Global Game Events / Params that may be 
// referred to by classes / managers / UI throughout the game. 
// 
// Most settings in this class will be saved/reloaded so would be persistent 
// for the player. 
//
// GNTODO: Write the save / load :D
//

public static class GameGlobals
{
	// Bitfields to track game events
	private static ulong m_iGameStateFlags_01;

	// Player Room Transitions, TransitionTo needs to be saved out as the player's last spawn point!
	public static Vector3 s_vRoomTransitionTo = Vector3.zero;
	public static Vector3 s_vRoomTransitionFrom = Vector3.zero;


	// User Preferences
	public static float s_fVOL_Master = Types.s_fVOL_MaxAttenuation;
	public static float s_fVOL_SFX = Types.s_fVOL_MaxAttenuation;
	public static float s_fVOL_Music = Types.s_fVOL_DefaultMusic;

	// Slider values to match the above...
	public static float s_fUI_SliderMaster = 1.0f;
	public static float s_fUI_SliderMusic = 1.0f;
	public static float s_fUI_SliderSFX = 1.0f;

	// Scores
	public static ulong s_iEugatronHighScore = 0;
	public static ulong s_iCecconoidHighScore = 0;
	public static ulong s_iEugatronPreviousBest = 0;
	public static ulong s_iCecconoidPreviousBest = 0;
	public static ulong s_iLastScore;
	public static bool s_bEugatronHighThisTurn = false;
	public static bool s_bCecconoidHighThisTurn = false;


	// Set a new high score and toggle the flag that the GameState checks 
	// to see if the player has score a new high score during this
	// session 
	public static void SetEugatronHighScore(ulong iNewScore)
	{
		s_iEugatronPreviousBest = s_iEugatronHighScore;
		s_iEugatronHighScore = iNewScore;
		s_bEugatronHighThisTurn = true;
		// GNTODO: Save
	}


	// As above...
	public static void SetCecconoidHighScore(ulong iNewScore)
	{
		s_iCecconoidPreviousBest = s_iCecconoidHighScore;
		s_iCecconoidHighScore = iNewScore;
		s_bCecconoidHighThisTurn = true;
		// GNTODO: Save
	}




	public static void SetGameEvent(ulong iFlag)
	{
		m_iGameStateFlags_01 |= iFlag;
	}



	public static bool TestGameEvent(ulong iFlag)
	{
		return (bool)((m_iGameStateFlags_01 & iFlag) != 0);
	}



	public static void ClearGameEvent(ulong iFlag)
	{
		m_iGameStateFlags_01 &= ~(iFlag);
	}



	public static void SetDefaults()
	{
		m_iGameStateFlags_01 = 0x00;
		SetGameEvent(Types.s_iGE_IntroShown);
	}
}
