using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerBoundries : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D npcCollider)
    {
        NonPlayerMovement npc = npcCollider.GetComponent<NonPlayerMovement>();
        if (npc != null)
        {
            npc.GetComponent<NonPlayerMovement>();
            npc.outOfBounds = true;
        }
    }
}
