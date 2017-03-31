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

    private bool isMoving;
    public float walkSpeed;
    Vector2 input;
    Animator animator;
    float moveVelocityX;
    float moveVelocityY;
    private bool canMove;
    private int rndInt;
    bool timesUp;
    int i;
    TextBoxManager tBoxM;

    // Use this for initialization
    void Start()
    {
        currentXs = new float[8] { x0, x1, x2, x3, x4, x5, x6, x7 };
        currentYs = new float[8] { y0, y1, y2, y3, y4, y5, y6, y7 };
        currentDir = new int[8] { dir0, dir1, dir2, dir3, dir4, dir5, dir6, dir7 };

        tBoxM = FindObjectOfType<TextBoxManager>();
        i = 0;
        animator = GetComponent<Animator>();
        canMove = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (tBoxM.isAnEvent)
        {
            print("EVENT");
            tBoxM.isAnEvent = false;
            print("event2");
            // Decide Walk or Turn
            if (currentDir[i] != 0)
            {
                animator.SetInteger("WalkDirection", currentDir[i]);
            }
            else
            { // if it is zero, we're walkin'!
                canMove = true;
                print("setting canMove to true: " + canMove);
                //Decide X or Y
                if (currentXs[i] != 0 && currentYs[i] != 0)
                {
                    print("ERROR!!!Should not have a value for both x_i &y_i");
                }
                else if (currentXs[i] != 0)
                {
                    // Decide left or right
                    if (transform.position.x < currentXs[i])
                    {
                        // walks RIGHT until it reaches the X_i position
                        currentDir[i] = 2;
                        print("WalkingRIGHT");
                        if (transform.position.x >= currentXs[i])
                        {
                            print("STOP");
                            canMove = false;
                            i++;
                        }
                    }
                    else
                    {
                        currentDir[i] = 4;
                        // walks LEFT until it reaches the X_i position
                        if (transform.position.x <= currentXs[i])
                        {
                            canMove = false;
                            i++;
                        }
                    }

                }
                else if (currentYs[i] != 0)
                {
                    // Decide up or down
                    if (transform.position.y < currentYs[i])
                    {
                        // walks UP until it reaches the Y_i position
                        currentDir[i] = 1;
                        if (transform.position.y >= currentYs[i])
                        {
                            canMove = false;
                            i++;
                        }
                    }
                    else
                    {
                        currentDir[i] = 3;
                        // walks DOWN until it reaches the Y_i position
                        if (transform.position.y <= currentYs[i])
                        {
                            canMove = false;
                            i++;
                        }
                    }
                }

            }

            print("Done in big if");
            print("canmove: " + canMove);
            // The actual movement
            isMoving = false;
            moveVelocityX = 0;
            moveVelocityY = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY);
            if (canMove)
            {
                print("incanmove");
                isMoving = true;
                if (currentDir[i] == 1)
                {
                    moveVelocityY = walkSpeed * 3;
                }
                if (currentDir[i] == 3)
                {
                    moveVelocityY = -walkSpeed * 3;

                }

                if (currentDir[i] == 2)
                {
                    print("WHERELOOPIS");
                    moveVelocityX = -walkSpeed * 4;
                    
                    while (transform.position.x < currentXs[i])
                    {
                        print("loop");
                        
                        GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocityX, moveVelocityY) * Time.deltaTime;
                    }
                    
                }

                if (currentDir[i] == 4)
                {
                    moveVelocityX = walkSpeed * 4;

                }

            }
        }
        animator.SetBool("IsMoving", isMoving);
        /*
        if (isMoving)
        {
            if (currentDir[i] == 1)
            {
                
            }
            if (currentDir[i] == 3)
            {

            }

            if (currentDir[i] == 2)
            {
                
                isMoving = false;
            }

            if (currentDir[i] == 4)
            {

            }
        }
        */
    }
}
