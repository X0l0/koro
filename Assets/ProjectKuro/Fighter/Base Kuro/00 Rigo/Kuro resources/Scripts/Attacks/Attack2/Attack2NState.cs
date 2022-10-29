using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2NState : AbilityState
{
    public Attack2NState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Debug.Log("atk 2 Nstate state entered");
        //relates to the individual vs base core system as a confirm for the base core signal sytem.
        Core.MoveCoolDown.StartAtk2Cooldown();//starts cooldown only when actually entering the state.

        //Core.r2d.velocity = new Vector2(Core.r2d.velocity.x, Core.JumpHeight * .75f);

    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)
        {
            //Debug.Log("atk 2 Nstate animation triggered");
            IndivCore.EyeLazer();
            Animationtriggered = false;//stops multiple fx from appearing


        }

        if (isAnimationFinished)
        {
            //Debug.Log("animation cycle defined as finished");
            isAnimationFinished = false;
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
            //IndivCore.DeActivateHB1();//de activates hitbox depending on animator
        }
    }
}
