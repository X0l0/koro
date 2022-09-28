﻿using System.Collections;
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
    public GameObject InventoryUI;
    bool InventoryOpen = false;


    public PlayerState1 currentstate;//current state of the enum
    public float speed;//walkspeed
    private Rigidbody2D myRigidbody2D;//rigidbody of the player
    private Vector3 change;//vector 3 responsible for taking in movement input
    private Animator animator;//player animator
    public VectorValue startingPosition;//used in scene changes


    void Start()
    {
        //initilizaes state
        currentstate = PlayerState1.walk;
        //gets components
        animator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        //sets animator to looking down
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
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
        }      

        if (Input.GetKeyDown(KeyCode.E))//E is the inventory buttom
        {
            if(InventoryOpen == false)//when pressed it will check if the inventory is open, false means its not
            {
                //turn off movement
                ControlOn(false);
                //open inventory
                InventoryUI.SetActive(true);
                //set inventory bool to true
                InventoryOpen = true;
                //Stop player moving animations
                animator.SetBool("Moving", false);
            }
            else if (InventoryOpen == true)
            {
                //close inventory
                InventoryUI.SetActive(false);
                //set bool to false
                InventoryOpen = false;
                //turn on movement
                ControlOn(true);
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)//if change is not 0, aka there is input
        {
            MoveCharacter();
            //setting animator angles and whether or not there is movement
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
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
}