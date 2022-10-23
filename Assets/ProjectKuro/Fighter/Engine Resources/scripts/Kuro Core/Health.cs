using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public CardHolder CardHolder { get; private set; }
    public KuroCore Core { get; private set; }

    public string CoreToGet = "RigoCore";

    public int currentHealth;
    public HealthBar healthBar; // UI

    int damage;//damage int holds the output of the damage calculation

    private TextMeshPro DamageText;

    void Awake()//was awake
    {
        CardHolder = GetComponentInParent<CardHolder>();
        Core = GetComponent<KuroCore>();//change this eventually to intake variable

        //Debug.Log("loading current health from cardholder");
        currentHealth = CardHolder.KuroData.CurrHP;

        DamageText = transform.parent.Find("DamageText").GetComponent<TextMeshPro>();
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

        damage = ((((Attack / CardHolder.KuroData.DEF) * MovePower * (Level / 5)) / 15)); // * modifiers)

        //doknockback vs weight = hit vs launch state. 

        Core.BeHit();//send hit signal to player with knockback and hitstun data to enter hit and then knocked states.

        //apply particles effect, sound, screen kick.

        currentHealth -= damage;//turn this into damage calculation taking in attack power, typing, from enemy. and defense stat, defense typing, of player to finally take away from the players hp
        healthBar.SetHealth(currentHealth); // HealthBar UI

        DamageText.text = damage.ToString(); //Display damage taken
        DamageText.gameObject.SetActive(true); //Show damage text, which is turned off by DelayedSetInactive.cs

        if (currentHealth <= 0)//if an attack kills, sets state to dead
        {
            Debug.Log(Core.StateMachine.CurrentState.animBoolName);
            if(Core.StateMachine.CurrentState.animBoolName == "launch"){ //Checks if died while in the air
                Core.StateMachine.ChangeState(Core.DeadInAirState); //Used to change state to dying while in the air
            }
            else{
                Core.StateMachine.ChangeState(Core.DeadState);//Used to change state to dying while grounded
            }
            
        }

    }

}
