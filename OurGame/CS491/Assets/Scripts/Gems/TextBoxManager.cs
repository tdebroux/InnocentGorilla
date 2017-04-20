using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine = -1;
    public int endAtLine;

    public Movement player;

    public bool isActive;

    public bool stopPlayerMovement;

    private bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;

    Animator animator;
    private int characterNum = 1;
    MoveEventCommands eventObj;
    public bool isAnEvent = false;
    NonPlayerMovement aiMovement;
    public static TextBoxManager S;
    private bool displayText;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Movement>(); // to freeze player
        animator = GetComponent<Animator>(); // to switch sprite heads
        animator.SetInteger("CharacterNumber", characterNum);
        eventObj = FindObjectOfType<MoveEventCommands>();
        endAtLine = textLines.Length - 1;
        displayText = false;
        DisableTextBox();// makes sure it doesn't start appeared on the screen
        currentLine = -5;
    }
    void Awake()
    {
        S = this;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        // cant progress until event is finished
        if (!eventObj.canMove)
        {
            //do all events (even consecutive)
            if (checkEvent(currentLine))
            {
                isAnEvent = true;
                currentLine += 1;
                return;
            }

            if (checkSceneChange())
            {
                int space = textLines[currentLine].IndexOf(" ");
                int len = textLines[currentLine].Length;
                string scene = textLines[currentLine].Substring(space + 1);
                SceneManager.LoadScene(scene.Trim());
            }
            if (checkStartEvent())
            {
                int space = textLines[currentLine].IndexOf(" ");
                string person = textLines[currentLine].Substring(space + 1);
                //if this person stop their random movement
                person = person.Trim();
                if (person.Equals("Douglas"))
                {
                    aiMovement = GameObject.FindWithTag("Douglas").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Sarah"))
                {
                    aiMovement = GameObject.FindWithTag("Sarah").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Ernie"))
                {
                    aiMovement = GameObject.FindWithTag("Ernie").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Eric"))
                {
                    aiMovement = GameObject.FindWithTag("Eric").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Weber"))
                {
                    aiMovement = GameObject.FindWithTag("Weber").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                currentLine += 1;
                return;
            }

            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!isTyping)
                {

                    if (currentLine == textLines.Length - 1)
                    {
                        if (aiMovement != null)
                        {
                            aiMovement.startMovement();
                            aiMovement = null;
                        }
                        DisableTextBox();
                        return;
                    }
                    if (checkSwitch())
                    {
                        int space = textLines[currentLine].IndexOf(" ");
                        int len = textLines[currentLine].Length;
                        string person = textLines[currentLine].Substring(space + 1);
                        person = person.Trim();
                        setCharacterNumber(person);
                        animator.SetInteger("CharacterNumber", characterNum);
                        currentLine += 1;
                    }
                    if (currentLine != 0)
                    {
                        StartCoroutine(TextScroll(textLines[currentLine]));
                        currentLine += 1;
                    }
                }
                else if (!cancelTyping)
                {
                    cancelTyping = true;
                }
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
            if (checkSwitch())
            {
                int space = textLines[currentLine].IndexOf(" ");
                int len = textLines[currentLine].Length;
                string person = textLines[currentLine].Substring(space + 1);
                person = person.Trim();
                print("Switching! : " + person);
                setCharacterNumber(person);
                animator.SetInteger("CharacterNumber", characterNum);
                currentLine += 1;
            }
            if (checkStartEvent())
            {
                int space = textLines[currentLine].IndexOf(" ");
                string person = textLines[currentLine].Substring(space + 1);
                //if this person stop their random movement
                person = person.Trim();
                if (person.Equals("Douglas"))
                {
                    aiMovement = GameObject.FindWithTag("Douglas").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Sarah"))
                {
                    aiMovement = GameObject.FindWithTag("Sarah").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Ernie"))
                {
                    aiMovement = GameObject.FindWithTag("Ernie").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Eric"))
                {
                    aiMovement = GameObject.FindWithTag("Eric").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                else if (person.Equals("Weber"))
                {
                    aiMovement = GameObject.FindWithTag("Weber").GetComponent<NonPlayerMovement>();
                    aiMovement.stopMovement();
                }
                currentLine += 1;
            }
            //currentLine++;
            StartCoroutine(TextScroll(textLines[currentLine]));
            currentLine++;
        }
        else
        {
            DisableTextBox();
        }

    }
    public bool checkSceneChange()
    {
        return textLines[currentLine].Contains("(change)".Trim());
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

    public bool checkStartEvent()
    {
        return textLines[currentLine].Contains("(startevents)");
    }

    public bool checkEvent(int line)
    {
        return textLines[line].Trim().Equals("(event)");
    }

    public void setCharacterNumber(string line)
    {
        if (line.Equals("Player"))
        {
            characterNum = 8;
        }
        else if (line.Equals("Douglas"))
        {
            characterNum = 2;
        }
        else if (line.Equals("Weber"))
        {
            characterNum = 3;
        }
        else if (line.Equals("Ernie"))
        {
            characterNum = 4;
        }
        else if (line.Equals("Eric"))
        {
            characterNum = 5;
        }
        else if (line.Equals("Sarah"))
        {
            characterNum = 6;
        }
        else if (line.Equals("Tod"))
        {
            characterNum = 7;
        }
        else if (line.Equals("Null"))
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
            textLines = (theText.text.Split('\n'));
        }
        for (int i = 0; i < textLines.Length; i++)
        {
            textLines[i].Trim();
        }
    }
}
