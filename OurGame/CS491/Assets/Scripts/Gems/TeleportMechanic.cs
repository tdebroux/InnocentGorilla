using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TeleportMechanic : MonoBehaviour
{
    public float x;
    public float y;
    ScreenFader fadeScr;
    private Vector2 TeleportPosition;
    Movement player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindObjectOfType<Movement>();
        TeleportPosition = new Vector2(x, y);
        fadeScr = GameObject.FindObjectOfType<ScreenFader>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "MainCharacter")
        {
            //player.canMove = false;
            fadeScr.FadeToBlack();
            other.transform.position = TeleportPosition;
            //player.StartCoroutine("CantMoveForTime", .7f);
        }
    }
    void Awake()
    {

    }
}
