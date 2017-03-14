using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnRails : MonoBehaviour
{
    float[] walkTimes = { 5f, 2f };
    int[] currentDir = {0, 3};
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
    bool timesUp;
    int i;

    // Use this for initialization
    void Start()
    {
        i = 0;
        animator = GetComponent<Animator>();
        timesUp = true;
        canMove = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(i == walkTimes.Length)
        {
            timesUp = false;
            canMove = false;
        }
        // Time Update
            if (timesUp)
            {
                timesUp = false;
                StartCoroutine("timeUntilNewDirection", walkTimes[i]); 
            }
            isMoving = false;
            moveVelocityX = 0;
            moveVelocityY = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY);
            if (canMove)
            {
                isMoving = true;
                if (currentDir[i] == 0)
                {
                    moveVelocityY = walkSpeed * 3;
                }
                if (currentDir[i] == 2)
                {
                    moveVelocityY = -walkSpeed * 3;

                }

                if (currentDir[i] == 3)
                {
                    moveVelocityX = -walkSpeed * 4;

                }

                if (currentDir[i] == 1)
                {
                    moveVelocityX = walkSpeed * 4;

                }

                animator.SetInteger("WalkDirection", currentDir[i]);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;
            }
            animator.SetBool("IsMoving", isMoving);
        
    }

    public IEnumerator timeUntilNewDirection(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        timesUp = true;
        i++;

    }
}

