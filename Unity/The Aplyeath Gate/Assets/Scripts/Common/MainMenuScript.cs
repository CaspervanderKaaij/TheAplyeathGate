using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

	public Image[] images;
	private GameObject loading;
	public Image fadeIn;

	void Start ()
	{
		loading = GameObject.FindGameObjectWithTag ("Enemy");
		loading.SetActive (false);
		fadeIn.color = Color.white;
		for (int i = 0; i < images.Length; i++) {
			images [i].color = new Color (0, 0, 0, 0.234f);
		}
		for (int q = 0; q < loading.transform.childCount; q++) {
			loading.transform.GetChild (q).gameObject.SetActive (true);
		}
	}

	void Update ()
	{
		if (fadeIn.color.a < 0.2f) {
			Ray mainCamRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (mainCamRay, out hit)) {
				if (hit.transform.tag == "PentagramEnabled") {
					images [0].color = new Color (1, 1, 1, 1);
				} else if (hit.transform.tag == "PentagramDisabled") {
					images [1].color = new Color (1, 1, 1, 1);
				}
			} else {
				for (int i = 0; i < images.Length; i++) {
					images [i].color = new Color (0, 0, 0, 0.234f);
				}
			}

			if (Input.GetButtonDown ("Fire1")) {
				for (int i = 0; i < images.Length; i++) {
					if (images [i].color == new Color (0, 0, 0, 0.234f)) {
						if (i == 1) {
							loading.SetActive (true);
							Camera.main.GetComponent<AudioListener> ().enabled = false;
							SceneManager.LoadScene (1);
						} else {
							Application.Quit ();
						}
					}
				}
			}
		} else {
			fadeIn.color = Color.Lerp (fadeIn.color, Color.clear, Time.deltaTime);
		}
	}

	void FixedUpdate ()
	{
		Screen.SetResolution (1920, 1080, true);
	}
}
