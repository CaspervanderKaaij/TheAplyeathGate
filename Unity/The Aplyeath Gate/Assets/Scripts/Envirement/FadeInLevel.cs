using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInLevel : MonoBehaviour
{

	private Image img;

	void Start ()
	{
		img = transform.GetComponent<Image> ();
		img.color = Color.white;
	}


	void Update ()
	{
		img.color = Color.Lerp (img.color, Color.clear, Time.deltaTime);
		if (img.color.a < 0.4f) {
			if (Input.GetButtonUp ("Fire1")) {
				GameObject.FindObjectOfType<PlayerMovement> ().state = "normal";
				transform.parent.gameObject.SetActive (false);
			}
		}
	}
}
