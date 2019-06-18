
public class gs_SplashScreenEnter : GameState
{
	public override void Awake()
	{
		m_sStateName = "SplashScreen ENTER";
		base.Awake();
	}

	private void Start()
	{
		if(m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._SPLASHSCREEN_IN, "", false);
	}
}
