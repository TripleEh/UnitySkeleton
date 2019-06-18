public class gs_GameEnter : GameState
{
	public override void Awake()
	{
		m_sStateName = "Game ENTER";
		base.Awake();
	}

	private void Start()
	{
		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._GAME_IN, "", false);
	}
}
