using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundedState
{
    public LandState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (movedirection != 0)
        //{
        //    stateMachine.ChangeState(Core.MoveState);
        //}
        //else 
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(Core.IdleState);
        }
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2((Core.MoveDirection * Core.MaxSpeed), Core.r2d.velocity.y);//this allows the player to control there movement midair
    }

}