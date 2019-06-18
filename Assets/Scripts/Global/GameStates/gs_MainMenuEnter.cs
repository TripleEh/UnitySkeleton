
public class gs_MainMenuEnter : GameState
{
	public override void Awake()
	{
		m_sStateName = "Main Menu ENTER";
		base.Awake();
	}

	private void Start()
	{
		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._MAINMENU_IN, "", false);
	}
}
