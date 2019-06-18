
public class gs_GameIn : GameState
{
	public override void Awake()
	{
		m_sStateName = "Game IN";
		base.Awake();
	}

	private void Start()
	{
		//if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._SPLASHSCREEN_IN, "", false);
	}
}
