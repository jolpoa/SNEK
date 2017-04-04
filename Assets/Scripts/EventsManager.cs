using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {

	public SnakeInput _snakeInput;

	public GameInput _gameInput;

	public TimeController _timeController;

	public Snake _snake;

	void Start () {
		_snakeInput.initSNEK (_snake);
		_snake.SpriteLoads ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_snake.SetLastPositionOfTail ();

		_gameInput.UpdateInput ();

		if (_timeController.ItIsTime())
			_snakeInput.UpdateSnake (_snake);
	}

	void Update()
	{
		_snake.ChangingSprites ();
	}
}
