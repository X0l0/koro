using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2AerState : AbilityState
{
    public Attack2AerState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DoATK2 = false;//relates to the individual vs base core system as a confirm for the base core signal sytem.
        Core.MoveCoolDown.StartAtk2Cooldown();//starts cooldown only when actually entering the state.
        
        //stay in air?



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
            IsAbilityDone = true;//lets ability super state take over, mainly switching to idle or in air depending on if grounded
        }
    }

}
