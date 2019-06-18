using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
	// Bitfield for the inventory...
	private ulong m_iInventoryItems;

	// Sets iItem bit to high in the inventory bitfield
	//
	public void SetInventoryItem(ulong iItem)
	{
		m_iInventoryItems |= iItem;
	}



	// Checks the bitfield to see if a given bit is high
	//
	public bool HasInventoryItem(ulong iItem)
	{
		return (bool)((m_iInventoryItems & iItem) != 0);
	}



	// Clears iItem bit 
	// 
	public void ClearInventoryItem(ulong iItem)
	{
		m_iInventoryItems &= ~(iItem);
	}



	public void SetDefaults()
	{
		m_iInventoryItems = 0x0;
	}



	public void AddAll()
	{
		//m_iInventoryItems = Types.s_iINV_ALL;
	}



	public void GetEquippedWeapon()
	{
	}



	public void DropInventory()
	{
	}
}
