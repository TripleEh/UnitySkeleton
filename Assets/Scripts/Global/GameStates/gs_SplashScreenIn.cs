
public class gs_SplashScreenIn : GameState
{
	public override void Awake()
	{
		m_sStateName = "SplashScreen IN";
		base.Awake();
	}

	private void Start()
	{
		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._SPLASHSCREEN_EXIT, "", false);
	}
}
