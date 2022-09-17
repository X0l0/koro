using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoroParty : MonoBehaviour
{
    #region singleton
    public static KoroParty instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one koroparty
    //and you can call it easily by just calling KoroParty.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of KoroParty found");
            return;
        }
        instance = this;
    }
    #endregion
    //#region delegate
    //public delegate void OnPartyChange();//tehse are meant to deleagte things that need to be updtated when there are changes in the party
    //public OnPartyChange onPartyChangeCallback;
    //#endregion

    //this script is in charge of holding the players koro party, showing there data, and sending them to and from combat.
  
    [SerializeField] List<GameObject> CurrentParty;//party sytem, replace with transforms?


    public Transform Koro;//temporary transform variable to hold koro

    public GameObject KoroRig;//temporary variable that holds rig 
    public CardHolder KoroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KoroConnector;//slot that holds a koros connector script. used in loading and unloading into combat.

    public BattleStarter Currentenemy;

    //make it so there are two lists, one that stores the rigs and one that stores the data cards. The rigs would be loaded

    //add function to switch order of koro in current party
    public void AddKoro(GameObject NewKoro)//this is called in the create Koro script.
    {
        if (CurrentParty.Count >= 6)//checks if there is room
        {
            Debug.Log("Not Enough Room");
            return;
        }
        CurrentParty.Add(NewKoro);//adds koro to list

    }

    public void AddKoroObject(Transform newKoro)//function used in adding koro, edited later?
    {
        Koro = newKoro;
        
        KoroRig = Koro.gameObject;//this loads a local rig slot with the rig connected to the trasnform.
        KoroConnector = KoroRig.GetComponent<MatchConnecter>();//this loads a connector slot with the connector attached to the rig
        KoroConnector.P1 = true;//TEMPORARY THING, MEANS THAT ONLY THE PLAYER KORO LOADS INTO THE UI
    }

    //public void ShowParty()
    //{
        //KoroRig = CurrentParty[0];//this fills a local gameobject variable with the first in the list
        //KoroCardHolder = KoroRig.GetComponent<CardHolder>();//this fills a local cardholder variable with the one from the selected rig
        //int Health = KoroCardHolder.KoroData.CurrHP;//this fills a local int variable representing health with the data in the cardholder
        //print(Health);//this prints that local health variable, effectively communicating the health stat of the koro in your party
        ////though this system is a bit complicated and long to get in theory you would only need one variable for each stat to communicate, filling it up with whatever koro is selected.
    //}
    //remove koro

    public void BeChallenged(BattleStarter CurrentEnemy)//called by wild koro and trainers thorugh battlestarter.
    {
        Currentenemy = CurrentEnemy;

        //turn off overworld input 1
        PlayerMovement.instance.ControlOn(false);

        //call entercombat function in game manager
        OWMatchManager.instance.EnterCombat(Currentenemy);//this tells the overworld match manager to turn off overworld things and turn on combat scene things.

    }

    public void SendKoroToCombat()//called by Match Manager when combat scene is loaded .
    {
        //this code cycles through the current party reading out the names of the game objects. succesful test, use for later stuff
        //for (int i = 0; i < CurrentParty.Count; i++)
        //{
        //    Debug.Log("party member is " + CurrentParty[i].name);
        //}


        Currentenemy.SendEnemyKoroToCombat();//calls for current enemy script to send its koros as well
        Currentenemy = null;//empties current enemy variable to make room for future enemies


        //add a for loop so that when all koros have been sent send a signal to switchkoro that teamis done.


        Koro.transform.parent = SwitchKoro1.instance.transform;//this sends the child koro to under the player brain object.
        KoroConnector.ConnectToBrain(SwitchKoro1.instance);//sends signal to koro connector to relocate and connect to player brain.

        SwitchKoro1.instance.AllKoroSent();//tells switch koro that all koros have been sent and the next steps can begin.
    }

}
