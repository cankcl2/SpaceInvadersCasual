using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupShipSpeed : Pickup
{
	public override void PickMeUp()
	{
		//Geminin h�z�n� de�i�tir
		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats.shipSpeed -= 0.1f;
		gameObject.SetActive(false);
	}
}
