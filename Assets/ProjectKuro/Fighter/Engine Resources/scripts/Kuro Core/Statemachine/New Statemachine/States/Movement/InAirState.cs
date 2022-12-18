using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : AerialState
{

    private float movedirection;
    private bool FacingRight;



    private bool attackinput;
    private bool attack2input;
    private bool attack3input;
    private bool attack4input;
    public InAirState(KuroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        movedirection = Core.MoveDirection;
        attackinput = Core.AttackInput;
        attack2input = Core.Attack2Input;//this senses if the attack input is pressed
        attack3input = Core.Attack3Input;//this senses if the attack input is pressed
        attack4input = Core.Attack4Input;//this senses if the attack input is pressed


        if (attackinput)
        {
            Core.UseAttackInput();
            //stateMachine.ChangeState(Core.Attack1AerState);
            Core.DoAttack1();
        }
        else if (attack2input)
        {
            Core.UseAttack2Input();
            //stateMachine.ChangeState(Core.Attack2NState);
            Core.DoAttack2();
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Core.r2d.velocity = new Vector2((movedirection * Core.MaxSpeed) * .75f  , Core.r2d.velocity.y);//this allows the player to control there movement midair
    }
}