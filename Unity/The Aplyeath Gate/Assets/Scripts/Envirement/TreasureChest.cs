using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{

	public bool open = false;
	public AudioClip sfxChestOpen;
	private float timer = 0;
	public int[] treasure;
	//private GameObject uiFindObject;
	private CommonEffects ce;
	private PlayerMovement plmo;

	void Start ()
	{
		//uiFindObject = GameObject.Find ("ItemGot");
		//uiFindObject.SetActive (false);
		ce = GameObject.FindObjectOfType<CommonEffects> ();
		plmo = GameObject.FindObjectOfType<PlayerMovement> ();
	}

	void OnTriggerStay (Collider col)
	{
		if (Input.GetButtonDown ("Fire1")) {
			if (col.tag == "Player") {
				if (open == false) {
					Health colHealth = col.GetComponent<Health> ();
					for (int i = 0; i < colHealth.item.Length; i++) {
						if (treasure [i] != 0) {
							colHealth.item [i] = treasure [i];
							treasure [i] = 0;
						}
					}
					open = true;
					ce.PlaySound (sfxChestOpen, 0.4f, 1, transform.position);
					plmo.state = "cutscene";
					timer += Time.deltaTime;
				}
			}
		}
		if (timer != 0) {
			if (timer < 0.75f) {
				if (open == true) {
					transform.GetChild (0).localRotation = Quaternion.Lerp (transform.GetChild (0).localRotation, Quaternion.Euler (27, 0, 0), Time.deltaTime * 10);
				}
				ce.uiChest.SetActive (true);
				timer += Time.deltaTime;
			} else {
				ce.uiChest.SetActive (false);
				plmo.state = "normal";
			}
		}
	}
}
