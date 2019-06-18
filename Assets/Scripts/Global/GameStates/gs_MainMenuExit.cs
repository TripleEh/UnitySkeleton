public class gs_MainMenuExit : GameState
{
	public override void Awake()
	{
		m_sStateName = "Maine Menu EXIT";
		base.Awake();
	}

	private void Start()
	{
		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._GAME_ENTER, "", false);
	}
}
