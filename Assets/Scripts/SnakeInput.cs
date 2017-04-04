using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour {

	public GameInput _gameInput;

	TimeController _timeController;

	public SnakeInput(GameInput gameinput){
		_gameInput = gameinput;
	}
		
	public void UpdateSnake(Snake snake)
	{

		if (_gameInput.Direction == "up") {
			snake.MoveUp ();
		} else if (_gameInput.Direction == "left") {
			snake.MoveLeft ();
		} else if (_gameInput.Direction == "down") {
			snake.MoveDown ();
		} else if (_gameInput.Direction == "right") {
			snake.MoveRight ();
		}

		snake.ChangePositions ();
	}

	public void initSNEK(Snake snake)
	{				
		//func to add proper number of snake parts

		if (snake.sizeOfSnakeAtStart < 2)
			snake.sizeOfSnakeAtStart = 2;

		for (int i = 0; i < snake.sizeOfSnakeAtStart; i++) {
			snake.appendToSnake ();
			snake.Parts [i].transform.parent = snake.transform.parent;
		}

		UpdateSnake (snake);

	}
}
