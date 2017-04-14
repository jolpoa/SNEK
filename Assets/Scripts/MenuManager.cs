using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	public GameObject OptionsPanel;

	public GameStatusManager _gameStatusManager;

	public void OpenOptions()
	{
		_gameStatusManager.gameStatus.CurrentStatus = GameStatus.Options;
	}

	public void BackToTitleScreen()
	{
		SceneManager.LoadScene ("StartingScreen");
	}

	public void Exit()
	{
		Application.Quit ();
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
