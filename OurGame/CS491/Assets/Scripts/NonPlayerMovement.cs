using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerMovement : MonoBehaviour {
    int currentDir = 2;
    public bool isMoving;
    public float walkSpeed;
    Vector2 input;
    Animator animator;
    float moveVelocityX;
    float moveVelocityY;
    public bool canMove;
    private int rndInt;

    public float timeBetweenWalks;
    public float timeIdle;
    private bool timesUp;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        timesUp = true;
    }
	// Update is called once per frame
	void Update () {
        // Time Update
        if (timesUp)
        {
            currentDir = Random.Range(0, 4);
            timesUp = false;
            StartCoroutine("timeUntilNewDirection", timeBetweenWalks);
            canMove = false;
            
            StartCoroutine("CantMoveForTime", timeIdle);
        }
        isMoving = false;
        moveVelocityX = 0;
        moveVelocityY = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY);
        if (canMove)
        {
            isMoving = true;
            if (currentDir == 0)
            {
                moveVelocityY = walkSpeed * 3;
            }
            if (currentDir == 2)
            {
                moveVelocityY = -walkSpeed * 3;
                
            }

            if (currentDir == 3)
            {
                moveVelocityX = -walkSpeed * 4;
                
            }

            if (currentDir == 1)
            {
                moveVelocityX = walkSpeed * 4;
                
            }
           
            animator.SetInteger("WalkDirection", currentDir);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;
        }
        animator.SetBool("isWalking", isMoving);
    }

    public IEnumerator timeUntilNewDirection(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timesUp = true;
    }

    public IEnumerator CantMoveForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }
}
