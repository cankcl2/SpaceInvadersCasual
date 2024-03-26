using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupFireRate : Pickup
{
	public override void PickMeUp()
	{
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats.fireRate > 0.1f)
        {
			GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().shipStats.fireRate -= 0.03f;

		}
		gameObject.SetActive(false);
	}
}
