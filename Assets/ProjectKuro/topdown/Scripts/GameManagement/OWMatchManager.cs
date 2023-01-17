using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;

public class OWMatchManager : MonoBehaviour
{

    #region singleton
    public static OWMatchManager instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one kuroparty
    //and you can call it easily by just calling PlayerMovement.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of OWMatchManager found");
            return;
        }
        instance = this;

        BlackoutBox.SetActive(true);
    }
    #endregion

    //overworld aspects
    [SerializeField]
    public GameObject OWVcam;
    [SerializeField]
    public GameObject OverWorldGrid;
    [SerializeField]
    public GameObject KiaNPC;

    //scenes 
    private Scene OverworldScene;
    private Scene CombatScene;
    public string sceneToLoad;

    public BattleStarter Currentenemy;

    public bool IsInCombat;//bool is used to track when the player is in combat

    //bools for checking if combat capable, when boys are sent, when each of overworld and combat world are loaded or unloaded, when boys are received. 

    //transition graphic variables
    [SerializeField]
    private GameObject BlackoutBox;//image that does fade ins and screen wipes
    [SerializeField]
    private CanvasGroup EffectCanvas;
    [SerializeField]
    public bool Introdone = false;//bool used to tell when the intro is completed
    [SerializeField]
    private float IntroTime;//how long the fade in is
    private float IntroTimer = 0.0f;//counts down



    private void Start()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);

        IntroTimer = IntroTime;

        //IF EVERYTHING LOADED
        PlayerMovement.instance.ControlActive = false;
    

        
    }

    private void Update()
    {
        if (!Introdone) { IntroCountdown(); }
    }

    void IntroCountdown()//this is called in update and it counts down the time but also updates the graphic
    {
        //subtract time since last called
        IntroTimer -= Time.deltaTime;

        if (IntroTimer < 0.0f)//this is if the timer reaches 0, aka the move finishes cooldown.
        {
            Introdone = true;//says intro is done

            if (BlackoutBox != null)
            { 
                BlackoutBox.gameObject.SetActive(false);//turns off blackout box
                KiaNPC.GetComponent<NPC>().walking = true;//sets kia npc to walking aka kicking off cutscene
                //Atk1imageCooldown.fillAmount = 0.0f;//lowers and keeps the fill amount to 0, sace code for enemy encounter
            }
        }
        else//this is called every frame the cooldown isnt done and updates the graphic.
        {
            if (BlackoutBox != null)
            {

                EffectCanvas.alpha = IntroTimer/IntroTime;

                //Atk1imageCooldown.fillAmount = Atk1cooldownTimer / Atk1cooldownTime;//this fills the image by dividing the cooldown timer by the cooldown time.//see above greened out code
            }
        }


    }

    public void EnterCombat(BattleStarter CurrentEnemy)//make sure this command is only called by player? that way theres a way to check if the player can even fight.
    {
        IsInCombat = true;

        Currentenemy = CurrentEnemy;

        //play noise and graphic

        //Set combat scene as active scene
        OverworldScene = SceneManager.GetActiveScene();
        CombatScene = SceneManager.GetSceneByName(sceneToLoad);
        SceneManager.SetActiveScene(CombatScene);//reduce to 1 line?

        //set camera off
        OWVcam.SetActive(false);

        //tell Match manager to start combat, turns on UI and camera
        MatchManager.instance.LoadCombatSystem();//this uses a singleton to communicate through classes to tell the match manager to begin entering combat this entails turning on the camera and UI

    }

    public void UnloadOverworld()//called by kuroparty after all kuros are sent to minimize data usage and avoid unloading the world before the rigs are moved.
    {
        //unload OW scene
        OverWorldGrid.SetActive(false);

    }

    public void loadOverworld()//called by kuroparty after all kuros are sent to minimize data usage and avoid unloading the world before the rigs are moved.
    {
        //load OW scene
        OverWorldGrid.SetActive(true);

    }

    public void ExitCombat(bool p1win)//can only be called by match manager on a loss win or run from a battle?
    {
        //Set Overworld Scene as active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(OverworldScene.name));

        //set camera to on
        OWVcam.SetActive(true);

        //turn overworld input on
        PlayerMovement.instance.ControlOn(true);

        IsInCombat = false;

        if(p1win){
            if(Currentenemy.gameObject.tag == "WildKuro"){
                Destroy(Currentenemy.gameObject);
            }
            else{
                Currentenemy.gameObject.GetComponent<NPC>().enabled = true;
                Currentenemy.gameObject.GetComponent<NPC>().sayDefeated();
                Currentenemy.gameObject.GetComponent<NPC>().startDialogue();
                Currentenemy.isDefeated = true;
            }
        }
        else{
            PlayerMovement.instance.transform.position = PlayerMovement.instance.startingPosition.initialValue;
            PlayerMovement.instance.resetHealth();
        }
    }

}
