using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour {

	private SpriteRenderer _spriteRenderer;

	void Update()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
		_spriteRenderer.sprite = Resources.Load<Sprite> ("Graphics/HeadWithBlood") as Sprite;
	}
	/*
	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer> ();
	}


	void Update () {
		if (Time.time >= 3.0f)
			_spriteRenderer.sprite = Resources.Load<Sprite> ("Graphics/HeadWithBlood") as Sprite;
	}*/
}
