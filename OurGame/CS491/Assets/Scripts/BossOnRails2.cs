using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOnRails2 : MonoBehaviour
{
    int[] currentDir = { 0, 3, 2 };
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
    TextBoxManager tBoxM; 

    // Use this for initialization
    void Start()
    {
        tBoxM = FindObjectOfType<TextBoxManager>();
        i = 0;
        animator = GetComponent<Animator>();
        canMove = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (i == 0 && tBoxM.checkEnter())
        {
            canMove = true;
            if (transform.position.y > 22.83)
            {
                print("Stop walking");
                i++;
            }
        }

        if (i == 1)
        {
            animator.SetInteger("WalkDirection", currentDir[i]);
            canMove = false;
            if (tBoxM.checkLeave())
            {
                i++;
            }
        }

        if (i == 2)
        {
            canMove = true;
            if (transform.position.y < 0)
            {
                i++;
            }
        }

        if (i == 3)
        {
            canMove = false;
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
}
