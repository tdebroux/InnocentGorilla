﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
	public void NewGameButton(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}
	public void ExitGame(){
		Application.Quit ();
	}
}
