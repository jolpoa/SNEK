using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour {

	public Snake _snake;

	public BackGround _background;

	private Matrix Tiles;

	void Awake () {
		Tiles = new Matrix (_background.xNumberOfTiles, _background.yNumberOfTiles);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		BlankTiles ();
		CheckWhereIsSnake ();
	}

	void Update()
	{
		Debug.Log (Tiles.tab[5][5]);
	}


	private void BlankTiles()
	{
		for (int i = 0; i < Tiles.R; i++) {
			for (int j = 0; j < Tiles.C; j++) {
				Tiles.tab [i] [j] = 0;
			}
		}
	}

	private void CheckWhereIsSnake()
	{
		Tiles.tab [_snake.transform.position.x] [_snake.transform.position.y] = true;

		for (int i = 0; i < _snake.Parts.Count; i++) {
			Tiles.tab [_snake.Parts[i].transform.position.x] [_snake.Parts[i].transform.position.y] = true;
		}
	}

	public class Matrix
	{
		public bool[,] tab;
		public int R;
		public int C;

		public Matrix (int row, int collumn)
		{
			this.R = row;
			this.C = collumn;
			this.tab = new bool[row, collumn];
		}

	}
}
