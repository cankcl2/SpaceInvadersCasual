using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup
{
	public override void PickMeUp()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddHealth();
		gameObject.SetActive(false);
	}
}