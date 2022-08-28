using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour//attaches to game object and handles input
{
    public KoroCore KoroCore { get; private set; }

    public float InputMoveDirection;
    //public bool IsDownPressed;

    #region inputholdtime
    [SerializeField]//serialized fields allow the variables to be private but be seen and editied in the editor
    private float InputHoldTime = 0.2f;//how long it holds the jump input boolean
    public float JumpInputStartTime;//start times are held locally and are logged when inputs are put in, they are then compared with the actual time to see if enough time
    public float AttackInputStartTime;//has passed to let go of input.
    public float MovementInputStartTime;
    #endregion


    public void ConnectCore(KoroCore Core)
    {
        KoroCore = Core;
    }

    public void ClearCore()
    {
        KoroCore = null;//part of core testing
    }
    public void Update()
    {
        //CheckDirectionalInput();
        if (KoroCore == null)//if there is no core connected disregard inputs
        {
            return;
        }

        CheckJumpInputHoldTime();
        CheckAttackInputHoldTime();


        //// Movement controls
        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //{
        //    InputMoveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;//glitch where left input can override right input, but not vice versa

        //    //InputMoveDirection = Input.GetAxisRaw("Horizontal");

        //    KoroCore.SetMoveDirection(InputMoveDirection);
        //    MovementInputStartTime = Time.time;//not used?
        //}

        ////jump
        //if (Input.GetKeyDown(KeyCode.W))//holding up lets you go farther for a certain amount of time
        //{

        //    //player.Jump();
        //    KoroCore.Jump();
        //    JumpInputStartTime = Time.time;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        ////IsDownPressed = Input.GetKey(KeyCode.S);
        //KoroCore.DownIsPressed(Input.GetKey(KeyCode.S));
        //}
        //else if (Input.GetKeyUp(KeyCode.S))
        //{
        //KoroCore.DownIsPressed(false);
        //}

        //// Attack
        //if (Input.GetKeyDown(KeyCode.U))//instead of having multiple if bools, maybe just one variable that changes depending on the input?
        //{
        //    //player.Attack1();
        //    KoroCore.Attack1();

        //    AttackInputStartTime = Time.time;
        //}

        //// Attack 2
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    //player.Attack2();
        //    KoroCore.Attack2();

        //    AttackInputStartTime = Time.time;
        //}

        //// Attack 3
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    //player.Attack3();
        //    AttackInputStartTime = Time.time;
        //}

        //// Attack 4
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    //player.Attack4();
        //    AttackInputStartTime = Time.time;
        //}


    }

    #region InputHoldTime
    private void CheckDirectionalInput()
    {
        if (Time.time >= MovementInputStartTime + InputHoldTime)
        {
            //player.expireJumpInput();
            InputMoveDirection = 0;
           
        }
    }

    private void CheckJumpInputHoldTime()//these put the bool back to false after a set amount of time if the input is put in during a state where the action cannot be performed.
    {
        if(Time.time >= JumpInputStartTime + InputHoldTime)
        {
            KoroCore.expireJumpInput();
        }
    }

    private void CheckAttackInputHoldTime()//change to delta time?
    {
        if (Time.time >= AttackInputStartTime + InputHoldTime)//if the current time is past the input start time plus the eloted hold time, then the input is set back to false
        {
            KoroCore.expireAttackInput();
        }
    }
    #endregion

}



