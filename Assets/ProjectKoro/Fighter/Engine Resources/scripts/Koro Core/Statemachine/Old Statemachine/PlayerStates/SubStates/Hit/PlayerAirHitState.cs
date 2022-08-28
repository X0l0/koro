using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirHitState : PlayerAerialState
{
    public PlayerAirHitState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
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
            player.hit = false;
            //stateMachine.ChangeState(player.InAirState);
            stateMachine.ChangeState(player.LaunchState);
        }
    }
}
