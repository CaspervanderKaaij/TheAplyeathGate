using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUI : MonoBehaviour
{

	private Vector3 center;
	private Vector3 centerRot;
	public bool centered;

	void Start ()
	{
		center = transform.localPosition;
		centerRot = transform.localEulerAngles;
	}

	void Update ()
	{
		if (transform.localRotation == Quaternion.Euler (centerRot)) {
			centered = true;
		} else {
			centered = false;
		}
		transform.localPosition = Vector3.MoveTowards (transform.localPosition, center, Time.unscaledDeltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, Quaternion.Euler (centerRot), Time.unscaledDeltaTime * 12);
	}

	public void MoveGun (Vector3 pos, Vector3 rot)
	{
		transform.localPosition += pos;
		if (rot != Vector3.zero) {
			transform.localEulerAngles = rot;
		}
	}
}
