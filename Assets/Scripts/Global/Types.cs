using UnityEngine;

// Contains type definitions / enums / constants / magic numbers / interfaces etc that are referenced globally
//
public static class Types
{
	// Game Constants
	//
	public const string s_sGameName = "NEW GAME";

	public const int s_iPoolSize_AudioSFX = 32;
	public const float s_fFixedDeltaTimeUpdate = 1f / 60f;

	public const float s_fPixelsPerUnit = 100.0f;
	public const float s_fPixelSize = 1.0f / s_fPixelsPerUnit;

	public const float s_fVOL_MaxAttenuation = -0.3f;
	public const float s_fVOL_MinAttenuation = -80.0f;
	public const float s_fVOL_DefaultMusic = -2.0f;

	public const float s_fCAM_ShakeDistanceScale = s_fPixelSize * 8.0f;
	public const float s_fCAM_ShakeDecay = 1.5f;
	public const float s_fCAM_ShakeDeadzone = 0.20f;


	// ------- Bit fields shifters, for GameGlobals.m_iGameStateFlags_0x to track events

	// Global Events
	public const ulong s_iGE_IntroShown = 0x01;

}