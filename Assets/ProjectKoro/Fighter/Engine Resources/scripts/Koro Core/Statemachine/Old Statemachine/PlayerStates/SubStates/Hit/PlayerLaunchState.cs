using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchState : PState
{
    private bool isGrounded;
    public PlayerLaunchState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isGrounded && player.r2d.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.DownState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
