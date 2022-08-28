using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PState//broader state that holds idle and move since they have similar requirements and exit conditions.
{
    protected float movedirection;

    private bool jumpinput;

    private bool attackinput;
    private bool attack2input;
    private bool attack3input;
    private bool attack4input;

    private bool isGrounded;

    //the relevant variables need to have a version for the state they are used in. The data is then passed through

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfGrounded();
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
        movedirection = player.MoveDirection;//passes in movedirection to tell if player is moving
        jumpinput = player.JumpInput;//this senses if the jump input is pressed
        attackinput = player.AttackInput;//this senses if the attack input is pressed
        attack2input = player.Attack2Input;//this senses if the attack input is pressed
        attack3input = player.Attack3Input;//this senses if the attack input is pressed
        attack4input = player.Attack4Input;//this senses if the attack input is pressed

        if (jumpinput)
        {
            player.UseJumpInput();//this sends a signal to the input handler that the jump is used and turns it back to false
            Debug.Log("jumpinput detected going into jump state");
            stateMachine.ChangeState(player.JumpState);
        }
        else if (attackinput)
        {
            player.UseAttackInput();
            stateMachine.ChangeState(player.AttackState);
        }
        else if (attack2input)
        {
            player.UseAttack2Input();
            stateMachine.ChangeState(player.Attack2State);
        }
        else if (attack3input)
        {
            player.UseAttack3Input();
            stateMachine.ChangeState(player.Attack3State);
        }
        else if (attack4input)
        {
            player.UseAttack4Input();
            stateMachine.ChangeState(player.Attack4State);
        }
        else if (!isGrounded)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
