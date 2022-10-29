using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigoCore : KuroCore
{
    //in theory specific cores will inherit from a base kuro core class
    //the base class will handle shared attributes like similar states, connections to other scripts, inputs, similar 
    //required components, similar base functions like switch direction, ground check etc.

    //the specific cores would probably handle things like unique values, unique functions usually relating to attack, 
    //it would hold unique states relating to attack or other things. unique assets like sounds and when to play them. etc

    //attack1chrgState
    public Attack1CharState Attack1CharState { get; private set; }
    public Attack1NState Attack1NState { get; private set; }
    public Attack1AngState Attack1AngState { get; private set; }
    public Attack1AerState Attack1AerState { get; private set; }

    public Attack2CharState Attack2CharState { get; private set; }
    public Attack2NState Attack2NState { get; private set; }
    public Attack2AngState Attack2AngState { get; private set; }
    public Attack2AerState Attack2AerState { get; private set; }
    

    //public PlayerAttack3State Attack3State { get; private set; }
    //public PlayerAttack4State Attack4State { get; private set; }


    #region hitboxes
    //these must be connected during base designing.
    public HitBox HB1;
    public HitBox HB2;
    //public HitBox HB1;
    //public HitBox HB1;

    #endregion
    [SerializeField] GameObject eyelazer;

    public Rigidbody2D eyelazerRB;
    private Projectile RigoProjectile;
    public float projectileSpeed;

    public new void Start()
    {
        base.Start();
        Attack1CharState = new Attack1CharState(this, StateMachine, "chargeattack1");
        Attack1NState = new Attack1NState(this, StateMachine, "neutralattack1");
        Attack1AngState = new Attack1AngState(this, StateMachine, "angledattack1");
        Attack1AerState = new Attack1AerState(this, StateMachine, "aerialattack1");

        Attack2CharState = new Attack2CharState(this, StateMachine, "chargeattack2");
        Attack2NState = new Attack2NState(this, StateMachine, "neutralattack2");
        Attack2AngState = new Attack2AngState(this, StateMachine, "angledattack2");
        Attack2AerState = new Attack2AerState(this, StateMachine, "aerialattack2");

        RigoProjectile = eyelazer.GetComponent<Projectile>();//gets the projectile script of slotted in projectile
        eyelazerRB = eyelazer.GetComponent<Rigidbody2D>();

        //RigoProjectile.DeemUser(this);//fills projectile script with this.

        //Attack3State = new PlayerAttack3State(this, StateMachine, "attack3");
        //Attack4State = new PlayerAttack4State(this, StateMachine, "attack4");

    }

    public new void Update()//uses new to override base update
    {
        base.Update();//base update is then called anyway.
        if (DoATK1 == true)
        {
            if (!isGrounded)
            {
            StateMachine.ChangeState(Attack1AerState);
            }
            else
            {
            StateMachine.ChangeState(Attack1CharState);
            }
        }
        if (DoATK2 == true)
        {
            if (!isGrounded)
            {
                StateMachine.ChangeState(Attack2AerState);
            }
            else
            {
                StateMachine.ChangeState(Attack2CharState);

            }
        }
    }


    public void EyeLazer()
    {
        //Debug.Log("eye lazer fired");
        //relocates it
        eyelazer.transform.position = Mouth.transform.position;
        eyelazer.transform.rotation = Mouth.rotation;

        //sets it active
        RigoProjectile.BecomeActive();

        //fires it.
        eyelazerRB.AddForce(Mouth.right * projectileSpeed, ForceMode2D.Impulse);

        soundManager.PlaySound("Eyelazer");

    }

    #region Hitboxes
    public void ActivateHB1()//called by attack states to activate and de activate respective hitboxes
    {
        //Debug.Log("signal from state received forwarding to hitbox.");
        HB1.ActivateHitBox();
    }

    public void DeActivateHB1()
    {
        //Debug.Log("signal from state received forwarding to hitbox.");
        HB1.DeActivateHitBox();
    }
    //public void ActivateHB2()
    //{

    //}
    #endregion
}
