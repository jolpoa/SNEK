using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SnakeTutorial : MonoBehaviour {

	public List<GameObject> Snake = new List<GameObject> ();

	public TimeController _timeController;

	public int sizeOfSnakeAtStart;


	public string Direction = "right";
	public Vector3 tempPos;

	private Vector3 LastPositionOfTail;

	private Sprite _turnSprite;
	private Sprite _bodySprite;
	private Sprite _tailSprite;

	Object[] _sprites;

	void Start () {
		initSNEK ();
		SpriteLoads ();
	}

	void FixedUpdate()
	{
		SetLastPositionOfTail ();
		Inputs ();
		updateSnake ();
	}

	void Update () {
		ChangingSprites ();
	}

	void appendToSnake()			//function that instaniate clones of Body						// at Start of game?
	{
		GameObject part = Instantiate (Resources.Load<GameObject> ("Prefabs/Body")) as GameObject;
		if (!Snake.Contains (part)) {
			Snake.Add (part);
		}
	}


	string CheckCurve(int prev, int current, int next)				//func to check which direction snake turned (if it does)
	{
		float prevPartX = Snake [prev].transform.position.x;
		float prevPartY = Snake [prev].transform.position.y;

		float nextPartX = Snake [next].transform.position.x;
		float nextPartY = Snake [next].transform.position.y;

		float currentPartX = Snake [current].transform.position.x;
		float currentPartY = Snake [current].transform.position.y;

		if (((prevPartX > currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX > currentPartX))) {
			return "down-to-right";
		} else if (((prevPartX > currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY && nextPartX > currentPartX))) {
			return "up-to-right";
		} else if (((prevPartX < currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX < currentPartX))) {
			return "down-to-left";
		} else if (((prevPartX < currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY) && (nextPartX < currentPartX))) {
			return "up-to-left";
		} else if (prevPartY != currentPartY) {
			return "same-vertical";
		}
		return "same-horizontal";

	}

	void initSNEK(){							//func to add proper number of snake parts
		for (int i = 0; i < sizeOfSnakeAtStart; i++) {
			appendToSnake ();
		}
		for (int j = Snake.Count - 1; j > 0; j--) {
			if (j > 0) {
				Snake [j].transform.parent = transform.parent;
				Snake [j].transform.position = Snake [j - 1].transform.position;
			}
		}
		Snake [0].transform.position = transform.position;

		if (Direction == "up") {
			tempPos.y++;
			transform.localEulerAngles = new Vector3 (0, 0, 90);
		} else if (Direction == "left") {
			tempPos.x--;
			transform.localEulerAngles = new Vector3 (0, 0, 180);
		} else if (Direction == "down") {
			tempPos.y--;
			transform.localEulerAngles = new Vector3 (0, 0, 270);
		} else if (Direction == "right") {
			tempPos.x++;
			transform.localEulerAngles = new Vector3 (0, 0, 0);
		}

		transform.position = tempPos;
	}

	void Inputs()		//func that interpret input to direction of snake head
	{

		if (Input.GetAxis ("Vertical") > 0) {
			Direction = "up";
		}
		if (Input.GetAxis ("Vertical") < 0) {
			Direction = "down";
		}
		if (Input.GetAxis ("Horizontal") < 0) {
			Direction = "left";
		}
		if (Input.GetAxis ("Horizontal") > 0) {
			Direction = "right";
		}

	}


	void SpriteLoads()		//lol, just like in name, it is loading sprites from resources
	{
		_sprites = Resources.LoadAll ("Graphics/SnakeTiles");
		_bodySprite = (Sprite)_sprites[2];
		_turnSprite = (Sprite)_sprites[3];
		_tailSprite = (Sprite)_sprites[4];
		Snake [Snake.Count - 1].GetComponent <SpriteRenderer> ().sprite = _tailSprite;
	}

	void SetLastPositionOfTail()
	{
		LastPositionOfTail = Snake [Snake.Count - 1].transform.position;
	}
		


	void updateSnake()		//function that move our lovely animal, also should change sprites of bodyparts
	{

		if (_timeController.ItIsTime()) {

			for (int j = Snake.Count - 1; j > 0; j--) {
				if (j > 0) {
					Snake [j].transform.position = Snake [j - 1].transform.position;
				}
			}


			Snake [0].transform.position = transform.position;


				if (Direction == "up") {
					tempPos.y++;
					transform.localEulerAngles = new Vector3 (0, 0, 90);
				} else if (Direction == "left") {
					tempPos.x--;
					transform.localEulerAngles = new Vector3 (0, 0, 180);
				} else if (Direction == "down") {
					tempPos.y--;
					transform.localEulerAngles = new Vector3 (0, 0, 270);
				} else if (Direction == "right") {
					tempPos.x++;
					transform.localEulerAngles = new Vector3 (0, 0, 0);
				}
			}

		transform.position = tempPos;
	}

	void ChangingSprites(){			//messy func to set right sprites to right parts of snek :))

		string curve = "";

		for (int j = Snake.Count - 1; j > 0; j--) {
			if (j > 0) {
				if (j < Snake.Count - 1) {
					curve = CheckCurve (j - 1, j, j + 1);
					if (curve == "same-vertical") {
						PaintSpriteBody (j, new Vector3 (0, 0, 90));
					} 
					else if (curve == "same-horizontal") {
						PaintSpriteBody (j, new Vector3 (0, 0, 0));
					}
					else if (curve == "up-to-left") {
						PaintSpriteTurn (j, new Vector3 (0, 0, 180));
					} 
					else if (curve == "up-to-right") {
						PaintSpriteTurn (j, new Vector3 (0, 0, 90));
					} 
					else if (curve == "down-to-right") {
						PaintSpriteTurn (j, new Vector3 (0, 0, 0));
					} 
					else if (curve == "down-to-left") {
						PaintSpriteTurn (j, new Vector3 (0, 0, 270));
					}
				}
			}
		}

		float prevPartX = transform.position.x;
		float prevPartY = transform.position.y;

		float nextPartX = Snake [1].transform.position.x;
		float nextPartY = Snake [1].transform.position.y;

		float currentPartX = Snake [0].transform.position.x;
		float currentPartY = Snake [0].transform.position.y;

		if (((prevPartX > currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX > currentPartX))) {
			PaintSpriteTurn (0, new Vector3 (0, 0, 0));
		} else if (((prevPartX > currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY && nextPartX > currentPartX))) {
			PaintSpriteTurn (0, new Vector3 (0, 0, 90));
		} else if (((prevPartX < currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX < currentPartX))) {
			PaintSpriteTurn (0, new Vector3 (0, 0, 270));
		} else if (((prevPartX < currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY) && (nextPartX < currentPartX))) {
			PaintSpriteTurn (0, new Vector3 (0, 0, 180));
		} else if (prevPartY != currentPartY) {
			PaintSpriteBody (0, new Vector3 (0, 0, 90));
		} else {
			PaintSpriteBody (0, new Vector3 (0, 0, 0));
		}
		RotateTail ();
	}




	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Fruit")) {
			Destroy (other.gameObject);
			appendToSnake ();
			Snake [Snake.Count - 1].transform.position = LastPositionOfTail;
			Snake [Snake.Count - 1].GetComponent <SpriteRenderer> ().sprite = _tailSprite;
		}
		else if (other.gameObject.CompareTag ("Snek")) {
			_timeController.FreezeTime ();
		}
			
	}




	void PaintSpriteTurn(int ID, Vector3 localEulerAngles)
	{
		Snake [ID].GetComponent <SpriteRenderer> ().sprite = _turnSprite;
		Snake [ID].transform.localEulerAngles = localEulerAngles;
	}

	void PaintSpriteBody(int ID, Vector3 localEulerAngles)
	{
		Snake [ID].GetComponent <SpriteRenderer> ().sprite = _bodySprite;
		Snake [ID].transform.localEulerAngles = localEulerAngles;
	}


	void RotateTail()
	{
		float tailY = Snake [Snake.Count - 1].transform.position.y;
		float tailX = Snake [Snake.Count - 1].transform.position.x;

		float nextToTailX = Snake [Snake.Count - 2].transform.position.x;
		float nextToTailY = Snake [Snake.Count - 2].transform.position.y;


		if (nextToTailY > tailY)
			Snake [Snake.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 90);
		if (nextToTailY < tailY)
			Snake [Snake.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 270);
		if (nextToTailX > tailX)
			Snake [Snake.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 0);
		if (nextToTailX < tailX)
			Snake [Snake.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 180);
	}


}
