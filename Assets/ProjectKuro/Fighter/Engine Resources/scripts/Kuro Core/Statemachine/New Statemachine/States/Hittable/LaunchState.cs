using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchState : State
{
    private bool isGrounded;
    public LaunchState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isGrounded = Core.isGrounded;
        if (isGrounded && Core.r2d.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(Core.DownState);
        }
    }

    
}
