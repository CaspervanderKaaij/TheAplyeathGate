using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

	public float speed = 50;
	private CommonEffects ce;
	public int level = 36;
	public AudioClip gunShot;
	private PlayerMovement pm;
	private GunUI gunUi;
	public GameObject bloodParticle;
	public Text levelUI;

	void Start ()
	{
		Cursor.lockState = CursorLockMode.None;
		ce = GameObject.FindObjectOfType<CommonEffects> ();
		pm = GameObject.FindObjectOfType<PlayerMovement> ();
		gunUi = GameObject.FindObjectOfType<GunUI> ();
		if (Application.isEditor == false) {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	void Update ()
	{
		levelUI.text = "LVL " + level;
		if (pm.state != "cutscene") {
			if (pm.state != "start") {
				if (Application.isEditor == true) {
					if (Input.GetKeyDown (KeyCode.LeftShift)) {
						if (Cursor.lockState == CursorLockMode.Locked) {
							Cursor.lockState = CursorLockMode.None;
						} else {
							Cursor.lockState = CursorLockMode.Locked;
						}
					}
				}
				Vector2 rotateHelp = new Vector2 (-Input.GetAxis ("Mouse Y"), Input.GetAxis ("Mouse X"));
				if (Mathf.Abs (rotateHelp.x) > 3) {
					if (rotateHelp.x > 0) {
						rotateHelp.x = 3;
					} else {
						rotateHelp.x = -3;
					}
				}
				if (Mathf.Abs (rotateHelp.y) > 7) {
					if (rotateHelp.y > 0) {
						rotateHelp.y = 7;
					} else {
						rotateHelp.y = -7;
					}
				}
				transform.eulerAngles += new Vector3 (rotateHelp.x, rotateHelp.y, 0) * Time.deltaTime * speed;
				gunUi.MoveGun (new Vector3 (-rotateHelp.y / 100, rotateHelp.x / 100, 0), Vector3.zero);

				if (transform.eulerAngles.x > 60) {
					if (transform.eulerAngles.x < 180) {
						transform.eulerAngles = new Vector3 (59.9999f, transform.eulerAngles.y, transform.eulerAngles.z);
					}
				}
				if (transform.eulerAngles.x < 300) {
					if (transform.eulerAngles.x > 180) {
						transform.eulerAngles = new Vector3 (300.0001f, transform.eulerAngles.y, transform.eulerAngles.z);
					}
				}

				if (Input.GetButtonDown ("Fire1")) {
					Shoot ();
				}
			}
		}
	}

	public void Shoot ()
	{
		if (gunUi.centered == true) {
			RaycastHit hit;
			ce.PlaySound (gunShot, 1, 0, transform.position);
			gunUi.MoveGun (new Vector3 (0, 0.1f, 0), new Vector3 (-109, 0, -7.566f));
			ce.TimeStop (0.1f, 0.01f);
			if (Physics.Raycast (new Ray (transform.position, transform.forward), out hit)) {
				if (hit.transform.tag == "Enemy") {
					hit.transform.GetComponent<Health> ().DoDamage (1, level, new Vector2 (transform.position.x, transform.position.z));
					GameObject p = GameObject.Instantiate (bloodParticle);
					p.transform.position = hit.point;
				}
			}
		}
	}
}
