using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PState
{
    //protected bool facingright;
    protected bool IsAbilityDone;

    public bool isGrounded; //may need to be protected if ability states need to check if there grounded.
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //facingright = player.facingRight;
        
        if (IsAbilityDone)
        {
            if (isGrounded && player.r2d.velocity.y < 0.1f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else
            {
                stateMachine.ChangeState(player.InAirState);//this may need to be moved and focused on the direct aerial superstate
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
