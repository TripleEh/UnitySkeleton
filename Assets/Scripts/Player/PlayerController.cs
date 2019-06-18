using UnityEngine;

[RequireComponent(typeof(PlayerState))]
public class PlayerController : MonoBehaviour
{
	// Grab all the components we need control of. 
	//
	void Awake()
	{
	}


	public void Update()
	{
		if (TimerManager.IsPaused()) return;

		// Clear Input
		{
		}

		// Handle Game Pause
		{
		}

		// Handle the developer menu
		{
		}
	}

	

	// Do the correct update for the current state.
	//
	private void FixedUpdate()
	{
		if (TimerManager.IsPaused()) return;
	}




	// Update the controls
	//
	private void PlayerUpdate()
	{
		/*
		if (m_gcPlayerState.GetPlayerCanMove())
		{
			// Get Input
			{
			}

			// Do the Deadzone
			{
				if (m_vMovementTrajectory.magnitude < Types.s_fDeadZone_Movement)
					m_vMovementTrajectory = Vector2.zero;
				else
					m_vMovementTrajectory = m_vMovementTrajectory.normalized * ((m_vMovementTrajectory.magnitude - Types.s_fDeadZone_Movement) / (1.0f - Types.s_fDeadZone_Movement));

				if (m_vFireTrajectory.magnitude < Types.s_fDeadZone_Firing)
					m_vFireTrajectory = Vector2.zero;
				else
					m_vFireTrajectory = m_vFireTrajectory.normalized * ((m_vFireTrajectory.magnitude - Types.s_fDeadZone_Firing) / (1.0f - Types.s_fDeadZone_Firing));
			}

			// Move the object
			Vector3 vTraj = (new Vector3(m_vMovementTrajectory.x, m_vMovementTrajectory.y, 0.0f) * m_gcPlayerState.GetPlayerMovementSpeed()) * TimerManager.fFixedDeltaTime;
			m_gcRgdBdy.MovePosition(transform.position + vTraj);
		}

		// Pass fire to the equipped weapon
		if (m_vFireTrajectory.magnitude > 0.0f) m_gcPlayerState.UpdateEquippedWeapon(m_vFireTrajectory, true);
		else m_gcPlayerState.UpdateEquippedWeapon(Vector2.zero, false);
		*/
	}



	// Some of the enemy bullets use the player's trajectory to 'guess' where the player
	// will be in a few frames time...
	//
	public Vector2 GetMovementTrajectory()
	{
		return Vector2.zero;
	}



	public void MovePlayerInstant(Vector3 vPos)
	{
		transform.position = vPos;
	}
}
