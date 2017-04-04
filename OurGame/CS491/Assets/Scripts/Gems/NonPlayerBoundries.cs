using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerBoundries : MonoBehaviour
{
    NonPlayerMovement bot;
    // Use this for initialization
    void Start()
    {
        bot = GameObject.FindObjectOfType<NonPlayerMovement>();
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D botCollider)
    {
        if (botCollider.GetType().Equals(bot.GetType()))
        {

        }
        if (botCollider.name != "MainCharacter")
        {
            print("out");
            bot.outOfBounds = true;
        }
    }
}
