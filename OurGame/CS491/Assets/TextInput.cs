using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input;
	InputField.SubmitEvent se;
	public Text output;
	private int lineCount = 0;
	public Text correct;
    Movement Player;
    public bool gameOver;

	void Start () {
        gameOver = false;
        Player = FindObjectOfType<Movement>();
		input = gameObject.GetComponent<InputField> ();
		se = new InputField.SubmitEvent ();
		se.AddListener (SubmitInput);
		input.onEndEdit = se;
	}
	private void SubmitInput(string arg0){
		if (lineCount < 8) {
			output.text += "\n" + arg0;
			input.text = "";
			lineCount++;
			if(lineCount == 8){
				if (output.text.Trim().Equals (correct.text.Trim())) {
					output.text = "\nJohn Smith \n35 \n420 Marketplace Terr.\nMilwaukee, WI 53226\n414-638-6382\nyeahboi@blazeit.net\n$520,500.00\n10.5%\n" + "Do these records match?";
				} else {
					output.text = output.text + "\n\n" + "Do these records match?";
				}
                if (Input.GetKeyDown(KeyCode.Return)){
                    gameOver = true; 
                }
			}
		} 
		input.ActivateInputField ();
	}

    public bool getGameOver() {
        return gameOver;
    }


}
