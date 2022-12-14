using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler1 : InputHandler
{
    new void Update()
    {
        base.Update();//base update is then called anyway.

        if (KuroCore == null)//if there is no core connected disregard inputs
        {
            return;
        }

        // Movement controls
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)){
                InputMoveDirection = 0; //If holding both move keys, don't move
            }
            else if(Input.GetKey(KeyCode.A)){
                InputMoveDirection = -1; //If holding left move key, move left
            }
            else{
                InputMoveDirection = 1; //If holding right move key, move right
            }

            KuroCore.SetMoveDirection(InputMoveDirection);
            MovementInputStartTime = Time.time;
        }

        //jump
        if (Input.GetKey(KeyCode.W) && KuroCore.isGrounded)//holding up lets you go farther for a certain amount of time
        {

            //player.Jump();
            KuroCore.Jump();
            JumpInputStartTime = Time.time;
           
        }

        if (Input.GetKey(KeyCode.S))
        {
            //IsDownPressed = Input.GetKey(KeyCode.S);
            KuroCore.DownIsPressed(Input.GetKey(KeyCode.S));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            KuroCore.DownIsPressed(false);
        }

        // Attack
        if (Input.GetKeyDown(KeyCode.U) && !KuroCore.AttackInput)//instead of having multiple if bools, maybe just one variable that changes depending on the input?
        {
            //player.Attack1();
            KuroCore.Attack1();

            AttackInputStartTime = Time.time;
         
        }

        // Attack 2
        if (Input.GetKeyDown(KeyCode.I) && !KuroCore.Attack2Input)
        {
            //Debug.Log("atk 2 keycode pressed down");
            //player.Attack2();
            KuroCore.Attack2();//activates function in controlled kuro core that the input was pressed

            AttackInputStartTime = Time.time;
     
        }

        // Attack 3
        if (Input.GetKeyDown(KeyCode.O) && !KuroCore.Attack3Input)
        {
            //player.Attack3();
            AttackInputStartTime = Time.time;
         
        }

        // Attack 4
        if (Input.GetKeyDown(KeyCode.P) && !KuroCore.Attack4Input)
        {
            //player.Attack4();
            AttackInputStartTime = Time.time;
        
        }


    }




}
