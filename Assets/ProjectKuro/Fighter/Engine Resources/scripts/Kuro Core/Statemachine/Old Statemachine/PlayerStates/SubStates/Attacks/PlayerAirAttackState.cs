using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerAerialState
{
    public PlayerAirAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //change gravity or velocity to stay in air until attack is finished?
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)
        {
            player.Barkfx();//calls fx function in player.
            Animationtriggered = false;//stops multiple fx from appearing
        }

        if (isAnimationFinished)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }
}
