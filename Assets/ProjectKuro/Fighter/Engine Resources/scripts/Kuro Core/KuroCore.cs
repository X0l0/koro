using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region requiredcomponents
//[RequireComponent(typeof(CapsuleCollider2D))]//require component help against startup errors
//[RequireComponent(typeof(Rigidbody2D))]
#endregion

public class KuroCore : MonoBehaviour//attaches to game object to create states and statemachine
{

    public CardHolder CardHolder { get; private set; }
    public MatchConnecter MatchConnecter { get; private set; }

    public KuroCore BaseCore;//this is filled by itself so individual cores can properly read it, used in attack states


    #region States
    //get private set seems to be able to connect scripts to one another quite easily, extra research into the specifics of it may be required but these are scripts and variables that need to be communicated 
    public StateMachine StateMachine { get; private set; }//creates state machine for player object
    public IdleState IdleState { get; private set; }//creates idle state
    public MoveState MoveState { get; private set; }//creates move state
    public JumpState JumpState { get; private set; }
    public InAirState InAirState { get; private set; }
    public LandState LandState { get; private set; }
    public TurnState TurnState { get; private set; }


    public HitState HitState { get; private set; }
    public LaunchState LaunchState { get; private set; }
    public DownState DownState { get; private set; }
    public GetUpState GetUpState { get; private set; }
    public DieState DeadState { get; private set; }//naming not consistent may need to be changed later
    //public StunState StunState { get; private set; }

    #endregion

    #region othercomponents
    //making these get private set and then connecting them in start lets you reference across them. however doing so here may be uneccessary as these scripts are more likely to send signals to player then rather recieve them from player.

    public SoundManager soundManager;
    public Health health { get; private set; }
    public MoveCooldown MoveCoolDown { get; private set; }
    public Animator Anim { get; private set; }//animator
    public Rigidbody2D r2d { get; private set; }//was not public
    public Transform t;//could be private?

    #endregion

    #region Movement
    public bool facingRight = true;
    public float FacingDirection;
    public float MoveDirection;
    public bool isGrounded;//was not public
    public Transform Groundcheck;
    public LayerMask GroundLayer;
    public int MaxSpeed;//70
    public int JumpHeight;//140
    #endregion

    #region Inputs
    public bool JumpInput { get; private set; }
    public bool isDownPressed { get; private set; }
    public bool AttackInput { get; private set; }
    public bool Attack2Input { get; private set; }
    public bool Attack3Input { get; private set; }
    public bool Attack4Input { get; private set; }

    #endregion



    public bool DoATK1 = false;//bool used in communicatin when to attack between base core states and individual states.
    public bool DoATK2 = false;


    public Transform Mouth;//this is a transform point where projectiles and effects come out of

  
    #region Hit
    public bool hit;//may be replaced by direct beHit function 
    public float StunTime;
    public bool IsStunned;
    [SerializeField] private GameObject DownEffect;
    #endregion

    //#region Dash
    //public float dashSpeed;
    //public float startDashTime;
    //float dashTime;//was not public
    //float direction;//was not public
    //public bool isDashing;
    //#endregion

    private void Awake()//awake happens before any start
    {

        BaseCore = this;
        #region CreateStates
        //creates statemachine and states
        StateMachine = new StateMachine();
        IdleState = new IdleState(this, StateMachine, "idle");
        MoveState = new MoveState(this, StateMachine, "move");
        JumpState = new JumpState(this, StateMachine, "jump");
        InAirState = new InAirState(this, StateMachine, "inairup");
        LandState = new LandState(this, StateMachine, "land");
        TurnState = new TurnState(this, StateMachine, "turn");

        HitState = new HitState(this, StateMachine, "hit");
        LaunchState = new LaunchState(this, StateMachine, "launch");
        DownState = new DownState(this, StateMachine, "down");
        GetUpState = new GetUpState(this, StateMachine, "getup");
        DeadState = new DieState(this, StateMachine, "dead");
        //StunState = new PlayerStunState(this, StateMachine, "stun");
        #endregion
    }

    public void Start()
    {
        #region initializecomponents

        CardHolder = GetComponentInParent<CardHolder>();
        MatchConnecter = GetComponentInParent<MatchConnecter>();

        soundManager = GetComponent<SoundManager>();

        health = GetComponent<Health>();
        MoveCoolDown = GetComponent<MoveCooldown>();

        Anim = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
      
        r2d.gravityScale = 60;
        t = transform;//may be able to be removed
        #endregion

        #region initializeVariables
        facingRight = t.localScale.x > 0;
        StateMachine.Initialize(IdleState);//change to intro state.
        #endregion
    }

    public void Update()
    {

        StateMachine.CurrentState.LogicUpdate();//applys update logic to whatever state is active

        // Ground Check
        isGrounded = Physics2D.OverlapCircle(Groundcheck.position, 0.2f, GroundLayer);

        if (isGrounded || r2d.velocity.magnitude < 10f)//if grounded and starts to stop moving from movedirection becoming zero, will set it to zero as soon as it reaches a certain slowness and takes away all momentum
        {
            MoveDirection = 0;

        }
    }

    public void ChangeFacingDirection()
    {

       #region ChangeFacingDirection
        if (MoveDirection != 0) //lets you change facing direction during attacks, may need to be changed to a direct function?
        {
            if (MoveDirection > 0 && !facingRight) // left
            {
                facingRight = true;
                FacingDirection = 1;
                transform.Rotate(0f, 180f, 0f);
            }
            if (MoveDirection < 0 && facingRight) // right
            {
                facingRight = false;
                FacingDirection = -1;
                transform.Rotate(0f, 180f, 0f);
            }
        }
        #endregion
    }

    public void ChangeDirection()
    {
        if (facingRight)//switches to left
        {
            facingRight = false;
            FacingDirection = -1;
            transform.Rotate(0f, 180f, 0f);
        }
        else if (!facingRight)//switches to right
        {
            facingRight = true;
            FacingDirection = 1;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void FixedUpdate() { StateMachine.CurrentState.PhysicsUpdate(); }

    #region Inputs
    //inputs where orignally handled by having bools in the input handler and having the states read the bools through the player. this new system has the bools instead held directly in player and has them turned on and off by the input handler through these functions.
    public void Attack1()//called by input handler
    {
        if (MoveCoolDown.Atk1OnCoolDown == true)// if on cooldown do nothing
        {
            return;
        }
        else//register attack input, is held for .2 seconds then is set back to false. this is read by certain states only.
        {
            AttackInput = true;//change to attack 1 all throughout
            //MoveCoolDown.StartAtk1Cooldown();
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
            Debug.Log("FLAg");
            //MoveCoolDown.StartAtk2Cooldown();
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

    public void SetMoveDirection(float Movedirection)//called by input handler constantly to decide move direction 
    {
        MoveDirection = Movedirection;
        //Debug.Log("movedirection detected");
    }
    public void Jump()
    {
        JumpInput = true;
    }

    public void DownIsPressed(bool isdownpressed)
    {
        isDownPressed = isdownpressed;
        //Debug.Log("isdownpressed = " + isdownpressed);
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

    public void BeHit()//CHANGE TO DO LAUCNH STATES
    {//slow time before entering hit state?
        if (isGrounded == false)
            StateMachine.ChangeState(LaunchState);
        else if (isGrounded == true)
        {
            StateMachine.ChangeState(HitState);
        }
    }

    public void DownFX()
    {
    GameObject effect = Instantiate(DownEffect, t.position, transform.rotation);//instantiates hit effect at calculated fx location
    Destroy(effect, .222f);//destroys effect after a certain amount of time.

    }

    public void BeStunned(float Stuntime)//takes in stun time from atack
    {
        StunTime = Stuntime;//relays data to variable
        IsStunned = true;
        StateMachine.ChangeState(HitState);//CHANGE TO STUN 
    }

    public IEnumerator HoldStun()
    {//
        //Debug.Log("holding stun");

        yield return new WaitForSeconds(StunTime);//holds script here for eloted stun time

        //Debug.Log("Stun over");
        StunTime = 0f;//once over the local variable is reset
        IsStunned = false;//is stunned is set to false which the current stun state reads and allows to the player to change states.
    }

    public void Die()//called by PlayerDeadState to disable hitboxes and input script
    {
        //Debug.Log("Enemy Died!");
        //soundManager.PlaySound("WolfDie");//sounds need work!!!
        GetComponent<Collider2D>().enabled = false;//removes hitbox
        GetComponent<SpriteRenderer>().sortingLayerName = default;//idk double check what this does?
        gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = false;//removes pushbox

        //communicate when kuro Dies
        MatchConnecter.KuroDead();

        ////tell Match Connector to start disconencting process.
        //MatchConnecter.BringOffline();


        //GetComponent<InputHandler>().enabled = false;
        //animator.SetBool("Isdead", true);
        //this.enabled = false;
    }


    #endregion

    #region AttackFunctions

    public void DoAttack1()
    {
        DoATK1 = true;
    }

    public void DoAttack2()
    {
        DoATK2 = true;
        Debug.Log("FLAg");
    }
    //public void DoDash()
    //{
    //    dashTime = startDashTime;//this resets the dash time
    //    direction = MoveDirection;//this means you dash wherever your last horizontal input was
    //    r2d.velocity = Vector2.zero;//this resets the velocity so the dash is consistent 
    //    isDashing = true;//this bool manages the length of time the dash is active
    //    if (facingRight == true)//this means if your not moving you will dash wherever your facing
    //    {
    //        direction = 1;
    //    }
    //    else if (facingRight == false)
    //    {
    //        direction = -1;
    //    }

    //    if (isDashing == true)
    //    {
    //        //gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = false;
    //        r2d.velocity = Vector2.right * direction * dashSpeed;//this applies the velocity
    //        Debug.Log("Dash ");
    //        dashTime -= Time.deltaTime;//this counts down

    //        if (dashTime <= 0)//this exits the dash when the countdown ends
    //        {
    //            //gameObject.transform.Find("playerpushbox").GetComponent<Collider2D>().enabled = true;
    //            isDashing = false;

    //        }
    //    }

    //}

   // public void Barkfx()//may be removable as its just visual effects
   // {
        //soundManager.PlaySound("Bark");
        //GameObject effect = Instantiate(BarkEffect, Mouth.position, transform.rotation);
        //Destroy(effect, .291f);
    //}

    #endregion

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();//passes in events from animator to whatever state is currently active
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
