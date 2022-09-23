using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is the overworld player object, responsible for the overworld players inputs and capabilities.
public enum PlayerState1//basic enum to manage what the player is doing
{
    walk,
    interact
    //battle?
    //ride?
}

public class PlayerMovement : MonoBehaviour
{

    #region singleton
    public static PlayerMovement instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one koroparty
    //and you can call it easily by just calling PlayerMovement.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of PlayerMovement found");
            return;
        }
        instance = this;
    }
    #endregion


    public bool ControlActive = true;//connects to battle code to turn off and on overworld control.

    [SerializeField]
    public GameObject PauseUI;
    bool PauseOpen = false;


    public PlayerState1 currentstate;//current state of the enum
    public float speed;//walkspeed
    private Rigidbody2D myRigidbody2D;//rigidbody of the player
    private Vector3 change;//vector 3 responsible for taking in movement input
    private Animator animator;//player animator
    public VectorValue startingPosition;//used in scene changes

    [SerializeField]
    public float MoveX;
    public float MoveY;

    void Start()
    {
        //initilizes state
        currentstate = PlayerState1.walk;
        //gets components
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        //sets animator to look in last known direction
        if(MoveX != null){
            animator.SetFloat("MoveX", MoveX);
            animator.SetFloat("MoveY", MoveY);
        }
        //sets starting position
        transform.position = startingPosition.initialValue;
    }

    //public void update()
    //{

    //}

    void Update()//called every frame
    {
        change = Vector3.zero;//sets the change vectors to 0 to indicate no movement

        if (ControlActive == true)//if control is on
        {
            change.x = Input.GetAxisRaw("Horizontal");//sets x values to horizontal inputs
            change.y = Input.GetAxisRaw("Vertical");//see above
            UpdateAnimationAndMove();
            //Debug.Log(change);
            if (Input.GetKeyDown(KeyCode.E))//E is the pause buttom
            {
                if(PauseOpen == false)//when pressed it will check if the pause is open, false means its not
                {
                    //stop time
                    Time.timeScale = 0;
                    //turn off movement
                    ControlOn(false);
                    //open pause
                    PauseUI.SetActive(true);
                    //set pause bool to true
                    PauseOpen = true;
                    //Stop player moving animations
                    animator.SetBool("Moving", false);
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.E) && PauseOpen == true){
            //resume time
            Time.timeScale = 1;
            //close pause
            PauseUI.SetActive(false);
            //if the inventory is active, make it inactive
            if(PauseUI.gameObject.transform.parent.transform.GetChild(0).gameObject != null){
                PauseUI.gameObject.transform.parent.transform.GetChild(0).gameObject.SetActive(false);
            }
            //set bool to false
            PauseOpen = false;
            //turn on movement
            ControlOn(true);
        }
        else{
            animator.SetBool("Moving", false);
            animator.SetFloat("MoveX", MoveX);
            animator.SetFloat("MoveY", MoveY);
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)//if change is not 0, aka there is input
        {
            MoveCharacter();
            //setting animator angles and whether or not there is movement
            MoveX = change.x;
            MoveY = change.y;
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
        animator.SetFloat("MoveX", MoveX);
        animator.SetFloat("MoveY", MoveY);
    }


    void MoveCharacter()
    {
        change.Normalize();//normalizes input values for consistency
        //change is added to the current position, it is multiplied by the walkspeed, and by every tick that passes.
        myRigidbody2D.MovePosition(
            transform.position + change * speed * Time.deltaTime
            );
    }


    public void ControlOn(bool ControlOn)//bool controlled by combat scripts to turn input on and off.
    {
        ControlActive = ControlOn;
        return;
    }

    public void SetXY(float x, float y){
        MoveX = x;
        MoveY = y;
    }
}
