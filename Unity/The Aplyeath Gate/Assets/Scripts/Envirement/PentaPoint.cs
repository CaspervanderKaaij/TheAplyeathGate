using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentaPoint : MonoBehaviour
{

	public Material matChange;

	Renderer rend;

	void Start ()
	{
		rend = transform.GetComponent<Renderer> ();
	}

	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Player") {
			transform.tag = "PentagramEnabled";
			//transform.GetComponent<Renderer> ().material.color = Color.black;
			rend.material = matChange;
		}
	}
}
