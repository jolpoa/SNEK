using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeController : MonoBehaviour {

	public float TimeToMove;

	public Text TimeText;

	public int _elapsedTimeAtAll_int { get; set; }


	private float elapsedTimeFromLastUpgrade;
	private float elapsedTimeAtAll;


	void Update()
	{
		SetTime ();
	}

	void SetTime()		//Function to calculate how much time elapsed from last update of snake
	{
		elapsedTimeFromLastUpgrade += Time.deltaTime;
		elapsedTimeAtAll += Time.deltaTime;
		ChangeTimeText ();
	}
		

	void ChangeTimeText()		//func to "draw" time on screen
	{
		_elapsedTimeAtAll_int = (int)elapsedTimeAtAll;
		TimeText.text = "Time\n" +_elapsedTimeAtAll_int.ToString ();
	}


	public bool ItIsTime()		//func to test if it is time to update snek
	{
		if (elapsedTimeFromLastUpgrade >= TimeToMove) 
		{
			elapsedTimeFromLastUpgrade = 0;
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
