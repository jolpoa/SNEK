using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

	public Snake _snake;

	public BackGround _background;

	public Fruit _fruit;

	private Matrix Tiles { get; set; }

	void Awake () {
		Tiles = new Matrix ();
		SetUpTiles ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		BlankTiles ();
		CheckWhereIsSnake ();
	}


	private void BlankTiles()
	{
		for (int i = 0; i < Tiles.R; i++) {
			for (int j = 0; j < Tiles.C; j++) {
				Tiles.tab [i,j] = false;
			}
		}
	}

	private void CheckWhereIsSnake()
	{
		Tiles.tab [(int)_snake.transform.position.x + (_background.xNumberOfTiles / 2) + 1, (int)_snake.transform.position.y + (_background.yNumberOfTiles / 2) + 1] = true;

		for (int i = 0; i < _snake.Parts.Count; i++) {
			Tiles.tab [(int)_snake.Parts[i].transform.position.x + 6, (int)_snake.Parts[i].transform.position.y + 4] = true;
		}
	}

	public void SpawnFruit ()
	{
		bool spawned = false;

		int xSpawnedPosition;
		int ySpawnedPosition;

		int _xNumberOfTiles = _background.xNumberOfTiles;
		int _yNumberOfTiles = _background.yNumberOfTiles;

		while (!spawned) {

			xSpawnedPosition = Random.Range (0, _xNumberOfTiles );
			ySpawnedPosition = Random.Range (0, _yNumberOfTiles );

			if (Tiles.tab [xSpawnedPosition, ySpawnedPosition] == true)
				continue;

			_fruit.transform.position = new Vector3 ((float)xSpawnedPosition - (_background.xNumberOfTiles / 2), (float)ySpawnedPosition - (_background.yNumberOfTiles / 2), 0f);

			spawned = true;
		}

	}


	public class Matrix
	{
		public bool[,] tab;
		public int R;
		public int C;
	}

	public void SetUpTiles()
	{
		Tiles.tab = new bool[_background.xNumberOfTiles + 1, _background.yNumberOfTiles + 1];
		Tiles.R = _background.xNumberOfTiles;
		Tiles.C = _background.yNumberOfTiles;
	}
}
