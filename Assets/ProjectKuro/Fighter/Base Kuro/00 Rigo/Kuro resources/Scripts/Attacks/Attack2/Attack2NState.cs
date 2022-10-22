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
        //relates to the individual vs base core system as a confirm for the base core signal sytem.
        Debug.Log("entering attack 2 neutral start");
        Core.MoveCoolDown.StartAtk2Cooldown();//starts cooldown only when actually entering the state.
        //Core.r2d.velocity = new Vector2(Core.r2d.velocity.x, Core.JumpHeight * .75f);

    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Animationtriggered)
        {
            Debug.Log("Animation triggered");
            IndivCore.EyeLazer();
            Animationtriggered = false;//stops multiple fx from appearing

        } 
        
        if(isAnimationFinished)
        {
            isAnimationFinished = false;
            Debug.Log("animation cycle defined as finished");
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
        }
    }
}
