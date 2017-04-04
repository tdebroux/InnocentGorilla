using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerMovement : MonoBehaviour
{
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
    public float timeTilChangeDir;
    private bool timesUpWalk;
    private bool timesUpIdle;

    public bool outOfBounds;
    Vector3 currPosition;
    Vector3 oldPosition;
    bool isHitting;
    bool SaveAVectorTimesUp;
    bool isHittingTimesUp;
    bool changeDirTimesUp;
    TextBoxManager eventcommander;
    Transform weber;
    // Use this for initialization
    void Start()
    {
        changeDirTimesUp = true;
        isHittingTimesUp = true;
        SaveAVectorTimesUp = true;
        weber = GameObject.FindGameObjectWithTag("Weber").transform;
        animator = GetComponent<Animator>();
        timesUpWalk = false;
        timesUpIdle = true;
        eventcommander = FindObjectOfType<TextBoxManager>();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        //save a vector
        if (SaveAVectorTimesUp)
        {
            StartCoroutine("saveAVector", 0.29);
            SaveAVectorTimesUp = false;
        }

        //check the pos and oldpos are the same
        if (isMoving && canMove)
        {
            if (isHittingTimesUp)
            {
                StartCoroutine("checkIsHitting", .18f);
                isHittingTimesUp = false;
            }
        }
        */

        if (!eventcommander.isAnEvent)
        {

            // outta the play pen OR hitting a wall
            if (outOfBounds)
            {
                print(this.name);
                StartCoroutine("WalkOtherDirectonForSeconds", .7f);
            }
            else
            // actual movement
            if (timesUpWalk) //hands off to Idle
            {
                timesUpWalk = false;
                StartCoroutine("IdleForTime", timeIdle);
            }

            if (timesUpIdle) //hands off to Walk
            {
                StartCoroutine("WalkForTime", timeBetweenWalks);
                timesUpIdle = false;
            }

            if (changeDirTimesUp) //hands off self
            {

                StartCoroutine("changeDirInTime", timeTilChangeDir);
                changeDirTimesUp = false;
            }

            //reset isHitting
            //isHitting = false;



            isMoving = false;
            moveVelocityX = 0;
            moveVelocityY = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY);
            if (canMove)
            {
                isMoving = true;
                if (currentDir == 1)
                {
                    moveVelocityY = walkSpeed * 3;
                }
                if (currentDir == 3)
                {
                    moveVelocityY = -walkSpeed * 3;

                }

                if (currentDir == 4)
                {
                    moveVelocityX = -walkSpeed * 4;

                }

                if (currentDir == 2)
                {
                    moveVelocityX = walkSpeed * 4;

                }

                animator.SetInteger("WalkDirection", currentDir);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;
            }
            animator.SetInteger("WalkDirection", currentDir);
            animator.SetBool("isWalking", isMoving);

            // update position
            //currPosition = weber.position;
        }
    }

    public IEnumerator WalkForTime(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        timesUpWalk = true;
        canMove = false;
    }

    public IEnumerator IdleForTime(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        timesUpIdle = true;
        canMove = true;
    }

    public IEnumerator changeDirInTime(float seconds)
    {
        //print("changeDir");
        yield return new WaitForSeconds(seconds);
        currentDir = Random.Range(1, 5);
        changeDirTimesUp = true;
    }

    public IEnumerator saveAVector(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        print("saveOldPos");
        oldPosition = weber.position;
        SaveAVectorTimesUp = true;

    }

    public IEnumerator checkIsHitting(float seconds)
    {

        yield return new WaitForSeconds(seconds);
        print("checkIsHitting");
        isHitting = HittingTheWall();
        isHittingTimesUp = true;

    }

    public IEnumerator WalkOtherDirectonForSeconds(float seconds)
    {
        if (currentDir == 1)
        {
            moveVelocityY = -walkSpeed * 3;
            currentDir = 3;
        }
        else if (currentDir == 3)
        {
            moveVelocityY = walkSpeed * 3;
            currentDir = 1;

        }

        else if (currentDir == 4)
        {
            moveVelocityX = walkSpeed * 4;
            currentDir = 2;
        }

        else if (currentDir == 2)
        {
            moveVelocityX = -walkSpeed * 4;
            currentDir = 4;
        }
        animator.SetInteger("WalkDirection", currentDir);
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;

        outOfBounds = false;
        yield return new WaitForSeconds(seconds);

    }

    public bool HittingTheWall()
    {
        if (currPosition == oldPosition)
        {
            print(" H I T T I N G");
        }
        return currPosition == oldPosition;
    }

}
