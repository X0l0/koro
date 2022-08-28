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
        //Core.DoATK2 = false;//relates to the individual vs base core system as a confirm for the base core signal sytem.
        Core.MoveCoolDown.StartAtk2Cooldown();//starts cooldown only when actually entering the state.


        //Core.r2d.velocity = new Vector2(Core.r2d.velocity.x, Core.JumpHeight * .75f);

    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)
        {
            Animationtriggered = false;//stops multiple fx from appearing
        IndivCore.EyeLazer();
        }

        if (isAnimationFinished)
        {
            //IndivCore.DeActivateHB1();//de activates hitbox depending on animator
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
        }
    }
}
