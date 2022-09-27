using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)//upon an animation trigger, aka the end of the animation
        {
            player.r2d.velocity = new Vector2(player.r2d.velocity.x, player.JumpHeight);//apply jump force
        IsAbilityDone = true;//ability is done, letting the super state handle the logic, change to air state automtacially?
        }

    }

}
