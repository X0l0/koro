using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : PlayerAbilityState
{
    public bool Isstunned;//local bool that takes in player isStunned bool on update

    public PlayerStunState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.r2d.gravityScale = 0.0f;//stops gravity and holds self in air
        player.r2d.velocity = Vector3.zero;//takes away any other velocity
        //play shake effect
        player.StartCoroutine("HoldStun");//calls holdstun coroutine in main player, this script checks when stun is over in update and then lets go.
       
    }

    public override void Exit()
    {
        base.Exit();
        player.r2d.gravityScale = 70;//puts gravity back upon leaving state
        //player.r2d.velocity = Vector3.zero;//takes away any other velocity
    }

    public override void LogicUpdate()
    {
         base.LogicUpdate();


        Isstunned = player.IsStunned;//fills local bool from bool in player, checks every update if stun is over.

        if (Isstunned == false)
        {
                    stateMachine.ChangeState(player.LaunchState);
             }
    }


    //public IEnumerator HoldStun()
    //{
    ////    Debug.Log("hitconfirm, disabling hitbox");
    ////    GetComponent<Collider2D>().enabled = false;
    //   yield return new WaitForSeconds(.1f);
    ////    GetComponent<Collider2D>().enabled = true;
    ////    Debug.Log("hitconfirm over enabling hitbox");
    //}
}