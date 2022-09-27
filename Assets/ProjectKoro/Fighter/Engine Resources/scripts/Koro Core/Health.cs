using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public CardHolder CardHolder { get; private set; }
    public KoroCore Core { get; private set; }

    public string CoreToGet = "RigoCore";

    public int currentHealth;
    public HealthBar healthBar; // UI

    int damage;//damage int holds the output of the damage calculation

    void Awake()//was awake
    {
        CardHolder = GetComponentInParent<CardHolder>();
        Core = GetComponent<KoroCore>();//change this eventually to intake variable

        //Debug.Log("loading current health from cardholder");
        currentHealth = CardHolder.KoroData.CurrHP;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(140, 70, 50);
        }
    }

    public void LoadHealthBar(HealthBar HealthBar)//this is called by the match connecter when connecting and passes in the playebrains UI
    {
        healthBar = HealthBar;
        if(healthBar == null)
        {
            //Debug.LogWarning("Healthbar not detected.");
            return;
        }
        else
        {
            //Debug.Log("Loading Healthbar");
       healthBar.SetMaxHealth(currentHealth); // UI

        }
    }

    public void UnloadHealthBar()
    {
        healthBar = null;
    }

    public void TakeDamage(int Attack, int MovePower, int Level)//called by enemy attack hitbox to initiate all the things that need to happen when getting hit. starts off with a hitstop and calculations. then send behit signal and lastly applies damage and effectts
    {
        FindObjectOfType<HitStop>().Stop(0.01f);//send hitstop signal to gamemanager or stop it here

        //type effectiveness calculation

        damage = ((((Attack / CardHolder.KoroData.DEF) * MovePower * (Level / 5)) / 15)); // * modifiers)

        //doknockback vs weight = hit vs launch state. 

        Core.BeHit();//send hit signal to player with knockback and hitstun data to enter hit and then knocked states.

        //apply particles effect, sound, screen kick.

        currentHealth -= damage;//turn this into damage calculation taking in attack power, typing, from enemy. and defense stat, defense typing, of player to finally take away from the players hp
        healthBar.SetHealth(currentHealth); // HealthBar UI

        if (currentHealth <= 0)//if an attack kills, sets state to dead
        {

            Core.StateMachine.ChangeState(Core.DeadState);//fix this to compensate for dying in air or residual damage
        }

    }

}
