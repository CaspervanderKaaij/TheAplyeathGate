using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class PlayerMovement : MonoBehaviour
{

	CharacterController cc;
	private Transform camNoY;
	public float speed = 5;
	public float ghostJumpTime = 0;
	private float curYVel = -9.81f;
	public string state = "normal";
	private bool landed;
	public AudioClip landSfx;
	CommonEffects ce;

	void Start ()
	{
		cc = transform.GetComponent<CharacterController> ();
		camNoY = transform.GetChild (1);
		curYVel = -9.81f;
		landed = true;
		ce = GameObject.FindObjectOfType<CommonEffects> ();
	}

	void Update ()
	{
		if (state != "cutscene") {
			if (state != "start") {
				camNoY.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
				//if (ghostJumpTime > 0.5f) {
				cc.Move (camNoY.TransformDirection (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, curYVel * Time.deltaTime, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				curYVel = Mathf.MoveTowards (curYVel, -9.81f, Time.deltaTime * 20);
				//} else {
				//cc.Move (camNoY.TransformDirection (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, -Time.deltaTime / 10, Input.GetAxis ("Vertical") * speed * Time.deltaTime));
				//}
				RaycastHit hit;
				if (Physics.SphereCast (new Ray (transform.position, Vector3.down), transform.lossyScale.x, out hit, 0.09f)) {
					ghostJumpTime = 0;
					if (hit.distance < 1.1f) {
						if (curYVel < 0) {
							curYVel = -1;
						}
					}
				} else if (Physics.Raycast (new Ray (transform.position, Vector3.down), out hit, 0.9f)) {
					ghostJumpTime = 0;
					if (hit.distance < 1.1f) {
						if (curYVel < 0) {
							curYVel = -1;
						}
					}
				} else {
					ghostJumpTime += Time.deltaTime;
					//landed = false;
				}

				if (landed == true) {
					if (cc.isGrounded == false) {
						landed = false;
					}
				} else if (cc.isGrounded == true) {
					landed = true;
					ce.PlaySound (landSfx, 1, 0, transform.position);
				}

				//if(ghostJumpTime < 0.1f){
				if (landed == false) {
					//GameObject.FindObjectOfType<CommonEffects> ().PlaySound (landSfx,1,0,transform.position);
					//landed = true;
				}
				//}

				if (Input.GetButtonDown ("Jump")) {
					if (ghostJumpTime < 0.1f) {
						//cc.Move (new Vector3(0,7.5f * Time.deltaTime,0));
						curYVel = 7.5f;
						//landed = false;
						ghostJumpTime = 1;
					}
				}
			}
		}
	}
}
