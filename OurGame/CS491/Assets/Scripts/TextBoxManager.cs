using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public Movement player;

    public bool isActive;

    public bool stopPlayerMovement;

    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    Animator animator;
    public int characterNum;

    public bool isAnEvent = false;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Movement>(); // to freeze player
        animator = GetComponent<Animator>(); // to switch sprite heads
        animator.SetInteger("CharacterNumber", characterNum);
        endAtLine = textLines.Length - 1;

        DisableTextBox();// makes sure it doesn't start appeared on the screen

    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                if (currentLine == textLines.Length - 1)
                {
                    DisableTextBox();
                    return;
                }
                currentLine += 1;

                if (checkEvent())
                {
                    isAnEvent = true;
                    currentLine += 1;
                }
                else if (checkSwitch())
                {
                    int space = textLines[currentLine].IndexOf(" ");
                    int len = textLines[currentLine].Length;
                    string person = textLines[currentLine].Substring(space + 1);
                    setCharacterNumber(person);
                    animator.SetInteger("CharacterNumber", characterNum);
                    currentLine += 1;
                }

                if (currentLine == textLines.Length - 1)
                {
                    DisableTextBox();
                    return;
                }
                StartCoroutine(TextScroll(textLines[currentLine]));

            }

            else if (!cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }
    private IEnumerator TextScroll(string lineofText) // from the web DO NOT MESS WITH
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineofText.Length - 1))
        {
            theText.text += lineofText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineofText;
        isTyping = false;
        cancelTyping = false;
    }
    public void EnableTextBox() // from the web DO NOT MESS WITH
    {

        textBox.SetActive(true);
        isActive = true;
        if (stopPlayerMovement)
        {
            player.canMove = false;
        }
        if (currentLine < textLines.Length)
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
        }
        else
        {
            DisableTextBox();
        }

    }

    public bool checkSwitch()
    {
        if (textLines[currentLine].Contains(" "))
        {
            int space = textLines[currentLine].IndexOf(" ");
            return textLines[currentLine].Substring(0, space).Equals("(switch)");
        }
        return false;
    }

    public bool checkEvent()
    {
        return textLines[currentLine].Trim().Equals("(event)");
    }

    public void setCharacterNumber(string line)
    {
        if (line.Equals("Douglas"))
        {
            characterNum = 2;
        }
        else if (line.Equals("Player"))
        {
            characterNum = 1;
        }
        //add more characters
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;
        player.canMove = true;

    }
    public void ReloadScript(TextAsset theText)
    {
        if (theText != null)
        {
            //textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
        for (int i = 0; i < textLines.Length; i++)
        {
            textLines[i].Trim();
        }
    }

}
