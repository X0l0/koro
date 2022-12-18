using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    //cardholder
    [SerializeField] public CardHolder CardHolder;

    //Move Details
    public int MovePower = 1;

    //trajectory
    [SerializeField] 
    private float thrust;
    public int upwardfactor;

    //fx
    public GameObject HitEffect;//hit effect prefab to be spawned
    private Vector3 fx;//the location of where to instantiate the prefab


    //hitconfirm 
    //private bool hitboxactive;
    private Collider2D Hitbox;

    private RigoCore User;

    private void Start()
    {
        Hitbox = GetComponent<Collider2D>();//connects to respective collider for easy acess
        Hitbox.enabled = false;//makes the collider negative to begin with, control hitbox activation by the collider not the entire game object.
        //hitboxactive = false;
        User = this.gameObject.transform.parent.gameObject.GetComponent<RigoCore>();
    }

    private void OnTriggerEnter2D(Collider2D collision)//on the attack trigger hitbox colliding with the enemies hitbox
    {
        if (User.CompareTag("Player") && collision.CompareTag("Enemy") || User.CompareTag("Enemy") && collision.CompareTag("Player"))//hits Enemy if Player, and vice versa
        {
            //get variables and calculate direction
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();//connects to whatever is colliding's rigidbody and names it enemy 
            Vector2 direction = (new Vector2(enemy.transform.position.x, enemy.transform.position.y) - new Vector2(transform.position.x, transform.position.y + upwardfactor)).normalized;//figures out the knockback direction from both players positions and other variables
            
            //hit fx
            fx = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            hitfx();
            
            //apply damage then knockback
            enemy.AddForce(direction * thrust, ForceMode2D.Impulse);//applies direction and thrust to enemies rigid body via impulse.
            collision.GetComponent<Health>().TakeDamage(CardHolder.KuroData.ATTACK, MovePower, CardHolder.KuroData.LVL);

            DeActivateHitBox();

        }
    }

    private void hitfx()
    {

        /*GameObject effect = Instantiate(HitEffect, fx, transform.rotation);//instantiates hit effect at calculated fx location
        Destroy(effect, .222f);//destroys effect after a certain amount of time.
        */
        //HitStop.instance.Stop(.1f);

        HitEffect.transform.position = fx; //Set FX position to where the collision occurred
        HitEffect.SetActive(true); //Set FX active; will become inactive again via DelayedSetInactive.cs
    }

    public void ActivateHitBox()//called by state scripts and core when fighting
    {
        Debug.Log("activating hitbox");
        //hitboxactive = true;
        Hitbox.enabled = true;
    }

    public void DeActivateHitBox()//called by hitbox after hitconfirm
    {
        Debug.Log("Deactivating hitbox");
        //hitboxactive = false;
        Hitbox.enabled = false;
    }

}
