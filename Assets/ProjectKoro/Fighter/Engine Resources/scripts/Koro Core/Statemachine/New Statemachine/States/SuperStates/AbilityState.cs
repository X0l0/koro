using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : State
{
    public RigoCore IndivCore;//as this is an attack state, it creates an seperate reference to the individual core not the base core.
    //protected bool facingright;
    protected bool IsAbilityDone;
    public bool isGrounded; //may need to be protected if ability states need to check if there grounded.
    public AbilityState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
        IndivCore = core;//this variables is filled with the individual core upon being created.
    }


    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //facingright = player.facingRight;

        isGrounded = Core.isGrounded;

        if (IsAbilityDone)
        {
            if (isGrounded && Core.r2d.velocity.y < 0.1f)
            {
                stateMachine.ChangeState(Core.IdleState);
            }
            else
            {
                stateMachine.ChangeState(Core.InAirState);//this may need to be moved and focused on the direct aerial superstate
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
