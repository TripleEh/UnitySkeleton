
public class gs_SplashScreenExit : GameState
{
	public override void Awake()
	{
		m_sStateName = "SplashScreen EXIT";
		base.Awake();
	}

	private void Start()
	{
		// Splash screen only gets shown once...
		GameGlobals.SetGameEvent(Types.s_iGE_IntroShown);

		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._MAINMENU_ENTER, "", false);
	}
}
