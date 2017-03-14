using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {
	InputField input;
	InputField.SubmitEvent se;
	public Text output;
	private int lineCount = 0;
	public Text correct;
    public bool gameOver;

    void Start()
    {
        gameOver = false;
        input = gameObject.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.onEndEdit = se;
    }
	public void activateGame()
    {
		
	}
	private void SubmitInput(string arg0)
    {
		if (lineCount < 8) {
			output.text += "\n" + arg0;
			input.text = "";
			lineCount++;
			if(lineCount == 8){
				if (output.text.Trim().Equals (correct.text.Trim())) {
					output.text = "\nFrank Dean \n35 \n430b Union Terr.\nLakeville, MN 55044\n952-639-6382\ndeanFra332@sbcglobal.net\n$520,500.00\n105%\n" + "Do these records match?";
                } else {
					output.text = output.text + "\n\n" + "Do these records match?";
				}
                StartCoroutine("CantContinueForTime", 7f);

			}
		} 
		input.ActivateInputField ();
	}
    public IEnumerator CantContinueForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameOver = true;
        input.enabled = false;
    }
}
