using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieInAirState : State
{
    public DieInAirState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Core.DieInAir();
    }
}
