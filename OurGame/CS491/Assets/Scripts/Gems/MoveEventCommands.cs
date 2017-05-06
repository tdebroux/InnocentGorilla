using System.Collections;
using UnityEngine;

public class MoveEventCommands : MonoBehaviour
{
    // public rail movements - what the ding dong choose
    public int dir0;
    public float x0;
    public float y0;

    public int dir1;
    public float x1;
    public float y1;

    public int dir2;
    public float x2;
    public float y2;

    public int dir3;
    public float x3;
    public float y3;

    public int dir4;
    public float x4;
    public float y4;

    public int dir5;
    public float x5;
    public float y5;

    public int dir6;
    public float x6;
    public float y6;

    public int dir7;
    public float x7;
    public float y7;

    private float[] currentXs;
    private float[] currentYs;
    private int[] currentDir;

    bool reachX1;
    bool reachY1;

    public bool isMoving;
    public float walkSpeed;
    Vector2 input;
    Animator animator;
    float moveVelocityX;
    float moveVelocityY;
    public bool canMove;
    private int rndInt;
    bool timesUp;
    int i;
    public TextBoxManager tBoxM;

    // Use this for initialization
    void Start()
    {
        currentXs = new float[8] { x0, x1, x2, x3, x4, x5, x6, x7 };
        currentYs = new float[8] { y0, y1, y2, y3, y4, y5, y6, y7 };
        currentDir = new int[8] { dir0, dir1, dir2, dir3, dir4, dir5, dir6, dir7 };
        i = 0;
        animator = GetComponent<Animator>();
        canMove = false;
    }
    // Update is called once per frame
    void Update()
    {
       // print("name " + tBoxM.name);
        if (tBoxM.isAnEvent)
        {
            print("i: " + i);
            tBoxM.isAnEvent = false; //might be our skip event problems (maybe, tommy don't think so)
                                     // Decide Walk or Turn

            if (currentDir[i] != 0)
            {
                print("direction");
                animator.SetInteger("WalkDirection", currentDir[i]);
                canMove = false;
                moveVelocityX = 0;
                moveVelocityY = 0;
                isMoving = false;
                //transform.Translate(new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime);
                i++;
            }
            else
            { // if it is zero, we're walkin'!
                print("moving");
                canMove = true;
                //Decide X or Y
                if (currentXs[i] != 0 && currentYs[i] != 0)
                {
                    print("ERROR!!!Should not have a value for both x_i &y_i");
                }
                else if (currentXs[i] != 0)
                {
                    // Decide left or right
                    if (transform.position.x <= currentXs[i])
                    {
                        // walks RIGHT until it reaches the X_i position
                        currentDir[i] = 2;
                        animator.SetInteger("WalkDirection", currentDir[i]);

                    }
                    else
                    {
                        currentDir[i] = 4;
                        // walks LEFT until it reaches the X_i position
                        animator.SetInteger("WalkDirection", currentDir[i]);

                    }

                }
                else if (currentYs[i] != 0)
                {
                    // Decide up or down
                    if (transform.position.y <= currentYs[i])
                    {
                        // walks UP until it reaches the Y_i position
                        currentDir[i] = 1;
                        animator.SetInteger("WalkDirection", currentDir[i]);

                    }
                    else
                    {
                        currentDir[i] = 3;
                        // walks DOWN until it reaches the Y_i position
                        animator.SetInteger("WalkDirection", currentDir[i]);

                    }
                }
            }
        }
        // The actual movement
        if (canMove)
        {
            isMoving = true;
            if (currentDir[i] == 1)
            {
                moveVelocityY = walkSpeed * 3;
                if (transform.position.y > currentYs[i])
                {
                    isMoving = false;
                    canMove = false;
                    moveVelocityX = 0;
                    moveVelocityY = 0;
                    i++;
                    print("going down destination reached");
                    if (tBoxM.checkEvent(tBoxM.currentLine))
                    {
                        tBoxM.isAnEvent = true;
                    }
                }
                transform.Translate(new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime);
            }
            else if (currentDir[i] == 3)
            {
                moveVelocityY = -walkSpeed * 3;
                if (transform.position.y < currentYs[i])
                {
                    canMove = false;
                    isMoving = false;
                    moveVelocityX = 0;
                    moveVelocityY = 0;
                    i++;
                    if (tBoxM.checkEvent(tBoxM.currentLine))
                    {
                        tBoxM.isAnEvent = true;
                    }
                }
                transform.Translate(new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime);
            }

            else if (currentDir[i] == 2)
            {
                moveVelocityX = walkSpeed * 4;
                if (transform.position.x > currentXs[i])
                {
                    canMove = false;
                    isMoving = false;
                    moveVelocityX = 0;
                    moveVelocityY = 0;
                    i++;
                    if (tBoxM.checkEvent(tBoxM.currentLine))
                    {
                        tBoxM.isAnEvent = true;
                    }
                }
                transform.Translate(new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime);
            }

            else if (currentDir[i] == 4)
            {
                moveVelocityX = -walkSpeed * 4;
                if (transform.position.x < currentXs[i])
                {
                    canMove = false;
                    isMoving = false;
                    moveVelocityX = 0;
                    moveVelocityY = 0;
                    i++;
                    if (tBoxM.checkEvent(tBoxM.currentLine))
                    {
                        tBoxM.isAnEvent = true;
                    }
                }
                transform.Translate(new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime);
            }
        }
        animator.SetBool("isWalking", isMoving);
    }

    public IEnumerator CantMoveForTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }
}
