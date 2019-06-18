public class gs_GameExit : GameState
{
	public override void Awake()
	{
		m_sStateName = "Game EXIT";
		base.Awake();
	}

	private void Start()
	{
		//if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._SPLASHSCREEN_IN, "", false);
	}
}
