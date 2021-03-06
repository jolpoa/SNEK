using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snake : MonoBehaviour {


	[Range(2, 5)]
	public int sizeOfSnakeAtStart;

	[Header("Snake Parts")]
	public List<GameObject> Parts = new List<GameObject> ();

	public int Score { get; private set; }

	[HideInInspector]
	public Vector3 tempPos;

	public ScoreManager _scoreManager;

	public SpriteManager _spriteManager;

	public GameStatusManager _gameStatusManager;

	private SpriteRenderer _spriteRenderer;

	private AudioSource _deadSound;

	private Vector3 LastPositionOfTail;

	public int deaths;

	void Awake()
	{
		_deadSound = GetComponent<AudioSource> ();
	}


	public void appendToSnake()			//function that instaniate clones of Body						// at Start of game?
	{
		GameObject part = Instantiate (Resources.Load<GameObject> ("Prefabs/Body"), transform.parent) as GameObject;
		if (!Parts.Contains (part)) {
			Parts.Add (part);
		}
	}


	public string CheckCurve(int prev, int current, int next)				//func to check which direction snake turned (if it does)
	{
		float prevPartX = Parts [prev].transform.position.x;
		float prevPartY = Parts [prev].transform.position.y;

		float nextPartX = Parts [next].transform.position.x;
		float nextPartY = Parts [next].transform.position.y;

		float currentPartX = Parts [current].transform.position.x;
		float currentPartY = Parts [current].transform.position.y;

		if (((prevPartX > currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX > currentPartX))) {
			return SnakeDirection.DownToRight;
		} else if (((prevPartX > currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY && nextPartX > currentPartX))) {
			return SnakeDirection.UpToRight;
		} else if (((prevPartX < currentPartX) && (nextPartY < currentPartY)) || ((prevPartY < currentPartY) && (nextPartX < currentPartX))) {
			return SnakeDirection.DownToLeft;
		} else if (((prevPartX < currentPartX) && (nextPartY > currentPartY)) || ((prevPartY > currentPartY) && (nextPartX < currentPartX))) {
			return SnakeDirection.UpToLeft;
		} else if (prevPartY != currentPartY) {
			return SnakeDirection.SameVertical;
		}
		return SnakeDirection.SameHorizontal;

	}


	public void SpriteLoads()		//lol, just like in name, it is loading sprites from resources
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();

		Parts [Parts.Count - 1].GetComponent <SpriteRenderer> ().sprite = _spriteManager.TailSprite;
	}

	public void SetLastPositionOfTail()
	{
		LastPositionOfTail = Parts [Parts.Count - 1].transform.position;
	}


	public void ChangingSprites(){			//messy func to set right sprites to right parts of snek :))

		string curve;

		for (int j = Parts.Count - 1; j > 0; j--) {
			if (j > 0) {
				if (j < Parts.Count - 1) {
					curve = CheckCurve (j - 1, j, j + 1);
					if (curve == SnakeDirection.SameVertical) {
						PaintSpriteBody (j, new Vector3 (0, 0, 90));
					} 
					else if (curve == SnakeDirection.SameHorizontal) {
						PaintSpriteBody (j, new Vector3 (0, 0, 0));
					}
					else if (curve == SnakeDirection.UpToLeft) {
						PaintSpriteTurn (j, new Vector3 (0, 0, 180));
					} 
					else if (curve == SnakeDirection.UpToRight) {
						PaintSpriteTurn (j, new Vector3 (0, 0, 90));
					} 
					else if (curve == SnakeDirection.DownToRight) {
						PaintSpriteTurn (j, new Vector3 (0, 0, 0));
					} 
					else if (curve == SnakeDirection.DownToLeft) {
						PaintSpriteTurn (j, new Vector3 (0, 0, 270));
					}
				}
			}
		}

		float prevPartX = transform.position.x;
		float prevPartY = transform.position.y;

		float nextPartX = Parts [1].transform.position.x;
		float nextPartY = Parts [1].transform.position.y;

		float currentPartX = Parts [0].transform.position.x;
		float currentPartY = Parts [0].transform.position.y;

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

	public void ResetSnake()
	{
		DeleteParts ();
		ResetBodyTransformsToZero ();
		transform.position = new Vector3 (0, 0, 0);
		_spriteRenderer.sprite = _spriteManager.HeadSprite;
		tempPos = new Vector3 (0, 0, 0);
	}

	public void ResetDeaths()
	{
		deaths = 0;
	}

	void DeleteParts()
	{
		for (int i = Parts.Count - 1; i >= sizeOfSnakeAtStart; i--) {
			Destroy (Parts [i]);
			Parts.RemoveAt (i);
		}
		Parts [Parts.Count - 1].GetComponent <SpriteRenderer> ().sprite = _spriteManager.TailSprite;
	}

	void ResetBodyTransformsToZero()
	{
		for (int i = 0; i < Parts.Count; i++)
		{
			Parts [i].transform.position = new Vector3 (0, 0);
			Parts [i].transform.localEulerAngles = new Vector3 (0, 0, 0);
		}

	}



	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Fruit")) {
			_scoreManager.Score++;
			appendToSnake ();
			Parts [Parts.Count - 1].transform.position = LastPositionOfTail;
			Parts [Parts.Count - 1].GetComponent <SpriteRenderer> ().sprite = _spriteManager.TailSprite;
		} else if (other.gameObject.CompareTag ("Snek")) {
			deaths++;
			_deadSound.Play ();
			_spriteRenderer.sprite = _spriteManager.HeadWithBloodSprite;
			_gameStatusManager.gameStatus.CurrentStatus = GameStatus.Died;
		} else if (other.gameObject.CompareTag ("Wall")) {
			deaths++;
			_deadSound.Play ();
			_spriteRenderer.sprite = _spriteManager.HeadWithBloodSprite;
			_gameStatusManager.gameStatus.CurrentStatus = GameStatus.Died;
		}
			
	}




	public void PaintSpriteTurn(int ID, Vector3 localEulerAngles)
	{
		Parts [ID].GetComponent <SpriteRenderer> ().sprite = _spriteManager.TurnSprite;
		Parts [ID].transform.localEulerAngles = localEulerAngles;
	}

	public void PaintSpriteBody(int ID, Vector3 localEulerAngles)
	{
		Parts [ID].GetComponent <SpriteRenderer> ().sprite = _spriteManager.BodySprite;
		Parts [ID].transform.localEulerAngles = localEulerAngles;

	}

	public void RotateTail()
	{
		float tailY = Parts [Parts.Count - 1].transform.position.y;
		float tailX = Parts [Parts.Count - 1].transform.position.x;

		float nextToTailX = Parts [Parts.Count - 2].transform.position.x;
		float nextToTailY = Parts [Parts.Count - 2].transform.position.y;


		if (nextToTailY > tailY)
			Parts [Parts.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 90);
		if (nextToTailY < tailY)
			Parts [Parts.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 270);
		if (nextToTailX > tailX)
			Parts [Parts.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 0);
		if (nextToTailX < tailX)
			Parts [Parts.Count - 1].transform.localEulerAngles = new Vector3 (0, 0, 180);
	}

	public void MoveUp()
	{
		tempPos.y++;
		transform.localEulerAngles = new Vector3 (0, 0, 90);
	}

	public void MoveLeft() 
	{
		tempPos.x--;
		transform.localEulerAngles = new Vector3 (0, 0, 180);
	}
	
	public void MoveDown() 
	{
		tempPos.y--;
		transform.localEulerAngles = new Vector3 (0, 0, 270);
	}

	public void MoveRight()
	{
		tempPos.x++;
		transform.localEulerAngles = new Vector3 (0, 0, 0);
	}

	public void ChangePositions()
	{
		for (int j = Parts.Count - 1; j > 0; j--) {
			if (j > 0) {
				Parts [j].transform.position = Parts [j - 1].transform.position;
			}
		}

		Parts [0].transform.position = transform.position; 

		transform.position = tempPos;

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

	public class SnakeDirection
	{
		public static readonly string DownToRight = "Down-to-right";
		public static readonly string DownToLeft = "Down-to-left";
		public static readonly string UpToRight = "Up-to-right";
		public static readonly string UpToLeft = "Up-to-left";
		public static readonly string SameHorizontal = "Same-Horizontal";
		public static readonly string SameVertical = "Same-Vertical";
	}
}
