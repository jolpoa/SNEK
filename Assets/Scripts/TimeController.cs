using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeController : MonoBehaviour {

	[Range (0, 1)]
	public float TimeToMove;

	public Text TimeText;

	public Options _options;

	public int _elapsedTimeAtAll_int { get; set; }

	public int _elapsedTimeSeconds_int { get; set; }
	public int _elapsedTimeMinutes_int { get; set; }
	public string _elapsedTimeAtAll_string { get; set; }


	private float elapsedTimeFromLastUpgrade;
	private float elapsedTimeAtAll;

	private float _timeToMoveContainer;

	private bool TimerIsStoped = false;


	void Awake ()
	{
		_timeToMoveContainer = TimeToMove;
	}

	void Update()
	{
		SetTime ();
		ChangeHardness ();
	}

	void SetTime()		//Function to calculate how much time elapsed from last update of snake
	{
		elapsedTimeFromLastUpgrade += Time.deltaTime;
		elapsedTimeAtAll += Time.deltaTime;
		ChangeTimeText ();
	}

	public void StopTimer()
	{
		TimerIsStoped = true;
	}

	public void StartTimer()
	{
		TimerIsStoped = false;
	}

	public void ResetTimer()
	{
		elapsedTimeAtAll = 0;

		_elapsedTimeAtAll_string = "";
	}

	void ChangeTimeText()		//func to "draw" time on screen
	{
		if (!TimerIsStoped) {
			_elapsedTimeAtAll_int = (int)elapsedTimeAtAll;

			_elapsedTimeSeconds_int = _elapsedTimeAtAll_int % 60;
			_elapsedTimeMinutes_int = _elapsedTimeAtAll_int / 60;

			_elapsedTimeAtAll_string = _elapsedTimeMinutes_int.ToString ("00") + ":" + _elapsedTimeSeconds_int.ToString ("00");

			TimeText.text = "Time\n" + _elapsedTimeAtAll_string;
		}
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

	public void StopSnake()
	{
		TimeToMove = Mathf.Infinity;
	}

	public void ReviveSnake()
	{
		TimeToMove = _timeToMoveContainer;
	}

	void ChangeHardness()
	{
		if (_options.hardnessLvl.CurrentLvl == HardnessLvl.Easy)
			SetEasyLvl ();
		if (_options.hardnessLvl.CurrentLvl == HardnessLvl.Medium)
			SetMediumLvl ();
		if (_options.hardnessLvl.CurrentLvl == HardnessLvl.Hard)
			SetHardLvl ();
		if (_options.hardnessLvl.CurrentLvl == HardnessLvl.Imposibru)
			SetImposibruLvl ();
	}

	void SetEasyLvl()
	{
		_timeToMoveContainer = 0.4f;
	}
	void SetMediumLvl()
	{
		_timeToMoveContainer = 0.2f;
	}
	void SetHardLvl()
	{
		_timeToMoveContainer = 0.05f;
	}
	void SetImposibruLvl()
	{
		_timeToMoveContainer = 0.01f;
	}

	public class HardnessLvl
	{
		public string CurrentLvl;
		public static readonly string Easy = "Easy";
		public static readonly string Medium = "Medium";
		public static readonly string Hard = "Hard";
		public static readonly string Imposibru = "Imposibru";
	}
}
