using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : GroundedState
{
    public float CurrentMoveDirection;
    public bool FacingRight;
    public MoveState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        CurrentMoveDirection = Core.MoveDirection;
        Core.ChangeFacingDirection();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FacingRight = Core.facingRight;
        


        if (Core.MoveDirection != CurrentMoveDirection && Core.MoveDirection != 0 )
        {
            stateMachine.ChangeState(Core.TurnState);
        }
        if (movedirection == 0 )//if move input is let go of entirely it means its time to stop.
        {
            stateMachine.ChangeState(Core.IdleState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2(movedirection * Core.MaxSpeed, Core.r2d.velocity.y);//this takes in move input and applys the movement, not having this line of code in a state means it cannot move
    }
}
