using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalGameLaunch : MonoBehaviour
{
    public bool requireButtonPress;
    private bool waitForPress;
    public bool destroyWhenActivated;
    Movement player;
    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Movement>();
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

            player.canMove = true;
            print("YO");
        }
    }
}
