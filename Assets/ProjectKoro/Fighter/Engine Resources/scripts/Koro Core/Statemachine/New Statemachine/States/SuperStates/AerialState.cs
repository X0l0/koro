using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialState : State
{
    private bool isGrounded;

    public AerialState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()//this tells in air states to to turn into land whenever the ground is detected
    {
        base.LogicUpdate();

        isGrounded = Core.isGrounded;

        if (isGrounded && Core.r2d.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(Core.LandState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
