using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;

public class MoveCooldown : MonoBehaviour
{
    //this script has alot of repeating code, the use of arrays or something may be useful in refining it. Keeping things seperate is important however as each attack has its own individual cooldown.
    //public Player player { get; private set; }//may be uneccesary as this script takes input from player but doesnt send any direct out put, it is only read.

    [Header("Attack 1")]//eventually the graphics of the cooldowns would need to be attached to the kuro and be able to be loaded in and out. the other types of cooldowns will also need to be implemented.
    //attack 1 variables
    [SerializeField]
    private Image Atk1imageCooldown;//image that covers graphic and dissapears with time
    [SerializeField]
    private TMP_Text Atk1textCooldown;//the number showing how many seconds are left
    [SerializeField]//may be removable as it only needs to be read? 
    public bool Atk1OnCoolDown = false;//shows whether or not cooldown is active, was private but needed to be public
    private float Atk1cooldownTime = 0.2f;//shows how long cooldown is, make it so takes in from player.playerdata?
    private float Atk1cooldownTimer = 0.0f;//holds cooldown time as it counts down.

    [Header("Attack 2")]
    //attack 2 variables
    [SerializeField]
    private Image Atk2imageCooldown;
    [SerializeField]
    private TMP_Text Atk2textCooldown;
    [SerializeField]
    public bool Atk2OnCoolDown = false;//shows whether or not cooldown is active
    private float Atk2cooldownTime = .2f;//shows how long cooldown is, make it so takes in from player.playerdata?
    private float Atk2cooldownTimer = 0.0f;//holds cooldown time as it counts down.

    [Header("Attack 3")]
    //attack 3 variables
    [SerializeField]
    private Image Atk3imageCooldown;
    [SerializeField]
    private TMP_Text Atk3textCooldown;
    [SerializeField]
    public bool Atk3OnCoolDown = false;//shows whether or not cooldown is active
    private float Atk3cooldownTime = 5.0f;//shows how long cooldown is, make it so takes in from player.playerdata?
    private float Atk3cooldownTimer = 0.0f;//holds cooldown time as it counts down.

    [Header("Attack 4")]
    //attack 4 variables
    [SerializeField]
    private Image Atk4imageCooldown;
    [SerializeField]
    private TMP_Text Atk4textCooldown;
    [SerializeField]
    public bool Atk4OnCoolDown = false;//shows whether or not cooldown is active
    private float Atk4cooldownTime = 2.0f;//shows how long cooldown is, make it so takes in from player.playerdata?
    private float Atk4cooldownTimer = 0.0f;//holds cooldown time as it counts down.



    void Start()
    {

    }

    public void LoadCoolDownUI(Image Atk1Image,TMP_Text Atk1Text, Image Atk2Image, TMP_Text Atk2Text, Image Atk3Image, TMP_Text Atk3Text, Image Atk4Image, TMP_Text Atk4Text )//this is called by the match connector and passes in the UI Move slots
    {
        Atk1imageCooldown = Atk1Image;
        Atk1textCooldown = Atk1Text;

        Atk2imageCooldown = Atk2Image;
        Atk2textCooldown = Atk2Text;

        Atk3imageCooldown = Atk3Image;
        Atk3textCooldown = Atk3Text;

        Atk4imageCooldown = Atk4Image;
        Atk4textCooldown = Atk4Text;


        Atk1textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk1imageCooldown.fillAmount = 0.0f;

        Atk2textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk2imageCooldown.fillAmount = 0.0f;

        Atk3textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk3imageCooldown.fillAmount = 0.0f;

        Atk4textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk4imageCooldown.fillAmount = 0.0f;
    }

    public void UnloadCoolDownUI()
    {
        Atk1textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk1imageCooldown.fillAmount = 0.0f;

        Atk2textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk2imageCooldown.fillAmount = 0.0f;

        Atk3textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk3imageCooldown.fillAmount = 0.0f;

        Atk4textCooldown.gameObject.SetActive(false);//sets cooldown image and number to zero and inactive as when starting all moves are fully charged.
        Atk4imageCooldown.fillAmount = 0.0f;


        Atk1imageCooldown = null;
        Atk1textCooldown = null;

        Atk2imageCooldown = null;
        Atk2textCooldown = null;

        Atk3imageCooldown = null;
        Atk3textCooldown = null;

        Atk4imageCooldown = null;
        Atk4textCooldown = null;

        //reset cooldowns?
    }

    void Update()
    {//if cooldown is true, applys cooldown function which counts down and updates graphic
        if (Atk1OnCoolDown) {  Atk1CountDown();  }
        if (Atk2OnCoolDown) { Atk2CountDown(); }
        if (Atk3OnCoolDown) { Atk3CountDown(); }
        if (Atk4OnCoolDown) { Atk4CountDown(); }
    }

    #region Attack Countdowns
    void Atk1CountDown()//this is called in update and it counts down the time but also updates the graphic
    {
        //subtract time since last called
        Atk1cooldownTimer -= Time.deltaTime;

        if (Atk1cooldownTimer < 0.0f)//this is if the timer reaches 0, aka the move finishes cooldown.
        {
            Atk1OnCoolDown = false;//sets it back to false.
            Atk1textCooldown.gameObject.SetActive(false);//turns off text.
            Atk1imageCooldown.fillAmount = 0.0f;//lowers and keeps the fill amount to 0
        }
        else//this is called every frame the cooldown isnt done and updates the graphic.
        {
            Atk1textCooldown.text = Mathf.RoundToInt(Atk1cooldownTimer).ToString(); //this fills in the numver on the cooldown with the rounded up amount of time left.
            Atk1imageCooldown.fillAmount = Atk1cooldownTimer / Atk1cooldownTime;//this fills the image by dividing the cooldown timer by the cooldown time.
        }
    }

    void Atk2CountDown()//this is called in update and it counts down the time but also updates the graphic
    {
        //subtract time since last called
        Atk2cooldownTimer -= Time.deltaTime;

        if (Atk2cooldownTimer < 0.0f)//this is if the timer reaches 0, aka the move finishes cooldown.
        {
            Atk2OnCoolDown = false;//sets it back to false.
            Atk2textCooldown.gameObject.SetActive(false);//turns off text.
            Atk2imageCooldown.fillAmount = 0.0f;//lowers and keeps the fill amount to 0
        }
        else//this is called every frame the cooldown isnt done and updates the graphic.
        {
            Atk2textCooldown.text = Mathf.RoundToInt(Atk2cooldownTimer).ToString(); //this fills in the numver on the cooldown with the rounded up amount of time left.
            Atk2imageCooldown.fillAmount = Atk2cooldownTimer / Atk2cooldownTime;//this fills the image by dividing the cooldown timer by the cooldown time.
        }
    }

    void Atk3CountDown()//this is called in update and it counts down the time but also updates the graphic
    {
        //subtract time since last called
        Atk3cooldownTimer -= Time.deltaTime;

        if (Atk3cooldownTimer < 0.0f)//this is if the timer reaches 0, aka the move finishes cooldown.
        {
            Atk3OnCoolDown = false;//sets it back to false.
            Atk3textCooldown.gameObject.SetActive(false);//turns off text.
            Atk3imageCooldown.fillAmount = 0.0f;//lowers and keeps the fill amount to 0
        }
        else//this is called every frame the cooldown isnt done and updates the graphic.
        {
            Atk3textCooldown.text = Mathf.RoundToInt(Atk3cooldownTimer).ToString(); //this fills in the numver on the cooldown with the rounded up amount of time left.
            Atk3imageCooldown.fillAmount = Atk3cooldownTimer / Atk3cooldownTime;//this fills the image by dividing the cooldown timer by the cooldown time.
        }
    }

    void Atk4CountDown()//this is called in update and it counts down the time but also updates the graphic
    {
        //subtract time since last called
        Atk4cooldownTimer -= Time.deltaTime;

        if (Atk4cooldownTimer < 0.0f)//this is if the timer reaches 0, aka the move finishes cooldown.
        {
            Atk4OnCoolDown = false;//sets it back to false.
            Atk4textCooldown.gameObject.SetActive(false);//turns off text.
            Atk4imageCooldown.fillAmount = 0.0f;//lowers and keeps the fill amount to 0
        }
        else//this is called every frame the cooldown isnt done and updates the graphic.
        {
            Atk4textCooldown.text = Mathf.RoundToInt(Atk4cooldownTimer).ToString(); //this fills in the numver on the cooldown with the rounded up amount of time left.
            Atk4imageCooldown.fillAmount = Atk4cooldownTimer / Atk4cooldownTime;//this fills the image by dividing the cooldown timer by the cooldown time.
        }
    }
    #endregion

    #region Start Cooldowns
    public void StartAtk1Cooldown()//this is called when the move is used and it begins the process of cooldown by setting it to true, filling the timer, and turning on the number graphic.
    {
        if (Atk1OnCoolDown)//if this command is called again while cooldown is active, returns nothing and or can be programmed to make a fail noise. this may be able to be left empty as player inputs will check the bool before being sent .
        {
            //return false;
            //put in error noise 
        }
        else
        {
            Atk1OnCoolDown = true;//this means the move is now on cool down
            Atk1textCooldown.gameObject.SetActive(true);//this turns on the text
            Atk1cooldownTimer = Atk1cooldownTime;//this fills in the timer with the eloted cooldown amount
            //return true;
        }
    }

    public void StartAtk2Cooldown()//this is called when the move is used and it begins the process of cooldown by setting it to true, filling the timer, and turning on the number graphic.
    {
        if (Atk2OnCoolDown)//if this command is called again while cooldown is active, returns nothing and or can be programmed to make a fail noise. this may be able to be left empty as player inputs will check the bool before being sent .
        {
            //return false;
            //put in error noise 
        }
        else
        {
            Atk2OnCoolDown = true;//this means the move is now on cool down
            Atk2textCooldown.gameObject.SetActive(true);//this turns on the text
            Atk2cooldownTimer = Atk2cooldownTime;//this fills in the timer with the eloted cooldown amount
            //return true;
        }
    }

    public void StartAtk3Cooldown()//this is called when the move is used and it begins the process of cooldown by setting it to true, filling the timer, and turning on the number graphic.
    {
        if (Atk3OnCoolDown)//if this command is called again while cooldown is active, returns nothing and or can be programmed to make a fail noise. this may be able to be left empty as player inputs will check the bool before being sent .
        {
            //return false;
            //put in error noise 
        }
        else
        {
            Atk3OnCoolDown = true;//this means the move is now on cool down
            Atk3textCooldown.gameObject.SetActive(true);//this turns on the text
            Atk3cooldownTimer = Atk3cooldownTime;//this fills in the timer with the eloted cooldown amount
            //return true;
        }
    }

    public void StartAtk4Cooldown()//this is called when the move is used and it begins the process of cooldown by setting it to true, filling the timer, and turning on the number graphic.
    {
        if (Atk4OnCoolDown)//if this command is called again while cooldown is active, returns nothing and or can be programmed to make a fail noise. this may be able to be left empty as player inputs will check the bool before being sent .
        {
            //return false;
            //put in error noise 
        }
        else
        {
            Atk4OnCoolDown = true;//this means the move is now on cool down
            Atk4textCooldown.gameObject.SetActive(true);//this turns on the text
            Atk4cooldownTimer = Atk4cooldownTime;//this fills in the timer with the eloted cooldown amount
            //return true;
        }
    }
    #endregion 

}
