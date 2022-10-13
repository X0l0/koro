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
            InputMoveDirection = Input.GetKey(KeyCode.LeftArrow) ? -1 : 1;//glitch where left input can override right input, but not vice versa

            //InputMoveDirection = Input.GetAxisRaw("Horizontal");

            KuroCore.SetMoveDirection(InputMoveDirection);
            MovementInputStartTime = Time.time;
        }

        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow))//holding up lets you go farther for a certain amount of time
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
        if (Input.GetKeyDown(KeyCode.Keypad4))//instead of having multiple if bools, maybe just one variable that changes depending on the input?
        {
            //player.Attack1();
            KuroCore.Attack1();

            AttackInputStartTime = Time.time;
        }

        // Attack 2
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            //player.Attack2();
            KuroCore.Attack2();

            AttackInputStartTime = Time.time;
        }

        // Attack 3
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //player.Attack3();
            AttackInputStartTime = Time.time;
        }

        // Attack 4
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            //player.Attack4();
            AttackInputStartTime = Time.time;
        }


    }




}



