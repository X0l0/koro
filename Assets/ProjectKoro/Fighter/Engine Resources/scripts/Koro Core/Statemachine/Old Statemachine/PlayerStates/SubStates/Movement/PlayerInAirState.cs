using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAerialState
{
    
    private float movedirection;

    private bool attackinput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        movedirection = player.MoveDirection;
        attackinput = player.AttackInput;
        if (attackinput)
        {
            player.UseAttackInput();
            stateMachine.ChangeState(player.AirAttackState);
        }

  
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
            player.r2d.velocity = new Vector2(movedirection * player.MaxSpeed, player.r2d.velocity.y);//this allows the player to control there movement midair
    }
}
