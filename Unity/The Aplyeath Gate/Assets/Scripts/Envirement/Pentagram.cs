using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pentagram : MonoBehaviour
{

	public GameObject[] points;

	void Start ()
	{
		for (int i = 0; i <= points.Length - 1; i++) {
			points [i] = transform.GetChild (i + 1).gameObject;
			points [i].tag = "PentagramDisabled";
		}
	}

	void Update ()
	{
		int p = 0;
		for (int i = 0; i <= points.Length - 1; i++) {
			if (points [i].tag == "PentagramEnabled") {
				p += 1;
			}
			if (p >= points.Length) {
				SceneManager.LoadScene (2);
			}
		}
	}
}
