using UnityEngine;


public class GameCamera : MonoBehaviour
{
	enum ECameraState
	{
		_IDLE,
	}


	// Origin's of the rooms we're in / moving from...
	private static Vector3 m_vCameraOrigin = Vector3.zero;
	private static Vector3 m_vOldCameraOrigin = Vector3.zero;

	// Strength of the cam shake at any given frame...
	private float m_fCamShakeAmplitude;

	// Flag to block shake etc...
	private ECameraState m_iState = ECameraState._IDLE;

	// Camera Origin will (for the most part) be centered onto a specific room
	public Vector3 vCameraOrigin {
		get { return m_vCameraOrigin; }
		set {}
	}



	// Set the camera to a known default position. 
	// For whatever fucking reason, World Origin is four pixels too high for the camera to 
	// perfectly frame the room...
	//
	public void SetDefaults()
	{
		m_vCameraOrigin = Vector3.zero;
		transform.position = m_vCameraOrigin; 
		m_fCamShakeAmplitude = 0.0f;
	}



	// Can be called multiple times a frame...
	//
	public void AddShake(float fAmount)
	{
		m_fCamShakeAmplitude = Mathf.Clamp01(m_fCamShakeAmplitude + fAmount);
	}



	public void BeginTeleport(Vector3 vNewOrigin)
	{
		m_fCamShakeAmplitude = 0.0f;
		m_vOldCameraOrigin = m_vCameraOrigin;
		m_vCameraOrigin = vNewOrigin;
		transform.position = m_vCameraOrigin;
	}



	// Do the correct update!
	//
	public void Update()
	{
		switch (m_iState)
		{
			case ECameraState._IDLE: IdleUpdate(); break;
			// GNTODO: Add any other states here, like transitioning between rooms
		}
	}



	// If the camera is idle, then it's allowed to shake with explosions...
	//
	void IdleUpdate()
	{
		if (m_fCamShakeAmplitude > Types.s_fCAM_ShakeDeadzone)
		{
			Vector3 vRand = Random.insideUnitSphere * (Types.s_fCAM_ShakeDistanceScale * m_fCamShakeAmplitude);
			vRand.z = 0.0f;
			transform.position = m_vCameraOrigin + vRand;
		}
		else transform.position = m_vCameraOrigin;

		m_fCamShakeAmplitude = Mathf.Clamp01(m_fCamShakeAmplitude - (Types.s_fCAM_ShakeDecay * TimerManager.fGameDeltaTime));
	}



	// Lerp, quickly, between two rooms...
	// Because the lerp duration is standardised, the camera doesn't need to
	// report anywhere that it's complete, it can safely revert to idle.
	//
	void TransitionUpdate()
	{
	}



	// Follow the player to a new location in the world.
	//
	public void WarpToPosition(ref Vector3 vPos)
	{
		m_vCameraOrigin = vPos;
		m_fCamShakeAmplitude = 0.0f;
		transform.position = m_vCameraOrigin;
	}
}
