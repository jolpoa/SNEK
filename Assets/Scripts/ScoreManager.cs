using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public Snake _snake;

	public Text ScoreText;

	private int Score { get; set; }


	void Update()
	{
		SetTime ();
	}

	void SetTime()		//Function to calculate how much time elapsed from last update of snake
	{
		Score = _snake.Score;
		ChangeScoreText ();
	}


	void ChangeScoreText()		//func to "draw" time on screen
	{
		ScoreText.text = "Score\n" + Score.ToString ();
	}

}
