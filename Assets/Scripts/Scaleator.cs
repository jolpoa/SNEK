using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaleator : MonoBehaviour {
	[Range (1 , 8)]
	public float ScalingSpeed;

	[Range (0 , 1)]
	public float ScalingSize;
	private float pomocnicza;

	void FixedUpdate () {
		pomocnicza = Mathf.Cos (Time.timeSinceLevelLoad * ScalingSpeed);

		transform.localScale += new Vector3 (pomocnicza, pomocnicza, 0) * ScalingSize;
	}
}

