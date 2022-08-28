using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownState : State
{
    public DownState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
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
            stateMachine.ChangeState(Core.GetUpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
