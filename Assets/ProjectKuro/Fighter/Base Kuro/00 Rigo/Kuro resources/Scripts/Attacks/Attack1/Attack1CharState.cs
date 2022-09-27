using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1CharState : AbilityState
{
    public Attack1CharState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DoATK1 = false;//relates to the individual vs base core system as a confirm for the base core signal sytem.
        //Core.MoveCoolDown.StartAtk1Cooldown();//starts cooldown only when actually entering the state.
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            //Debug.Log("attack charge finished deciding if attack angled");
            //IndivCore.DoGroundAtk1();
            if (Core.isDownPressed == true)
            {
                stateMachine.ChangeState(IndivCore.Attack1AngState);
            }
            else if (Core.isDownPressed == false)
            {
                stateMachine.ChangeState(IndivCore.Attack1NState);
            }


        }
    }
}
