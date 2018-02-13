using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{

	public Vector3 rotateV3;

	void Update ()
	{
		transform.eulerAngles += rotateV3 * Time.deltaTime;
	}
}
