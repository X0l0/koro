using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUpState : State
{
    public GetUpState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)//goes off of animator frame events, change to intake data from enemy move and hold for a set amount of stun time.
        {
            stateMachine.ChangeState(Core.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
