using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSoundManager : MonoBehaviour {

	private AudioSource _audioSource;

	private bool played = false;

	[Range (0, 2)]
	public float Dilatation;

	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= Dilatation && !played) {
			_audioSource.Play ();
			played = true;
		}
	}
}