using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownState : State
{
    public DownState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    { 
        base.Enter();
        Core.DownFX();
        //landing noise? bounce? effecT?
        //player.soundManager.PlaySound("Land");
        Core.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        Core.gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)//goes off of animator frame events, change to intake data from enemy move and hold for a set amount of stun time.
        {
            stateMachine.ChangeState(Core.GetUpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
