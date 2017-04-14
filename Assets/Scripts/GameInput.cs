using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput:MonoBehaviour{

	public float Horizontal { get { return Input.GetAxis ("Horizontal"); } }
	public float Vertical { get { return Input.GetAxis ("Vertical"); } }
	public bool PauseKey { get { return Input.GetKeyDown (KeyCode.P); } }
	public bool RestartKey { get { return Input.GetKeyDown (KeyCode.R); } }
	public bool MenuKey { get { return Input.GetKeyDown (KeyCode.Escape); } }
	public bool AnyKey { get { return Input.anyKey; } }


	public string Direction { get; private set; }

	void Awake()
	{
		Direction = "right";
	}

	public void ResetInput()
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
