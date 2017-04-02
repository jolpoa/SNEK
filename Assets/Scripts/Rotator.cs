using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float RotationSpeed;
	public float z;

	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, 0, z) * Time.deltaTime * RotationSpeed);
	}
}
