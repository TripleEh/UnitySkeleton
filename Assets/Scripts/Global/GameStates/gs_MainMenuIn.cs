
public class gs_MainMenuIn : GameState
{
	public override void Awake()
	{
		m_sStateName = "Main Menu IN";
		base.Awake();
	}

	private void Start()
	{
		if (m_gcGameStateManager.CanChangeState()) m_gcGameStateManager.ChangeState(EGameStates._MAINMENU_EXIT, "", false);
	}
}
