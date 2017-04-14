using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameStatusManager : MonoBehaviour {

	public ScoreManager _scoreManager;
	[Space(5)]
	public Options _options;
	[Space(5)]
	public GameInput _gameInput;
	[Space(5)]
	public Snake _snake;
	[Space(5)]
	public BackGround _backGround;
	[Space(5)]
	public SnakeInput _snakeInput;

	[Space(5)]
	public TimeController _timeController;

	[Space(5)]
	public SpriteManager _spriteManager;

	[Header ("Life UI")]
	public Image[] Lifes = new Image[3];

	[Space(5)]
	public GameObject YouDiedPanel;
	public GameObject YouLosePanel;
	public GameObject YouWonPanel;
	public GameObject PausePanel;
	public GameObject OptionsPanel;
	public GameObject MenuPanel;
	public GameObject StartPanel;

	[Space(5)]
	public Text LoseingText;


	public GameStatus gameStatus;

	void Awake () {
		Cursor.visible = false;
		gameStatus = new GameStatus();
		gameStatus.CurrentStatus = GameStatus.Start;
	}

	void Update () {
		CheckGameStatus ();
		ChangeImages ();
		Debug.Log (gameStatus.CurrentStatus);
	}

	private void CheckGameStatus()
	{
		if (CheckIfRestart ()) {
		
			LoseingText.text = "You Lose";

			_gameInput.ResetInput ();
			_snake.ResetSnake ();
			_snake.ResetDeaths ();
			_snakeInput.UpdateSnake (_snake);
			_timeController.ResetTimer ();
			_scoreManager.ResetScore ();
			_timeController.ReviveSnake ();
			_timeController.StartTimer ();

			YouLosePanel.SetActive (false);
			YouDiedPanel.SetActive (false);
			YouWonPanel.SetActive (false);

		} else if (CheckIfAnotherLife ()) {
			
			YouDiedPanel.SetActive (false);

			_gameInput.ResetInput ();
			_snake.ResetSnake ();
			_snakeInput.UpdateSnake (_snake);
			_timeController.ReviveSnake ();
			_timeController.StartTimer ();

		} else if (CheckIfLose ()) {

			if (_options.hardnessLvl.CurrentLvl == HardnessLvl.Imposibru)
				LoseingText.text = "I told you Imposibru...";
			
			_timeController.StopSnake ();
			_timeController.StopTimer ();

			YouLosePanel.SetActive (true);

		} else if (CheckIfDied ()) {
			
			_timeController.StopSnake ();
			_timeController.StopTimer ();

			YouDiedPanel.SetActive (true);

		} else if (CheckIfWon ()) {
			
			_timeController.StopSnake ();
			_timeController.StopTimer ();

			YouWonPanel.SetActive (true);

		} else if (CheckIfEndOfPause ()) {

			PausePanel.SetActive (false);

			_timeController.StartTimer ();
			_timeController.ReviveSnake ();

		} else if (CheckIfPause ()) {
			
			_timeController.StopSnake ();
			_timeController.StopTimer ();

			PausePanel.SetActive (true);

		} else if (CheckIfEndOfMenu ()) {
			
			MenuPanel.SetActive (false);

			Cursor.visible = false;

			_timeController.StartTimer ();
			_timeController.ReviveSnake ();

		} else if (CheckIfMenu ()) {

			MenuPanel.SetActive (true);
			OptionsPanel.SetActive (false);

			Cursor.visible = true;

			_timeController.StopSnake ();
			_timeController.StopTimer ();

		} else if (CheckIfEndOfOptions ()) {

			MenuPanel.SetActive (true);
			OptionsPanel.SetActive (false);

		} else if (CheckIfOptions ()) {

			MenuPanel.SetActive (false);
			OptionsPanel.SetActive (true);

		} else if (CheckIfEndOfStart ()) {

			StartPanel.SetActive (false);

			_timeController.StartTimer ();
			_timeController.ReviveSnake ();

		} else if (CheckIfStart ()) {
			
			_timeController.StopSnake ();
			_timeController.StopTimer ();

		} 
	}

	private bool CheckIfStart()
	{
		if (gameStatus.CurrentStatus == GameStatus.Start) {
			return true;
		}
		return false;
	}

	private bool CheckIfEndOfStart()
	{
		if (gameStatus.CurrentStatus == GameStatus.Start && _gameInput.RestartKey) {
			gameStatus.CurrentStatus = GameStatus.Play;
			return true;
		}
		return false;
	}

	private bool CheckIfPlaying()
	{
		if (gameStatus.CurrentStatus == GameStatus.Play) {
			return true;
		}
		return false;
	}

	private bool CheckIfRestart()
	{
		if ((gameStatus.CurrentStatus == GameStatus.Play || gameStatus.CurrentStatus == GameStatus.Lose) && _gameInput.RestartKey) {
			return true;
		}
		return false;
	}

	private bool CheckIfAnotherLife()
	{
		if ((gameStatus.CurrentStatus == GameStatus.Died || gameStatus.CurrentStatus == GameStatus.Win) && _gameInput.RestartKey) {
			gameStatus.CurrentStatus = GameStatus.Play;
			return true;
		}
		return false;
	}

	private bool CheckIfLose()
	{
		if (_snake.deaths == 3) {
			gameStatus.CurrentStatus = GameStatus.Lose;
			return true;
		}
		return false;
	}


	private bool CheckIfDied()
	{
		if (gameStatus.CurrentStatus == GameStatus.Died) {
			return true;
		}
		return false;
	}

	private bool CheckIfWon()
	{
		if (_snake.Parts.Count >= (_backGround.xNumberOfTiles * _backGround.yNumberOfTiles) - 1){
			gameStatus.CurrentStatus = GameStatus.Win;
			return true;
		}
		return false;
	}

	private bool CheckIfPause()
	{
		if (_gameInput.PauseKey) {
			gameStatus.CurrentStatus = GameStatus.Pause;
			return true;
		}
		return false;
	}

	private bool CheckIfEndOfPause()
	{
		if (gameStatus.CurrentStatus == GameStatus.Pause && _gameInput.RestartKey) {
			gameStatus.CurrentStatus = GameStatus.Play;
			return true;
		}
		return false;
	}

	private bool CheckIfMenu()
	{
		if (gameStatus.CurrentStatus != GameStatus.Start && _gameInput.MenuKey) {
			gameStatus.CurrentStatus = GameStatus.Menu;
			return true;
		}
		return false;
	}

	private bool CheckIfEndOfMenu()
	{
		if (gameStatus.CurrentStatus == GameStatus.Menu && _gameInput.MenuKey) {
			gameStatus.CurrentStatus = GameStatus.Play;
			return true;
		}
		return false;
	}

	private bool CheckIfOptions()
	{
		if (gameStatus.CurrentStatus == GameStatus.Options) {
			return true;
		}
		return false;
	}

	private bool CheckIfEndOfOptions()
	{
		if (gameStatus.CurrentStatus == GameStatus.Options && _gameInput.MenuKey) {
			gameStatus.CurrentStatus = GameStatus.Menu;
			return true;
		}
		return false;
	}

	private void ChangeImages()
	{
		if (_snake.deaths >= 1) {
			Lifes [_snake.deaths - 1].sprite = _spriteManager.HeadWithBloodSprite;
		}
		for (int i = _snake.deaths; i <= 2; i ++)
			Lifes [i].sprite = _spriteManager.HeadSprite;
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
