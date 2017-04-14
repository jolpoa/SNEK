using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour {
	[Range (1 , 8)]
	public float ScalingSpeed;

	[Range (0 , 1)]
	public float ScalingSize;

	public FruitSpawner _fruitSpawner;

	private AudioSource Munch;

	private float pomocnicza;

	void Awake()
	{
		Munch = GetComponent<AudioSource> ();
	}

	void FixedUpdate () {
		pomocnicza = Mathf.Cos (Time.timeSinceLevelLoad * ScalingSpeed);

		transform.localScale += new Vector3 (pomocnicza, pomocnicza, 0) * ScalingSize;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Snek")) {
			Munch.Play ();
			_fruitSpawner.SpawnFruit ();
		}

	}
}

