using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public float TimeToMove;
	public Text elTime;


	private float elapsedTime;


	void Update()
	{
		SetTime ();
	}

	void SetTime()		//Function to calculate how much time elapsed from last update of snake
	{
		elapsedTime += Time.deltaTime;
		ShowTime ();
	}
		

	void ShowTime()		//func to "draw" time on screen
	{
		elTime.text = "Time :" + elapsedTime.ToString ();
	}


	public bool ItIsTime()		//func to test if it is time to update snek
	{
		if (elapsedTime >= TimeToMove) 
		{
			elapsedTime = 0;
			return true;
		} 
		else
			return false;
	}

	public void FreezeTime()
	{
		Time.timeScale = 0;
	}

}
