using UnityEngine;

public class PlaySFXOnSpawn : MonoBehaviour
{
	[SerializeField] private EGameSFX m_iSFX = EGameSFX._SFX1;

	private void Start()
	{
		GameInstance.Object.GetAudioManager().PlayAudioAtLocation(transform.position, m_iSFX);
	}
}
