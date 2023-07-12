using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shocklock : MonoBehaviour
{
   // [SerializeField] private float thrust;// set thrust from editor
    //[SerializeField] public PlayerData playerData;

    //public GameObject Player_2;// get player 2?
   // public int upwardfactor;
    private KuroSoundManager soundManager;

    //public Transform user;
   // public GameObject HitEffect;

    private void Start()
    {
        //player = GetComponent<Player>();
        //soundManager = KuroSoundManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)//on the attack trigger hitbox colliding with the enemies hitbox
    {
        if (collision.CompareTag("Enemy"))//compares tag for enemy, might be removable because of layering?
        {
           // Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();//connects to whatever is colliding's rigidbody and names it enemy 
                                                                      //if (enemy != null)//if the enemys hitbox is active, does the next things
                                                                      
            //collision.GetComponent<Player>().BeStunned(1f);//finds enemies health script and applies damage value. temporary disabled for cleaning

           //Vector2 direction = (new Vector2(enemy.transform.position.x, enemy.transform.position.y) - new Vector2(transform.position.x, transform.position.y + upwardfactor)).normalized;//figures out the knockback direction from both players positions and other variables
           // enemy.AddForce(direction * thrust, ForceMode2D.Impulse);//applies direction and thrust to enemies rigid body via impulse.

            Debug.Log("stun applied");
            //StartCoroutine("hitconfirm");

            GetComponent<CircleCollider2D>().enabled = false;//this turns off the hit box as a type of hit confirm, it is later turned back on by the state scripts upon entry.
            //hitboxactive = false;

        }
    }

}
