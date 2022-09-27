using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2hit : MonoBehaviour
{
    [SerializeField] private float thrust;
    //[SerializeField] public PlayerData playerData;
    [SerializeField] public CardHolder CardHolder;

    //public Player player { get; private set; }

    public GameObject Player_2;
    public int MovePower = 40;
    public int upwardfactor;


    // Start is called before the first frame update
    void Start()
    {
        //player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
          //  if (enemy != null)
           // {

                
                //GameObject.Find("Player_1").GetComponent<Player>().hit = true;//
                collision.GetComponent<Health>().TakeDamage(CardHolder.KuroData.ATTACK, MovePower, CardHolder.KuroData.LVL);

                
                //Debug.Log(collision.transform.position);
                //Player_2.transform.position = moveDirection;//COMMAND THAT TELEPORTS ENEMY

                //StartCoroutine(KnockCoroutine(enemy));//BOUNCES UP VERSION

                Vector2 direction = (new Vector2(enemy.transform.position.x, enemy.transform.position.y) - new Vector2(transform.position.x, transform.position.y + upwardfactor)).normalized;
                //enemy.velocity = new Vector2(knockbackDir.x * thrust, knockbackDir.y * thrust);

                //Vector2 direction = enemy.transform.position - transform.position;//V1 WAY OF DOING IT
                //direction.y = 0;
                enemy.AddForce(direction * thrust, ForceMode2D.Impulse);

                // Vector2 difference = (transform.position - collision.transform.position).normalized;//V2 WAY OF DOING IT
                // Vector2 force = difference * thrust;
                // rb.AddForce(force, ForceMode2D.Impulse);


            //}
            //else
           // {
                //isHit = false;
               // GameObject.Find("Player_1").GetComponent<CharacterController2D>().hit = isHit;
            //}


        }
    }


    private IEnumerator KnockCoroutine(Rigidbody2D enemy)
    {
        Vector2 forceDirection = enemy.transform.position - transform.position;
        Vector2 force = forceDirection.normalized * thrust;

        enemy.velocity = force;
        yield return new WaitForSeconds(.3f);

        enemy.velocity = new Vector2();//SETS VELOCITY TO ZERO AFTER .3 SECONDS
    }
}
