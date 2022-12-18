using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1AerState : AbilityState
{
    public Attack1AerState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DoATK1 = false;//relates to the individual vs base core system as a confirm for the base core signal sytem.
        Core.MoveCoolDown.StartAtk1Cooldown();//starts cooldown only when actually entering the state.

        InAirAtk = true;
        //change gravity or velocity to stay in air until attack is finished?
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            IndivCore.DeActivateHB1();//de activates hitbox depending on animator
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
        }
    }
}
