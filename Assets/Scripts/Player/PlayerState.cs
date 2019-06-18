using UnityEngine;


public class PlayerState : MonoBehaviour
{

	// Order is important here, this must be called AFTER PlayerInventory in the GameInstance!
	// 
	public void SetDefaults()
	{
	}



	// This is a cheat mode function, can be accessed through the DevMenu
	//
	public void SetPlayerIsGod(bool bState)
	{
	}



	// Another cheat mode function, will give all to Players. DevMenu
	//
	public void SetPlayerInventoryAll(bool bState)
	{
	}



	public void UnlockPlayer()
	{
	}



	public void LockPlayer()
	{
	}



	public void AddScore(uint iScore)
	{
	}



	public void AddLives(uint iAmount)
	{
	}



	public void AddMultiplier(uint iAmount)
	{
	}



	public void ResetMultiplier()
	{
	}


	public void OnPlayerHasDied()
	{
	}



	public void OnPlayerRespawn()
	{
	}
}
