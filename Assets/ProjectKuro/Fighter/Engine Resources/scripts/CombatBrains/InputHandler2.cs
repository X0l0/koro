using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler2 : InputHandler//attaches to game object and handles input
{

   new void Update()
    {
        base.Update();//base update is then called anyway.

        if (KuroCore == null)//if there is no core connected disregard inputs
        {
            return;
        }

        // Movement controls
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)){
                InputMoveDirection = 0; //If holding both move keys, don't move
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                InputMoveDirection = -1; //If holding left move key, move left
            }
            else{
                InputMoveDirection = 1; //If holding right move key, move right
            }

            KuroCore.SetMoveDirection(InputMoveDirection);
            MovementInputStartTime = Time.time;
        }

        //jump
        if (Input.GetKey(KeyCode.UpArrow) && KuroCore.isGrounded)//holding up lets you go farther for a certain amount of time
        {

            //player.Jump();
            KuroCore.Jump();
            JumpInputStartTime = Time.time;
           
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //IsDownPressed = Input.GetKey(KeyCode.S);
            KuroCore.DownIsPressed(Input.GetKey(KeyCode.DownArrow));
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            KuroCore.DownIsPressed(false);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.Keypad4) && !KuroCore.AttackInput)//instead of having multiple if bools, maybe just one variable that changes depending on the input?
        {
            //player.Attack1();
            KuroCore.Attack1();

            AttackInputStartTime = Time.time;
       
        }

        // Attack 2
        if (Input.GetKeyDown(KeyCode.Keypad8) && !KuroCore.Attack2Input)
        {
            //player.Attack2();
            KuroCore.Attack2();

            AttackInputStartTime = Time.time;

        }

        // Attack 3
        if (Input.GetKeyDown(KeyCode.Keypad5) && !KuroCore.Attack3Input)
        {
            //player.Attack3();
            AttackInputStartTime = Time.time;
     
        }

        // Attack 4
        if (Input.GetKeyDown(KeyCode.Keypad6) && !KuroCore.Attack4Input)
        {
            //player.Attack4();
            AttackInputStartTime = Time.time;
     
        }


    }




}



