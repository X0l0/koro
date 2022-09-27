using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PState
{
    public PlayerDeadState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Die();
    }
}