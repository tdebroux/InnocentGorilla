using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	private void SubmitInput(string arg0) {
		if (lineCount < 8) {
			output.text += "\n" + arg0;
			input.text = "";
			lineCount++;
			if(lineCount == 8){
				if (output.text.Trim().Equals (correct.text.Trim())) {
					output.text = "\nJohn Smith \n35 \n420 Marketplace Terr.\nMilwaukee, WI 53226\n414-638-6382\nyeahboi@blazeit.net\n$520,500.00\n10.5%\n" + "Do these records match?";
                } else {
					output.text = output.text + "\n\n" + "Press enter to submit";
				}
                StartCoroutine(WaitForKeyDown(KeyCode.Return));
                //StartCoroutine("CantContinueForTime", 7f);
			}
		} 
		input.ActivateInputField ();
	}
    IEnumerator WaitForKeyDown(KeyCode keyCode){
        while (!Input.GetKeyUp(keyCode))
            yield return null;
        while (!Input.GetKeyDown(keyCode))
            yield return null;
        //output.text = "yo";
        SceneManager.LoadScene("Menu");
    }
    /*
    public IEnumerator CantContinueForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameOver = true;
        input.enabled = false;
    }
    */
}
