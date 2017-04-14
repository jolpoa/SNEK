using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonAction : MonoBehaviour {

	public GameObject CreditsPanel;

	bool areOpened = false;

	void Update(){
		if (areOpened && Input.GetKey(KeyCode.Escape))
			CloseCredits();
	}

	public void OpenCredits()
	{
		CreditsPanel.SetActive (true);
		areOpened = true;
	}

	void CloseCredits()
	{
		CreditsPanel.SetActive (false);

		areOpened = false;
	}
}
