using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2AngState : AbilityState
{
    public Attack2AngState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
       
        Core.MoveCoolDown.StartAtk2Cooldown();//starts cooldown only when actually entering the state.

        //change rotatio of mouth point?

        



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
            isAnimationFinished = false;
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
        }
    }

}
