using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

	public GameObject MenuPanel;
	public GameObject OptionsPanel;

	public HardnessLvl hardnessLvl;

	public GameInput _gameInput;

	public GameStatusManager _gameStatusManager;


	void Awake()
	{
		hardnessLvl = new HardnessLvl ();
	}
				
	public void SetEasyLvl()
	{
		hardnessLvl.CurrentLvl = HardnessLvl.Easy;
	}

	public void SetMediumLvl()
	{
		hardnessLvl.CurrentLvl = HardnessLvl.Medium;
	}

	public void SetHardLvl()
	{
		hardnessLvl.CurrentLvl = HardnessLvl.Hard;
	}

	public void SetImpossibruLvl()
	{
		hardnessLvl.CurrentLvl = HardnessLvl.Imposibru;
	}

	public class HardnessLvl
	{
		public string CurrentLvl;
		public static readonly string Easy = "Easy";
		public static readonly string Medium = "Medium";
		public static readonly string Hard = "Hard";
		public static readonly string Imposibru = "Imposibru";
	}

	public class GameStatus
	{
		public string CurrentStatus;
		public static readonly string Start = "Start";
		public static readonly string Lose = "Lose";
		public static readonly string Win = "Win";
		public static readonly string Died = "Died";
		public static readonly string Pause = "Pause";
		public static readonly string Play = "Play";
		public static readonly string Menu = "Menu";
		public static readonly string Options = "Options";
	}
}
