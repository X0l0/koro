using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownState : PState
{
    public PlayerDownState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine,animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        //landing noise? bounce? effecT?
        player.soundManager.PlaySound("Land");
        //take away hitbox?
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
            stateMachine.ChangeState(player.GetUpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
