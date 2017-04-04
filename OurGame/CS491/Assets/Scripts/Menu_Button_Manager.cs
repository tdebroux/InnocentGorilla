using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu_Button_Manager : MonoBehaviour {
	public void NewGameBtn(string newGameLevel){
		SceneManager.LoadScene (newGameLevel);
	}
}
