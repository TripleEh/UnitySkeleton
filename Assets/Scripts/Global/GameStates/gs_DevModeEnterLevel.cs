// THIS IS A DEBUG MODE HELPER, FOR USE IN THE EDITOR ONLY!
//
public class gs_DevModeEnterLevel : GameState
{
	public override void Awake()
	{
		m_sStateName = "DevModeEnterLevel";
		base.Awake();
	}

	// Shortcut into the game when we're hitting play in the editor and the persistent scene 
	// hasn't been loaded. 
	//
	void Start()
	{
		GameInstance.Object.StartGame();
	}

	void Update()
	{
		if (m_gcGameStateManager.CanChangeState())
		{
			m_gcGameStateManager.ChangeState(EGameStates._GAME_IN, "", false);
		}
	}
}
