using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : State//broader state that holds grounded states like idle and move that are inputtabled and have similar exit conditions.
{
    protected float movedirection;//local variable that holds player inputs

    private bool jumpinput;
    
    private bool attackinput;
    private bool attack2input;
    private bool attack3input;
    private bool attack4input;

    private bool isGrounded;

    //the relevant variables need to have a version for the state they are used in. The data is then passed through

    public GroundedState(KoroCore core, StateMachine stateMachine, string animBoolName) : base(core, stateMachine, animBoolName)
    {
    }

    public override void LogicUpdate()//overrides over ride any inherited function and replace it with this one.
    {
        base.LogicUpdate();//base functions call the inheritated function.
        isGrounded = Core.isGrounded;
        movedirection = Core.MoveDirection;//passes in movedirection to tell if player is moving
        jumpinput = Core.JumpInput;//this senses if the jump input is pressed
        attackinput = Core.AttackInput;//this senses if the attack input is pressed
        attack2input = Core.Attack2Input;//this senses if the attack input is pressed
        attack3input = Core.Attack3Input;//this senses if the attack input is pressed
        attack4input = Core.Attack4Input;//this senses if the attack input is pressed

        //Debug.Log(movedirection);

        if (jumpinput)
        {
            Core.UseJumpInput();//this sends a signal to the input handler that the jump is used and turns it back to false
            //Debug.Log("jumpinput detected going into jump state");
            stateMachine.ChangeState(Core.JumpState);
        }
        else if (attackinput)
        {
            Core.UseAttackInput();
            //stateMachine.ChangeState(Core.Attack1NState);
            Core.DoAttack1();
        }
        else if (attack2input)
        {
            Core.UseAttack2Input();
            //stateMachine.ChangeState(Core.Attack2NState);
            Core.DoAttack2();
        }
        //else if (attack3input)
        //{
        //    Core.UseAttack3Input();
        //    stateMachine.ChangeState(Core.Attack3State);
        //}
        //else if (attack4input)
        //{
        //    Core.UseAttack4Input();
        //    stateMachine.ChangeState(Core.Attack4State);
        //}
        else if (!isGrounded)
        {
            stateMachine.ChangeState(Core.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
