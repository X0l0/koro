using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnState : GroundedState
{
    public TurnState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Exit()
    {
        Core.ChangeDirection();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(Core.IdleState);
        }
    }
}
