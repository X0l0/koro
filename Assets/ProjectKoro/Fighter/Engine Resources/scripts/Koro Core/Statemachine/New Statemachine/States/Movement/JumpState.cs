using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    public JumpState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.soundManager.PlaySound("Bounce");//plays hit sound if contact is made PUT BACK IN
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)//upon an animation trigger, aka the end of the animation
        {
            Core.r2d.velocity = new Vector2(Core.r2d.velocity.x, Core.JumpHeight);//apply jump force
            stateMachine.ChangeState(Core.InAirState);
            //IsAbilityDone = true;//ability is done, letting the super state handle the logic, change to air state automtacially?
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2((Core.MoveDirection * Core.MaxSpeed), Core.r2d.velocity.y);//this allows the player to control there movement midair
    }

}