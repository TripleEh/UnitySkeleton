using UnityEngine;

public static class GameMode
{
	// UI and parts of the game flow should block the player from 
	// being able to use the pause button
	private static bool m_bCanPause = false;
	private static bool m_bDevMenuActive = false;


	// Block pausing 
	//
	public static void SetCanPause(bool bState)
	{
		m_bCanPause = bState;
	}



	// Game-wide handler for the pause button. Stops our game timer and 
	// sends out the appropriate messages. We don't need to care what
	// responds to these...
	//
	public static void PauseGame()
	{
		if (!m_bCanPause) return;
		TimerManager.PauseGame();
		//Messenger.Invoke(Types.s_sHUD_Hide);
		//Messenger.Invoke(Types.s_sUI_ShowPauseScreen);
	}



	// As above, but to return the player back to the game. Again, we
	// don't need to care what actually responds to these messages
	//
	public static void UnPauseGame()
	{
		if (!m_bCanPause) return;
		TimerManager.UnPauseGame();
		//Messenger.Invoke(Types.s_sHUD_ResetHud);
		//Messenger.Invoke(Types.s_sUI_HidePauseScreen);
	}



	public static void ToggleDevMenu()
	{
#if DEBUG
		if(!m_bCanPause) return;

		if (!m_bDevMenuActive)
		{
			TimerManager.PauseGame();
			//Messenger.Invoke(Types.s_sHUD_Hide); 
			//Messenger.Invoke(Types.s_sMenu_DevMenuShow);
			m_bDevMenuActive = true;
		}
		else
		{
			TimerManager.UnPauseGame();
			//Messenger.Invoke(Types.s_sHUD_ResetHud); 
			//Messenger.Invoke(Types.s_sMenu_DevMenuHide);
			m_bDevMenuActive = false;
		}

		Cursor.visible = m_bDevMenuActive;
#endif 
	}
}
