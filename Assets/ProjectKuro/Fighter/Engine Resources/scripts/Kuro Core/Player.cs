using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region requiredcomponents
//[RequireComponent(typeof(CapsuleCollider2D))]//require component help against startup errors
//[RequireComponent(typeof(Rigidbody2D))]
#endregion

public class Player : MonoBehaviour//attaches to game object to create states and statemachine
{

    public CardHolder CardHolder { get; private set; }
    public MatchConnecter MatchConnecter { get; private set; }


    #region States
    //get private set seems to be able to connect scripts to one another quite easily, extra research into the specifics of it may be required but these are scripts and variables that need to be communicated 
    public PlayerStateMachine StateMachine { get; private set; }//creates state machine for player object
    public PlayerIdleState IdleState { get; private set; }//creates idle state
    public PlayerMoveState MoveState { get; private set; }//creates move state
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }

    public PlayerAttack1State AttackState { get; private set; }
    //attack 1 angled
    public PlayerAirAttackState AirAttackState { get; private set; }
    public PlayerAttack2State Attack2State { get; private set; }
    //attack2angled
    //attack 2 aerial
    public PlayerAttack3State Attack3State { get; private set; }
    public PlayerAttack4State Attack4State { get; private set; }

    public PlayerHitState HitState { get; private set; }
    public PlayerAirHitState AirHitState { get; private set; }
    public PlayerLaunchState LaunchState { get; private set; }
    public PlayerDownState DownState { get; private set; }
    public PlayerGetUpState GetUpState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public PlayerStunState StunState { get; private set; }

    #endregion

    #region othercomponents
    //making these get private set and then connecting them in start lets you reference across them. however doing so here may be uneccessary as these scripts are more likely to send signals to player then rather recieve them from player.
    //public InputHandler inputHandler { get; private set; }//gets movement input to change states

    public SoundManager soundManager;

    public Health health {get; private set; }
    public MoveCooldown MoveCoolDown { get; private set; }
    public Animator Anim { get; private set;  }//animator
    public Rigidbody2D r2d { get; private set; }//was not public

    public Transform t;//could be private?

    //public Collider2D mainCollider;
    #endregion

    #region Movement
    public bool facingRight = true;
    public float MoveDirection;
    public bool isGrounded;//was not public
    public Transform Groundcheck;
    public LayerMask GroundLayer;
    public int MaxSpeed = 110;
    public int JumpHeight = 140;
    #endregion

    #region Inputs
    public bool JumpInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool Attack2Input { get; private set; }
    public bool Attack3Input { get; private set; }
    public bool Attack4Input { get; private set; }

    #endregion

    #region Projectile
    public GameObject BarkEffect;//allows the effect to be connected in editor
    public Transform Mouth;//this is a transform point where projectiles and effects come out of
    public Rigidbody2D projectile;
    public float projectileSpeed;
    #endregion

    #region Hit
    public bool hit;//may be replaced by direct beHit function 
    public float StunTime;
    public bool IsStunned;
    #endregion

    #region Dash
    public float dashSpeed;
    public float startDashTime;
    float dashTime;//was not public
    float direction;//was not public
    public bool isDashing;
    #endregion

    private void Awake()//awake happens before any start
    {
        #region CreateStates
        //creates statemachine and states
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, "move");
        JumpState = new PlayerJumpState(this, StateMachine, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, "inair");
        LandState = new PlayerLandState(this, StateMachine, "land");

        AttackState = new PlayerAttack1State(this, StateMachine, "attack1");
        AirAttackState = new PlayerAirAttackState(this, StateMachine,"airattack1");
        Attack2State = new PlayerAttack2State(this, StateMachine, "attack2");
        Attack3State = new PlayerAttack3State(this, StateMachine, "attack3");
        Attack4State = new PlayerAttack4State(this, StateMachine, "attack4");

        HitState = new PlayerHitState(this, StateMachine, "hit");
        AirHitState = new PlayerAirHitState(this, StateMachine, "airhit");
        LaunchState = new PlayerLaunchState(this, StateMachine, "launch");
        DownState = new PlayerDownState(this, StateMachine, "down");
        GetUpState = new PlayerGetUpState(this, StateMachine, "getup");
        DeadState = new PlayerDeadState(this, StateMachine, "dead");
        StunState = new PlayerStunState(this, StateMachine, "stun");
        #endregion
    }

    private void Start()
    {
        #region initializecomponents


        CardHolder = GetComponentInParent<CardHolder>();
        MatchConnecter = GetComponentInParent<MatchConnecter>();
        health = GetComponent<Health>();
        MoveCoolDown = GetComponent<MoveCooldown>();
        Anim = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        //mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.gravityScale = 70;
        t = transform;//may be able to be removed
        //soundManager = SoundManager.instance;
        #endregion

        #region initializeVariables
        facingRight = t.localScale.x > 0;
        StateMachine.Initialize(IdleState);//change to intro state.
        dashTime = startDashTime;
        #endregion
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();//applys update logic to whatever state is active

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(Groundcheck.position, 0.2f, GroundLayer);//may be removable because of direct function?

        #region ChangeFacingDirection
        if (MoveDirection != 0) //lets you change facing direction during attacks, may need to be changed to a direct function?
        {
            if (MoveDirection > 0 && !facingRight) // left
            {
                facingRight = true;
                transform.Rotate(0f, 180f, 0f);
            }
            if (MoveDirection < 0 && facingRight) // right
            {
                facingRight = false;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        #endregion

        if (isGrounded || r2d.velocity.magnitude < 0.01f )//if grounded and starts to stop moving from movedirection becoming zero, will set it to zero as soon as it reaches a certain slowness and takes away all momentum
        {
                MoveDirection = 0;
                Debug.Log("move direction set to zero dummy");
        }
    }

    private void FixedUpdate(){ StateMachine.CurrentState.PhysicsUpdate(); }

    #region Inputs
    //inputs where orignally handled by having bools in the input handler and having the states read the bools through the player. this new system has the bools instead held directly in player and has them turned on and off by the input handler through these functions.
    public void Attack1()
    {
        if (MoveCoolDown.Atk1OnCoolDown == true)
        {
            return;
        }
        else
        {
        AttackInput = true;//change to attack 1 all throughout
        MoveCoolDown.StartAtk1Cooldown();
        }
    }

    public void Attack2()
    {
        if (MoveCoolDown.Atk2OnCoolDown == true)
        {
            return;
        }
        else
        {
            Attack2Input = true;
            MoveCoolDown.StartAtk2Cooldown();
        }
    }

    public void Attack3()
    {
        if (MoveCoolDown.Atk3OnCoolDown == true)
        {
          
            return;
        }
        else
        {
            Attack3Input = true;
            MoveCoolDown.StartAtk3Cooldown();
        }
    }

    public void Attack4()
    {
        if (MoveCoolDown.Atk4OnCoolDown == true)
        {
            return;
        }
        else
        {
            Attack4Input = true;
            MoveCoolDown.StartAtk4Cooldown();
        }
    }

    public void SetMoveDirection(float Movedirection)
    {
        MoveDirection = Movedirection;
    }
    public void Jump()
    {
        JumpInput = true;
    }

    public void UseJumpInput() => JumpInput = false;//these put the bool back to false after there used in the states.

    public void UseAttackInput() => AttackInput = false;
    public void UseAttack2Input() => Attack2Input = false;
    public void UseAttack3Input() => Attack3Input = false;
    public void UseAttack4Input() => Attack4Input = false;

    public void expireJumpInput()//DOUBLE CHECK THESE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        JumpInput = false;
    }

    public void expireAttackInput()
    {
        AttackInput = false;//this function has all attack inputs on the same timer. this means any and all attack inputs that are put in while an attack cannot be performed will not be counted after a certain amount of time.
        Attack2Input = false;
        Attack3Input = false;
        Attack4Input = false;
    }

    #endregion

    #region BaseFunctions
    public bool CheckIfGrounded()
    {
        Debug.Log("checking if grounded");
        return Physics2D.OverlapCircle(Groundcheck.position, 0.2f, GroundLayer);
    }
    public void BeHit()//CHANGE TO DO LAUCNH STATES
    {//slow time before entering hit state?
            if (isGrounded == false)
                StateMachine.ChangeState(AirHitState);
            else if (isGrounded == true)
            {
                StateMachine.ChangeState(HitState);
            }
    }

    public void BeStunned(float Stuntime)//takes in stun time from atack
    {
        StunTime = Stuntime;//relays data to variable
        IsStunned = true;
        StateMachine.ChangeState(StunState);
    }

    public IEnumerator HoldStun(){//
    Debug.Log("holding stun");

        yield return new WaitForSeconds(StunTime);//holds script here for eloted stun time
  
     Debug.Log("Stun over");
        StunTime = 0f;//once over the local variable is reset
        IsStunned = false;//is stunned is set to false which the current stun state reads and allows to the player to change states.
    }

    public void Die()//called by PlayerDeadState to disable hitboxes and input script
    {
        Debug.Log("Enemy Died!");
        soundManager.PlaySound("WolfDie");//sounds need work!!!
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sortingLayerName = default;
        gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = false;

        //tell Match Connector to start disconencting process.
        MatchConnecter.BringOffline();

        //communicate when kuro Dies

        //GetComponent<InputHandler>().enabled = false;
        //animator.SetBool("Isdead", true);
        //this.enabled = false;
    }


    #endregion

    #region AttackFunctions
    public void DoDash()
    {
            dashTime = startDashTime;//this resets the dash time
            direction = MoveDirection;//this means you dash wherever your last horizontal input was
            r2d.velocity = Vector2.zero;//this resets the velocity so the dash is consistent 
            isDashing = true;//this bool manages the length of time the dash is active
            if (facingRight == true)//this means if your not moving you will dash wherever your facing
            {
                direction = 1;
            }
            else if (facingRight == false)
            {
                direction = -1;
            }
        
        if (isDashing == true)
        {
            //gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = false;
            r2d.velocity = Vector2.right * direction * dashSpeed;//this applies the velocity
            Debug.Log("Dash ");
            dashTime -= Time.deltaTime;//this counts down

            if (dashTime <= 0)//this exits the dash when the countdown ends
            {
                //gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = true;
                isDashing = false;

            }
        }
        
    }
    public void ShootBolt()
    {
        //soundManager.PlaySound("Bark");
        Rigidbody2D projectileinstance;//create a temporary rigid body to hold output of instantiate function
        projectileinstance = Instantiate(projectile, Mouth.position, transform.rotation) as Rigidbody2D;//instantiates a selected rigid body, but also brings rest of prefab
        projectileinstance.AddForce(Mouth.right * projectileSpeed, ForceMode2D.Impulse);//since prefab was instantiated as a rigidbody, it is able to have rigid body class functions applied to it like addforce
    }
    public void Barkfx()//may be removable as its just visual effects
    {
        soundManager.PlaySound("Bark");
        GameObject effect = Instantiate(BarkEffect, Mouth.position, transform.rotation);
            Destroy(effect, .291f);
    }

    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();//passes in events from animator to whatever state is currently active
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
