using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

	public bool[] spawn;
	public GameObject toSpawn;

	void Start ()
	{
		for (int i = 0; i < spawn.Length; i++) {
			if (spawn [i] == true) {
				GameObject p = GameObject.Instantiate (toSpawn);
				if (transform.GetChild (i) != null) {
					p.transform.position = transform.GetChild (i).position;
				} else {
					p.transform.position = transform.GetChild (0).position;
				}
			}
		}
	}

}
