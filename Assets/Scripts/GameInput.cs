using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput:MonoBehaviour{

	public float Horizontal { get { return Input.GetAxis ("Horizontal"); } }
	public float Vertical { get { return Input.GetAxis ("Vertical"); } }

	public string Direction { get; private set; }

	void Awake()
	{
		Direction = "right";
	}

	public void UpdateInput()
	{
		if (Vertical > 0)
			Direction = "up";
		if (Vertical < 0)
			Direction = "down";
		if (Horizontal > 0)
			Direction = "right";
		if (Horizontal < 0)
			Direction = "left";
	}
		
}
