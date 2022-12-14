using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2CharState : AbilityState
{
    public Attack2CharState(RigoCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DoATK2 = false;//relates to the individual vs base core system as a confirm for the base core signal sytem.


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished == true)
        {

            if (Core.isDownPressed == true)//if down is pressed
            {
                stateMachine.ChangeState(IndivCore.Attack2AngState);
                isAnimationFinished = false;

            }
            else if (Core.isDownPressed == false)//if down is not pressed
            {
                stateMachine.ChangeState(IndivCore.Attack2NState);

                isAnimationFinished = false;
            }


        }
    }

}
