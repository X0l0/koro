using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandState : GroundedState
{
    private float Movedirection;
    public LandState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (movedirection != 0)
        //{
        //    stateMachine.ChangeState(Core.MoveState);
        //}
        //else 
        Movedirection = Core.MoveDirection;

        Core.ChangeFacingDirection();//added to let the player change direction/pivot when landing, creating a good feeling of tight controls.

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(Core.IdleState);
        }
    }



    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2(Core.r2d.velocity.x, Core.r2d.velocity.y);//this allows the player to control there movement midair
       
    }

}