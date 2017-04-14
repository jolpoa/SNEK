using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public Snake _snake;

	public Text ScoreText;

	public int Score { get; set; }


	void Update()
	{
		SetScore ();
	}

	void SetScore()		//Function to calculate how much time elapsed from last update of snake
	{
		ChangeScoreText ();
	}

	public void ResetScore()
	{
		Score = 0;
	}

	void ChangeScoreText()		//func to "draw" time on screen
	{
		ScoreText.text = "Score\n" + Score.ToString ();
	}

}
