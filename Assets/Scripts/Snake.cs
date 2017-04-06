using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snake : MonoBehaviour {


	[Range(2, 5)]
	public int sizeOfSnakeAtStart;

	[Header("Snake Parts")]
	public List<GameObject> Parts = new List<GameObject> ();

	public bool CollisionWithSnake { get; private set; }
	public bool CollisionWithWall { get; private set; }
	public bool CollisionWithFruit { get; private set; }

	public int Score { get; private set; }

	private SpriteRenderer _spriteRenderer;

	public Vector3 tempPos;

	private Vector3 LastPositionOfTail;

	private Sprite _turnSprite;
	private Sprite _bodySprite;
	private Sprite _tailSprite;
	private Sprite _headWithBloodSprite;


	public void appendToSnake()			//function that instaniate clones of Body						// at Start of game?
	{
		GameObject part = Instantiate (Resources.Load<GameObject> ("Prefabs/Body")) as GameObject;
		if (!Parts.Contains (part)) {
			Parts.Add (part);
		}
	}

	void LateUpdate()
	{
		//CollisionWithFruit = false;
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

		_bodySprite = Resources.Load<Sprite> ("Graphics/Body") as Sprite;
		_turnSprite = Resources.Load<Sprite> ("Graphics/Turn") as Sprite;
		_tailSprite = Resources.Load<Sprite> ("Graphics/Tail") as Sprite;
		_headWithBloodSprite = Resources.Load<Sprite> ("Graphics/HeadWithBlood") as Sprite;
		Parts [Parts.Count - 1].GetComponent <SpriteRenderer> ().sprite = _tailSprite;
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




	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Fruit")) {
			Score++;
			appendToSnake ();
			Parts [Parts.Count - 1].transform.position = LastPositionOfTail;
			Parts [Parts.Count - 1].transform.parent = transform.parent;
			Parts [Parts.Count - 1].GetComponent <SpriteRenderer> ().sprite = _tailSprite;
		} else if (other.gameObject.CompareTag ("Snek")) {
			_spriteRenderer.sprite = _headWithBloodSprite;
			CollisionWithSnake = true;
		} else if (other.gameObject.CompareTag ("Wall")) {
			_spriteRenderer.sprite = _headWithBloodSprite;
			CollisionWithWall = true;
		}
			
	}




	public void PaintSpriteTurn(int ID, Vector3 localEulerAngles)
	{
		Parts [ID].GetComponent <SpriteRenderer> ().sprite = _turnSprite;
		Parts [ID].transform.localEulerAngles = localEulerAngles;
	}

	public void PaintSpriteBody(int ID, Vector3 localEulerAngles)
	{
		Parts [ID].GetComponent <SpriteRenderer> ().sprite = _bodySprite;
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
