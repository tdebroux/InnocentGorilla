using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TeleportMechanic : MonoBehaviour {
    public float x;
    public float y;
    //float otherX;
    //float otherY;
    ScreenFader fadeScr;
    private Vector2 TeleportPosition;
    public Movement player;
    Collider2D character;
    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Movement>();
        TeleportPosition = new Vector2(x, y);
        fadeScr = GameObject.FindObjectOfType<ScreenFader>();
    }
	
	// Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        print("X " + x + " Y " + y);
        //otherX = x;
       // otherY = y;
        //TeleportPosition = new Vector2(x, y);
        if (other.name == "MainCharacter")
        {
            player.canMove = false;
            fadeScr.StartScene();
            //other.transform.position = TeleportPosition;
            //fadeScr.EndScene();
            player.canMove = true;
        }
    }
    public void teleport()
    {
        print("X " + x + " Y " + y);
        player.transform.position = TeleportPosition;
    }
    void Awake()
    {
        
    }
}
