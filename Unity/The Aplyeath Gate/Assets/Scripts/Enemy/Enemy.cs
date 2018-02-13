using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Animator ac;
	public int state = 0;
	public Transform goal;
	private PlayerMovement plmo;
	public Transform shootRayOrgin;
	private float timer = 0;
	private CommonEffects ce;
	public AudioClip shootSFX;
	public float sightRange = 10;
	public float sightRangeMuliplier = 1;
	public float sightMultiTimer = 0;

	void Start ()
	{
		ac = transform.GetComponent<Animator> ();
		plmo = GameObject.FindObjectOfType<PlayerMovement> ();
		ce = GameObject.FindObjectOfType<CommonEffects> ();
	}

	void Update ()
	{
		if (sightMultiTimer == 0) {
			sightRangeMuliplier = 1;
		} else {
			sightMultiTimer = Mathf.MoveTowards (sightMultiTimer, 0, Time.deltaTime);
		}
		if (state == 0) {
			Vector3 playerpos = plmo.transform.position;
			if (Vector3.Distance (transform.position, playerpos) < sightRange * sightRangeMuliplier) {
				Vector3 toTarget = (playerpos - transform.position).normalized;
				if (Vector3.Dot (toTarget, transform.right) > 0.75f) {
					if (goal != null) {
						state = 1;
					} else {
						state = 2;
					}
				}
			}
		}

		if (state == 1) {
			transform.LookAt (new Vector3 (goal.position.x, transform.position.y, goal.position.z));
			transform.eulerAngles -= new Vector3 (0, 90, 0);
			transform.position += transform.TransformDirection (Vector3.right * 2 * Time.deltaTime);
			if (Vector3.Distance (transform.position, goal.position) < 0.5f) {
				state = 2;
			}
		} else if (state == 2) {
			Vector3 playerpos = plmo.transform.position;
			if (Vector3.Distance (transform.position, playerpos) > sightRange * 3) {
				state = 0;
			}
			transform.LookAt (new Vector3 (playerpos.x, transform.position.y, playerpos.z));
			transform.eulerAngles -= new Vector3 (0, 90, 0);
			timer += Time.deltaTime;
			//Debug.DrawRay (shootRayOrgin.position, plmo.transform.position - transform.position);
			if (timer > 0.583f) {
				ce.PlaySound (shootSFX, 1, 1, transform.position);
				RaycastHit hit;
				if (Physics.Raycast (new Ray (shootRayOrgin.position, plmo.transform.position - transform.position), out hit, 10)) {
					if (hit.transform.tag == "Player") {
						if (plmo.state != "cutscene") {
							if (Vector2.SqrMagnitude (new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"))) == 0) {
								if (plmo.ghostJumpTime < 0.1f) {
									hit.transform.GetComponent <Health> ().DoDamage (5f, 1, Vector3.zero);
								}
							}
						}
					}
				}
				timer = 0;
			}
		}
		ac.SetInteger ("state", state);
	}
}
