using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2State : PlayerAbilityState
{
    public PlayerAttack2State(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.gameObject.transform.Find("hitbox 2").GetComponent<PolygonCollider2D>().enabled = true;//turns collider back on after being turned off as a hit confirm.
        player.gameObject.transform.Find("hitbox B2").GetComponent<PolygonCollider2D>().enabled = true;
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
        //if (Animationtriggered)
        //{
        //    if (facingright)
        //    {
        //        //player.r2d.velocity = new Vector2(30, player.r2d.velocity.y);
        //        player.r2d.AddForce(new Vector2(400, player.r2d.velocity.y), ForceMode2D.Impulse);
        //    }
        //    else if (!facingright)
        //    {
        //        player.r2d.AddForce(new Vector2(-400, player.r2d.velocity.y), ForceMode2D.Impulse);
        //    }

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
