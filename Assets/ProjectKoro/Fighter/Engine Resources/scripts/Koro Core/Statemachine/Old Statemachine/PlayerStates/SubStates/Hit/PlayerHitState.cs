using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerAbilityState
{
    public PlayerHitState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
 
        //damage function is in player and is directly called by enemy hitbox, move here?
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
            player.hit = false;//turns hit bool off as hit is done
            if (!isGrounded)//if off the ground after hit animation that means the knockback is alot and thus the launch state is activated
            {
                stateMachine.ChangeState(player.LaunchState);
            }
            else if (isGrounded)//if still on the ground after the hit animationi that means the knockback was low and footing can be regained.
            {
                IsAbilityDone = true;//make new hit super state rather then the ability state?
            }
        }
    }


}
