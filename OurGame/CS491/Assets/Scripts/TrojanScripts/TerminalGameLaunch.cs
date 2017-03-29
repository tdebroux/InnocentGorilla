using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalGameLaunch : MonoBehaviour
{
    public bool requireButtonPress;
    private bool waitForPress;
    public bool destroyWhenActivated;
    public Canvas terminal;
    Movement player;
    TextInput inputter;
    bool temp;
    // Use this for initialization
    void Start()
    {
        temp = true;
        inputter = FindObjectOfType<TextInput>();
        player = FindObjectOfType<Movement>();
        terminal = FindObjectOfType<Canvas>();
        terminal.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "MainCharacter")
        {
            if (requireButtonPress)
            {
                waitForPress = true;
                return;
            }

            if (destroyWhenActivated)
            {
                Destroy(gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (waitForPress && Input.GetKeyDown(KeyCode.J))
        {
            //launch minigame
            //check if game is done boolean
            player.canMove = false;
            terminal.enabled = true;
        }
        if (inputter.gameOver && temp)
        {
            player.canMove = true;
            terminal.enabled = false;
            temp = false;
        }
    }
}
