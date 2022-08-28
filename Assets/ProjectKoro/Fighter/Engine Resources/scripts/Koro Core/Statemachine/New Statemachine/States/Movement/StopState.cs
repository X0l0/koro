using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopState : GroundedState
{
    public bool FacingRight;
    public StopState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        FacingRight = Core.facingRight;
        if(FacingRight && Core.MoveDirection < 0)
        {
            stateMachine.ChangeState(Core.TurnState);
        }
        else if(!FacingRight && Core.MoveDirection > 0)
        {
            stateMachine.ChangeState(Core.TurnState);
        }
        else if (isAnimationFinished)
        {
            stateMachine.ChangeState(Core.IdleState);
        }
    }
}
