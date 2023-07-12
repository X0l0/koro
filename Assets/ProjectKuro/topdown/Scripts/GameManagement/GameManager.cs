using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//these libraries are added so this script can work with UI and Text
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //this script is in charge of managing overall game activities and events. specifically entering and exiting combat as well as certain cutscenes.
    //think of it as the god/watcher of the game world, controlling funamental parts of the game and being able to known and cummunicate certain important information.

    #region singleton
    public static GameManager instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one of this script
    //and you can call it easily by just calling (this script).instance

    private void Awake()//does before absolutely anything
    {

        //singleton 
        if (instance != null)//if the instance of gamemanger is not null aka there is another instance of this script.
        {
            Debug.LogWarning("More then one instance of GameManager found");
            return;
        }
        instance = this;//replaces any other instance with this one.


        BlackoutBox.SetActive(true);//this blackout box is related to UI MOVE LATERRRRR
    }
    #endregion

    //general components
    [SerializeField]
    public GameObject MainCamera;//game camera
    [SerializeField]
    public GameObject OverWorldGrid;//this gameobject is parent to all active overworld elements and thus can be unloaded and loaded
    [SerializeField]
    public GameObject AudioManager;//Audio player/manages base settings.
    [SerializeField]
    private CanvasGroup EffectCanvas;//UI

    [SerializeField]
    private GameObject BlackoutBox;//image that does fade ins and screen wipes

    //scenes 
    //The active Scene is the Scene which will be used as the target for new GameObjects instantiated by scripts and from what Scene the lighting settings are used
    public string sceneName;//string key for switching scenes

    [SerializeField]
    public string OverworldScene;//Overworld Base Scene
    [SerializeField]
    public string CombatScene;//subscene with combat



    //lab 


    //combat switching
    public BattleStarter Currentenemy;
    public bool IsInCombat;//bool is used to track when the player is in combat

    //Intro cutscene variables
    [SerializeField]
    public GameObject KiaNPC;//this is a imporant npc, its place here is for the intro cutscene
    [SerializeField]
    private float IntroTime;//how long the fade in is
    private float IntroTimer = 0.0f;//counts down
    public bool Introdone = false;//bool used to tell when the intro is completed

    //transition fx variables
    public bool FillTransitionfx = false;//these fill and empty bools are used to track when the respective animations are done
    public bool EmptyTransitionfx = false;
    public float TransitionfxTime;//this decides how long the transitions are, usually around 1f or 1 second
    public float TransitionfxTimer = 0.0f;//counts up and down depending on function.
    public bool TransitionLoaded = false;//this is a overall bool that helps track which transition was last used .

    private void Start()
    {
        //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);//used to load in current scene

        IntroTimer = IntroTime;//sets up intro timer with intro time.

        PlayerMovement.instance.ControlActive = false;//sets players control as to false to start off cutscene


    }

    private void Update()
    {
        if (!Introdone) { IntroCountdown(); }//if intro not done, do intro

        if (FillTransitionfx) { FIllTransition(); }//if doing enter battle fx, start countdown
        if(EmptyTransitionfx) { EmptyTransition(); }
    }



    //change overworld region
    //blackout for loading
    //unload overworld
    //activate and load second scene
    //send gamemanager, the player, and data to second scene 
    //completely unload and remove old scene.
   //define new scene names for the overworldscene and combatscene variables.

    //entering and exiting combat functions
    public void EnterCombat(BattleStarter CurrentEnemy)//called only by kuro party after being succesfully challenged. kicks off process
    {

        IsInCombat = true;//sets bool used in do battle transition code. helps let know to do loading actions after the animation

        Currentenemy = CurrentEnemy;//fill bool with current enemy so it can be passed through

        DoBattleTransition();


        //these below actions where moved into the battletransition fx system. create another sub function?

        ////Set combat scene as active scene
        //OverworldScene = SceneManager.GetActiveScene();
        //CombatScene = SceneManager.GetSceneByName(sceneName);
        //SceneManager.SetActiveScene(CombatScene);//reduce to 1 line?

        ////set camera off
        //GameCamera.SetActive(false);

        ////tell Match manager to start combat, turns on UI and camera
        //MatchManager.instance.LoadCombatSystem();//this uses a singleton to communicate through classes to tell the match manager to begin entering combat this entails turning on the camera and UI

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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(OverworldScene));

        //set camera to on
        MainCamera.SetActive(true);

        //activates battle transition
        DoBattleTransition();

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
        else{//else if player dies
            PlayerMovement.instance.transform.position = PlayerMovement.instance.startingPosition.initialValue;
            PlayerMovement.instance.resetHealth();
        }
    }


    //intro countdown used for fade in
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
                EffectCanvas.alpha = 1.0f;

                KiaNPC.GetComponent<NPC>().walking = true;//sets kia npc to walking aka kicking off cutscene
               
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


    //battle transitions
    public void DoBattleTransition()//called when entering and exiting combat, fills and emptys a screen wipe using update countdowns.
    {
        //Debug.Log("battle transition called");
        if (!TransitionLoaded)//if the transition is not filled aka filling screen to do loading 
        {
            BlackoutBox.gameObject.SetActive(true);//sets fx box as active, turned off in emptytransition function
            TransitionfxTimer = 0;//makes timer 0 so it can fill up reflecting the fill of the transition box
            BlackoutBox.GetComponent<Image>().fillAmount = 0f;//makes box completely empty so it can be filled

            FillTransitionfx = true;//bool is set to true, this is detected in update and thus counts down. 

        }
        else if (TransitionLoaded)//if the transition is filled, aka loading is done and filled screen can now be emptied
        {
            TransitionfxTimer = TransitionfxTime;
            BlackoutBox.GetComponent<Image>().fillAmount = 1f;

            EmptyTransitionfx = true;//bool is set to true, this is detected in update and activates the transition.

        }
        
 
    }

    void FIllTransition()//this is called in update when the game needs to wipe to a black screen and do some loading functions.
    {
        //Debug.Log("fill transition called");

        TransitionfxTimer += Time.deltaTime;//counts up every update

        if (TransitionfxTimer >= TransitionfxTime)//when timer counts up to equal the fx time
        {

            FillTransitionfx = false;//says no longer doing battle fx so stops counting
            TransitionLoaded = true;//changes this bool to transition loaded aka the screen is now blacked out.

            if (IsInCombat)//if is in combat aka going from the overworld into combat
            {
                //these following actions are loading actions, maybe move into there own function?

                //// Loads the second Scene
                //SceneManager.LoadScene(CombatScene, LoadSceneMode.Additive);
                // Set Scene2 as the active Scene
                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(CombatScene));

                    //set camera off
                    MainCamera.SetActive(false);

                    //tell Match manager to start combat, turns on UI and camera
                    MatchManager.instance.LoadCombatSystem();//this uses a singleton to communicate through classes to tell the match manager to begin entering combat this entails turning on the camera and UI
            }
            else if( !IsInCombat )//if not in combat aka going from combat back to the overworld.
            {
                //Set overworld scene as active scene.
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(OverworldScene));//reduce to 1 line?
                MatchManager.instance.UnloadCombatSystem();//calls to combat game manager to do its unloading processes.
            }
        }
        else//this is called every frame the timer ticks up.
        {
            if (BlackoutBox != null)//if the box is active, which it should be.
            {
                BlackoutBox.GetComponent<Image>().fillAmount = TransitionfxTimer / TransitionfxTime; //as the timer tiks up, so does the amount of fill of the blackout box. thus giving a wiping transition effect.
            }
        }
    }

    void EmptyTransition()//this is called in update and it counts down the time but also updates the graphic
    {
        //Debug.Log("empty transition called");

        //tiks down every update frame.
        TransitionfxTimer -= Time.deltaTime;

        if (TransitionfxTimer <= 0.0f)//this is if the timer reaches 0
        {
            EmptyTransitionfx = false;//says no longer doing battle fx
            TransitionLoaded = false;//sets transition loaded to false aka the screen is not blacked out.
        }
        else//this is called every time the timer tiks down.
        {
            if (BlackoutBox != null)
            {
                BlackoutBox.GetComponent<Image>().fillAmount = TransitionfxTimer / TransitionfxTime; //as the timer gets closer to 0, so does the amount of fill of the blackout box.
            }
        }


    }



}
