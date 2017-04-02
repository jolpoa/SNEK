using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float TimeToMove;
	public Text elTime;
	public float speed;

	private float elapsedTime;
	private Transform SnakeTransform;

	void Start()
	{
		elTime.text = "";

		SnakeTransform = GetComponent<Transform>();

		InvokeRepeating ("MoveThatSnake", TimeToMove, TimeToMove);
	}


	void Update () {
		
	
	}







	void MoveThatSnake()
	{
		SnakeTransform.Translate (SnakeTransform.right * speed);
	}

	void SetTime()
	{
		elapsedTime += Time.deltaTime;
	}

	bool ItIsTime()
	{
		if (elapsedTime >= TimeToMove) 
		{
			elapsedTime = 0;
			return true;
		} 
		else
			return false;
	}


	void ShowTime()
	{
		elTime.text = "Time :" + elapsedTime.ToString ();
	}
}
