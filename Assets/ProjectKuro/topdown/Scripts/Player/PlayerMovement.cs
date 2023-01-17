using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Hello! this script is responsible for controlling the overworld player character. Specifically it handles overworld inputs directly to move around, manages inputs with a enum state machine, communicates with animators depending on state
//controls some ledge programming, as well as activating and de activating pause. 
public enum PlayerState//basic enum to manage what the player is doing, an enum can hold some pre determined and different "modes" that can be changed and seen around the script.
{
    walk,//basic movement
    run,//faster movement NEW
    interact,//with people or items
    pause,//NEW
    //ride?
}

public class PlayerMovement : MonoBehaviour
{

    #region singleton
    public static PlayerMovement instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one kuroparty
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
    //components
    private Animator animator;//player animator
    private Rigidbody2D myRigidbody2D;//rigidbody of the player
    private KuroParty Party;
    public GameObject PauseUI;// connects UI elements for inventory

    //movement
    private Vector3 change;//vector 3 responsible for taking in movement input
    public float speed;//walkspeed
    public VectorValue startingPosition;//used in scene changes

    //state control
    public PlayerState currentstate;//current state of the enum
    bool PauseOpen = false;//a way to manage when pause is active
    public bool ControlActive = true;//this bool controls the player inputs and ability to do things. used in pause and combat and controlled by combat scripts
    private bool currentlyMoving;
    private bool TeamDead;

    //animator communication
    public float MoveX;//responsible for managing input to animators
    public float MoveY;

    //ledges !
    private bool inDownJumpZone;
    private bool inLeftJumpZone;
    private bool inRightJumpZone;

    void Start()
    {
        //initilizes state
        currentstate = PlayerState.walk;
        //gets components
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        Party = GetComponent<KuroParty>();
        //sets animator to look in last known direction
        animator.SetFloat("MoveX", MoveX);
        animator.SetFloat("MoveY", MoveY);
        //sets starting position
        transform.position = startingPosition.initialValue;
        currentlyMoving = false;

        inDownJumpZone = false;
        inLeftJumpZone = false;
        inRightJumpZone = false;
    }

    void Update()
    {
        change = Vector3.zero;//sets the change vectors to 0 to indicate no movement
        currentstate = PlayerState.walk;

        if (ControlActive == true)//if control is on
        {
            //gets movement input and applies it to force and animation
            change.x = Input.GetAxisRaw("Horizontal");//sets x values to horizontal inputs
            change.y = Input.GetAxisRaw("Vertical");//see above

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentstate = PlayerState.run;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentstate = PlayerState.walk;
            }

                UpdateAnimationAndMove();
            //Debug.Log(change);


            if (Input.GetKeyDown(KeyCode.E))//E is the pause buttom
            {
               
                //activate pause state
                if (PauseOpen == false)//when pressed it will check if the pause is open, false means its not
                {
                    currentstate = PlayerState.pause;
                    //stop time
                    Time.timeScale = 0;
                    //turn off movement
                    ControlOn(false);
                    //open pause
                    PauseUI.SetActive(true);
                    //select the resume button by default
                    GameObject.Find("ResumeButton").GetComponent<Button>().Select();
                    //set pause bool to true
                    PauseOpen = true;
                    //Stop player moving animations
                    animator.SetBool("Moving", false);

                    Party.ShowParty();
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.E) && PauseOpen == true){
            UnPause();
            currentstate = PlayerState.walk;
        }
        else//if the player is not active but also not in pause it will stay still, important for starting
        {
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
            animator.SetFloat("MoveX", MoveX);
            animator.SetFloat("MoveY", MoveY);
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)//if change is not 0, aka there is input
        {
            currentlyMoving = true;//makes it clear it is moving

            MoveX = change.x;//fills animation variables with input
            MoveY = change.y;

            animator.SetBool("Moving", true);//sets animator bool for movement

            if (currentstate == PlayerState.run)//these ifs manage switching between the run and walk animation states while moving
            {
                animator.SetBool("Running", true);

            }
            else if (currentstate == PlayerState.walk)
            {
                animator.SetBool("Running", false);
            }

        }
        else//if change is 0 aka no input
        {
            currentlyMoving = false;
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }


        animator.SetFloat("MoveX", MoveX);
        animator.SetFloat("MoveY", MoveY);//applies animation at the end after it has been determined
    }

    void FixedUpdate(){
        if(inDownJumpZone && animator.GetFloat("MoveY") == -1){ //Moves the player down if near a ledge and pressing down
            ControlActive = false;
            transform.position += -Vector3.up * Time.deltaTime * 2f;
        }
        else if(inLeftJumpZone && animator.GetFloat("MoveX") == -1){
            ControlActive = false;
            transform.position += -Vector3.right * Time.deltaTime * 2f;
        }
        else if(inRightJumpZone && animator.GetFloat("MoveX") == 1){
            ControlActive = false;
            transform.position += Vector3.right * Time.deltaTime * 2f;
        }
        else if(currentlyMoving){
            MoveCharacter();
        }
    }//used for ledges

    void MoveCharacter()
    {
        change.Normalize();//normalizes input values for consistency
        float sprintSpeed = speed * 2; //Temporary sprint speed in case the player is sprinting.
        if(currentstate == PlayerState.run){
            //change is added to the current position, it is multiplied by the runspeed, and by every tick that passes.
            myRigidbody2D.MovePosition(
                transform.position + change * sprintSpeed * Time.deltaTime
                );
        }
        else{
            //change is added to the current position, it is multiplied by the walkspeed, and by every tick that passes.
            myRigidbody2D.MovePosition(
                transform.position + change * speed * Time.deltaTime
                );
        }
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

    public void UnPause(){
        //resume time
        Time.timeScale = 1;
        //deselect any buttons
        GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        //close pause
        PauseUI.SetActive(false);
        //if the inventory is active, make it inactive
        for(int i = 0; i < PauseUI.gameObject.transform.parent.transform.childCount; i++){
            if(PauseUI.gameObject.transform.parent.transform.GetChild(i).gameObject != null){
                PauseUI.gameObject.transform.parent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        //set bool to false
        PauseOpen = false;
        //turn on movement
        ControlOn(true);
    }

    public void resetHealth(){
        gameObject.GetComponent<KuroParty>().resetHealth();
    }//??? adjust to be more in line with kuroparty and data card functions

    private void OnTriggerEnter2D(Collider2D other)//ledge jumping
    {
        if(other.gameObject.name == "JumpDownCollision")
        {
            inDownJumpZone = true;
        }
        else if(other.gameObject.name == "JumpLeftCollision"){
            inLeftJumpZone = true;
        }
        else if(other.gameObject.name == "JumpRightCollision"){
            inRightJumpZone = true;
        }
        else if(other.gameObject.name == "EndCollision"){
            if(inDownJumpZone || inLeftJumpZone || inRightJumpZone){
                inDownJumpZone = false;
                inLeftJumpZone = false;
                inRightJumpZone = false;
                ControlActive = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.name == "JumpDownCollision" && ControlActive)
        {
            inDownJumpZone = false;
        }
        else if(other.gameObject.name == "JumpLeftCollision" && ControlActive){
            inLeftJumpZone = true;
        }
        else if(other.gameObject.name == "JumpRightCollision" && ControlActive){
            inRightJumpZone = true;
        }
        else if(other.gameObject.name == "NoJumpCollision"){
            other.isTrigger = false;
        }
    }//ledge jumping

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "NoJumpCollision" && !ControlActive){
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
