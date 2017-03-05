using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class TeleportMechanic : MonoBehaviour {
   public float x;
   public float y;
   private Vector2 TeleportPosition;

    // Use this for initialization
    void Start () {
    TeleportPosition = new Vector2(x, y);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "MainCharacter")
        {
            other.transform.position = TeleportPosition;
        }
    }
}
