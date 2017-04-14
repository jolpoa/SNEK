using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour {

	public Sprite BodySprite;
	public Sprite TurnSprite;
	public Sprite TailSprite;
	public Sprite HeadSprite;
	public Sprite HeadWithBloodSprite;
	public Sprite HeadWithFireSprite;
	public Sprite HeadWithEggSprite;

	void Awake()
	{
		BodySprite = Resources.Load<Sprite> ("Graphics/Body") as Sprite;
		TurnSprite = Resources.Load<Sprite> ("Graphics/Turn") as Sprite;
		TailSprite = Resources.Load<Sprite> ("Graphics/Tail") as Sprite;
		HeadSprite = Resources.Load<Sprite> ("Graphics/Head") as Sprite;
		HeadWithBloodSprite = Resources.Load<Sprite> ("Graphics/HeadWithBlood") as Sprite;
		HeadWithFireSprite = Resources.Load<Sprite> ("Graphics/HeadWithFire") as Sprite;
		HeadWithEggSprite = Resources.Load<Sprite> ("Graphics/HeadWithEgg") as Sprite;
	}
}
