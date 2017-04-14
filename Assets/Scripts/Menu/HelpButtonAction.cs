using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonAction : MonoBehaviour {

	public RectTransform Help;

	public RectTransform PlayButton;
	public RectTransform HelpButton;
	public RectTransform CreditsButton;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
			HideHelp ();
	}

	public void ShowHelp()
	{
		Help.gameObject.SetActive (true);
	}

	public void HideHelp()
	{
		Help.gameObject.SetActive (false);
	}
}
