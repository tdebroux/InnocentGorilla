using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerBoundries : MonoBehaviour {
    NonPlayerMovement bot;
	// Use this for initialization
	void Start () {
        bot = GameObject.FindObjectOfType<NonPlayerMovement>();
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D botCollider)
    {
        print("out");
        if (bot.name != GameObject.FindGameObjectWithTag("Player").name)
        {
            bot.outOfBounds = true;
        }
    }   
}
