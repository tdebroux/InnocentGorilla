using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    int currentDir = 2;
    Vector2 input;
    public bool isMoving; 
    float t;
    public float walkSpeed;

    Animator animator; 
    float moveVelocityX;
    float moveVelocityY;
    public bool canMove;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (!canMove)
        {
            return;
        }
        moveVelocityX = 0;
        moveVelocityY = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY);
        isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        animator.SetBool("isMoving", isMoving);

        if (Input.GetKey(KeyCode.W))
        {
            currentDir = 0;
            moveVelocityY = walkSpeed * 3;

        }
        if (Input.GetKey(KeyCode.S))
        {
            currentDir = 2;
            moveVelocityY = -walkSpeed * 3;

        }

        if (Input.GetKey(KeyCode.A))
        {
            currentDir = 3;
            moveVelocityX = -walkSpeed * 4;

        }

        if (Input.GetKey(KeyCode.D))
        {
            currentDir = 1;
            moveVelocityX = walkSpeed * 4;

        }
       
        animator.SetInteger("walkDirectionAnimation", currentDir);
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;



    }
        
}


