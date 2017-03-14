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


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Movement>();
        animator = GetComponent<Animator>();
        animator.SetInteger("CharacterNumber", characterNum);
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }


        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        //theText.text = textLines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;
                if (checkSwitch())
                {
                    //switch image sprite
                    if (textLines[currentLine].Substring(9).Trim().Equals("Douglas"))
                    {
                        characterNum = 2;
                    }
                    else if (textLines[currentLine].Substring(9).Trim().Equals("Player"))
                    {
                        characterNum = 1;
                    }
                    currentLine++;
                }
                else if (checkGame())
                {
                    //call minigame
                    currentLine++;
                }
                else if (checkLeave())
                {
                    //boss exits
                    currentLine++;
                }
                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
                animator.SetInteger("CharacterNumber", characterNum);
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }
    private IEnumerator TextScroll(string lineofText)
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
    public void EnableTextBox()
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
    }
    public bool checkLeave()
    {
        return textLines[currentLine].Substring(0, 7).Equals("Glad to");
    }
    public bool checkGame()
    {
        return textLines[currentLine].Substring(0, 11).Equals("(startgame)");
    }
    public bool checkSwitch()
    {
        return textLines[currentLine].Substring(0, 8).Equals("(switch)");
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
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));
        }
    }

}
