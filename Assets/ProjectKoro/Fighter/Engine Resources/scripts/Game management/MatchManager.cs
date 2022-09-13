using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    //The match manager is responsibile for managing the fights of the game. its in charge of loading and starting the fight, unloading and stopping the fight, and keeping all the information in place while the fight is going.


    #region singleton
    public static MatchManager instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one koroparty
    //and you can call it easily by just calling PlayerMovement.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of MatchManager found");
            return;
        }
        instance = this;
    }
    #endregion


    //combat system aspects  
    [SerializeField]
    GameObject CombatSystem;//combat system is a empty game object that has all of the actual combat staff as its children. having it serialized here lets the match manager load and unload the entire combat stuff.
    [SerializeField]
    GameObject CombatVcam;//this combat vcam is kind of like a virtual cameraman that moves the actual camera around. its plugged into here so the match manager can control where the player looks.
    [SerializeField]
    GameObject FightingUI;//This gameobject is the entirety of the fighitng UI canvas, its here for loading and visual control purposes

                    
    [SerializeField]//these are the actual player brains aka objects that are in charge of handling the koro and representing the player and the enemy, or the 2 players.
    GameObject Brain1;
    [SerializeField]
    GameObject Brain2;


    bool p1Ready;//these are some bools representing when players are loaded and ready to fight.
    bool p2Ready;

    void Update()
    {
        if(p1Ready == true && p2Ready == true)//match manager checks if players are loaded in, change from update?
        {
            //make loading bools false to confirm they have been read and to prevent infinite loop.
            p1Ready = false;
            p2Ready = false;

            //called after all koro are sent to minimize data usage
            OWMatchManager.instance.UnloadOverworld();//communicates to overworld match manager to unload the overworld
            StartCombat();//starts battle after everything is loaded
        }

    }
    //These are called by the switch koros to let the game manager that they are loaded .
    public void P1Loaded() => p1Ready = true;
    public void P2Loaded() => p2Ready = true;


    public void LoadCombatSystem()//this is called by the OW match manager when entering combat
    {
        CombatSystem.SetActive(true);//loads combat system

        //pass in Koroparty members under the player brain.

        CombatVcam.SetActive(true);//turns on vcameraman

        FightingUI.SetActive(true);//turns on UI

        KoroParty.instance.SendKoroToCombat(); //tells player party that it can send both its own koro and the current enemys Koro to battle.

        //double check things 
    }

    public void StartCombat()//combat is only every actually started once everything is loaded.
    {

        SwitchKoro1.instance.BringKoroOnline();
        SwitchKoro2.instance.BringKoroOnline();
        //play animatoin and starting voice
        //start battle timer
        //etc
    }

    public void MatchSet(bool p1win)//called by switch koro scripts to tell when the match is ended, add way to stop both switch koros sending in signals?
    {
        //stop input and play animation?

        if(p1win == true)//this means player 1 wins
        {
            
            Debug.Log("***PLAYER 1 WINS!***");
            //play a graphic
            ExitCombat(p1win);
        }
        else if(p1win == false)//this means player 2 wins
        {
            
            Debug.Log("***PLAYER 2 WINS!***");
            //play a graphic
            ExitCombat(p1win);
        }

    }

    public void ExitCombat(bool p1win)//this would be called after the winnier is decided and intiate going back to the overworld.
    {
        //upon displaying who won, bring all rigs offline
        SwitchKoro1.instance.BringKoroOffline();
        SwitchKoro2.instance.BringKoroOffline();

        //load overworld
        OWMatchManager.instance.loadOverworld();
        
        //send rigs back to overworld
        SwitchKoro1.instance.UnloadKoroTeam();
        SwitchKoro2.instance.UnloadKoroTeam();

        //disable canvas 
        FightingUI.SetActive(false);
        //disable combat vcam
        CombatVcam.SetActive(false);

        //tell overworld that things are ready for it to take over again
        OWMatchManager.instance.ExitCombat(p1win);

        //unload combat scene, this is done last to make sure all rigs are removed before it is unloaded. may need a way to better communicate when rigs are clear
        CombatSystem.SetActive(false);

    }
}
