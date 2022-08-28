using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack3State : PlayerAbilityState
{
    public PlayerAttack3State(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.DoDash();
        player.gameObject.transform.Find("hitbox 3").GetComponent<PolygonCollider2D>().enabled = true;//turns collider back on after being turned off as a hit confirm.
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //if (Animationtriggered)
        //{
        //    player.Barkfx();
        //    Animationtriggered = false;//stops multiple fx from appearing
        //}

        if (isAnimationFinished)
        {
            IsAbilityDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}