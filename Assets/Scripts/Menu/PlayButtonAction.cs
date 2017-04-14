using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayButtonAction : MonoBehaviour {

	public void StartGame()
	{
		SceneManager.LoadScene ("Snake game");
	}
}
