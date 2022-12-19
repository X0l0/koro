using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public JumpState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.soundManager.PlaySound("Bounce");//plays hit sound if contact is made PUT BACK IN
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)//upon an animation trigger, aka the end of the animation
        {
            Vector2 jumpvelocity = new Vector2(Core.r2d.velocity.x, (Core.JumpHeight * 50));//apply jump force
            Core.r2d.AddForce(jumpvelocity, ForceMode2D.Impulse);
            stateMachine.ChangeState(Core.InAirState);
            //IsAbilityDone = true;//ability is done, letting the super state handle the logic, change to air state automtacially?


            // public bool isJumping;
            //    isJumping = true;
            //}
            //else if (Input.GetKeyUp(KeyCode.W) || KuroCore.gameObject.transform.position.y >= KuroCore.MaxJumpHeight)
            //{
            //    isJumping = false; //Make the player fall if the jump key is released or the max height is reached
            //}
            //else if (Input.GetKey(KeyCode.W) && isJumping)
            //{
            //    KuroCore.r2d.velocity = new Vector2(KuroCore.r2d.velocity.x, KuroCore.JumpHeight * 0.9f);//apply jump force
            //}

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2((Core.MoveDirection * Core.MaxSpeed), Core.r2d.velocity.y);//this allows the player to control there movement midair
    }

}