using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

	InputField input;
	InputField.SubmitEvent se;
	public Text output;
	private int lineCount = 0;
	public Text correct;

	void Start () {
		input = gameObject.GetComponent<InputField> ();
		se = new InputField.SubmitEvent ();
		se.AddListener (SubmitInput);
		input.onEndEdit = se;

	}
	private void SubmitInput(string arg0){
		if (lineCount < 4) {
			output.text += "\n" + arg0;
			input.text = "";
			lineCount++;
			if(lineCount == 4){
				if (output.text.Trim().Equals (correct.text.Trim())) {
					print ("Text matches.");
					output.text = "\nJohn Smith \n18 \nMilwaukee, WI\n414-638-6382\n\n" + "Do these records match?";
				} else {
					print ("Text doesn't match.");
					output.text = output.text + "\n\n" + "Do these records match?";
				}
			}
		} 
		input.ActivateInputField ();
	}

}
