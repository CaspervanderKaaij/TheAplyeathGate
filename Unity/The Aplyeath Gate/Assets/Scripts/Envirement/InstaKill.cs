using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{

	void OnTriggerEnter (Collider col)
	{
		if (col.GetComponent<PlayerMovement> () != null) {
			col.GetComponent<Health> ().health = 0;
		}
	}
}
