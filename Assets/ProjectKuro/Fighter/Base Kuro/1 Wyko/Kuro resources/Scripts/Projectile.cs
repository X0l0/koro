using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float thrust;// set thrust from editor
    [SerializeField] private float Lifespan;
    [SerializeField] private KuroCore User;
    [SerializeField] private int MovePower;

    public int upwardfactor;
    private SoundManager soundManager;

    public GameObject HitEffect;
    private Vector3 fx;

    private CapsuleCollider2D ProjCollider;
    private SpriteRenderer ProjSprite;
    private Rigidbody2D ProjRB;

    private bool IsActive;
    private float ActiveLifespan;

    private void Start()
    {
        ProjCollider = GetComponent<CapsuleCollider2D>();
        ProjSprite = GetComponent<SpriteRenderer>();
        ProjRB = GetComponent<Rigidbody2D>();
        soundManager = User.soundManager;

        IsActive = false;
    }

    private void Update()
    {
        if (IsActive)//if ever active
        {
            if (ActiveLifespan > 0)//and as long as the lifespan isnt 0
            {
                ActiveLifespan -= Time.deltaTime;//counts lifespan down
            }
            else//if lifespan is ever 0 aka runs out
            {
                //ActiveLifespan = 0;//sets to zero to stop from being negative
                BecomeInactive();
                //Debug.Log("Time has run out!");
                //IsActive = false;//sets is active to false
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//on the attack trigger hitbox colliding with the enemies hitbox
    {
        if (collision.CompareTag("Enemy"))//compares tag for enemy, might be removable because of layering?
        {
            //connect variables and calculates direction
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();//connects to whatever is colliding's rigidbody and names it enemy 
            Vector2 direction = (new Vector2(enemy.transform.position.x, enemy.transform.position.y) - new Vector2(transform.position.x, transform.position.y + upwardfactor)).normalized;//figures out the knockback direction from both players positions and other variables
                                 
            //hitfx
            fx = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);//uses enemy position to figure out where to put fx
            hitfx();

            //attack and knockback
            collision.GetComponent<Health>().TakeDamage(User.CardHolder.KuroData.ATTACK, MovePower, User.CardHolder.KuroData.LVL);//finds enemies health script and applies damage value.
            enemy.AddForce(direction * thrust, ForceMode2D.Impulse);//applies direction and thrust to enemies rigid body via impulse.


            BecomeInactive();

            //Debug.Log("projectile hit enemy");

        }
        else if (collision.CompareTag("Walls"))//plays fx and destroys self if hitting a wall
        {
            fx= new Vector3(transform.position.x, transform.position.y, transform.position.z);
            hitfx();
            //Destroy(gameObject);
            BecomeInactive();

            //Debug.Log("projectile hit wall");
        }
    }

    private void hitfx()
    {

        //soundManager.PlaySound("Shock");//plays hit sound if contact is made

        GameObject effect = Instantiate(HitEffect, fx, transform.rotation);
        Destroy(effect, .222f);//destroys effect after set amount of time.

    }


    public void BecomeActive()
    {
        ProjRB.velocity = Vector2.zero;
        ProjCollider.enabled = true;
        ProjSprite.enabled = true;
        ProjRB.simulated = true;

        ActiveLifespan = Lifespan;
        IsActive = true;

        Debug.Log("FLAg");
        //Debug.Log("projectile set to active");
        //StartCoroutine("ProjectileLifeSpan");
    }

    private void BecomeInactive()
    {
        IsActive = false;
        ActiveLifespan = Lifespan;
        ProjCollider.enabled = false;
        ProjSprite.enabled =false;
        ProjRB.simulated = false;
        ProjRB.velocity = Vector2.zero;

        Debug.Log("FLAg");
        //move to specific spot?
    }

}
