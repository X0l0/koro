using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroParty : MonoBehaviour
{
    #region singleton
    public static KuroParty instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one kuroparty
    //and you can call it easily by just calling KuroParty.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of KuroParty found");
            return;
        }
        instance = this;
    }
    #endregion
    //#region delegate
    //public delegate void OnPartyChange();//tehse are meant to deleagte things that need to be updtated when there are changes in the party
    //public OnPartyChange onPartyChangeCallback;
    //#endregion

    //this script is in charge of holding the players kuro party, showing there data, and sending them to and from combat.
  
    [SerializeField] List<GameObject> CurrentParty;//party sytem, replace with transforms?


    public Transform Kuro;//temporary transform variable to hold kuro

    public GameObject KuroRig;//temporary variable that holds rig 
    public CardHolder KuroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KuroConnector;//slot that holds a kuros connector script. used in loading and unloading into combat.

    public BattleStarter Currentenemy;

    //make it so there are two lists, one that stores the rigs and one that stores the data cards. The rigs would be loaded

    //add function to switch order of kuro in current party
    public void AddKuro(GameObject NewKuro)//this is called in the create Kuro script.
    {
        if (CurrentParty.Count >= 6)//checks if there is room
        {
            Debug.Log("Not Enough Room");
            return;
        }
        CurrentParty.Add(NewKuro);//adds kuro to list

    }

    public void AddKuroObject(Transform newKuro)//function used in adding kuro, edited later?
    {
        Kuro = newKuro;
        
        KuroRig = Kuro.gameObject;//this loads a local rig slot with the rig connected to the trasnform.
        KuroConnector = KuroRig.GetComponent<MatchConnecter>();//this loads a connector slot with the connector attached to the rig
        KuroConnector.P1 = true;//TEMPORARY THING, MEANS THAT ONLY THE PLAYER KURO LOADS INTO THE UI
    }

    //public void ShowParty()
    //{
        //KuroRig = CurrentParty[0];//this fills a local gameobject variable with the first in the list
        //KuroCardHolder = KuroRig.GetComponent<CardHolder>();//this fills a local cardholder variable with the one from the selected rig
        //int Health = KuroCardHolder.KuroData.CurrHP;//this fills a local int variable representing health with the data in the cardholder
        //print(Health);//this prints that local health variable, effectively communicating the health stat of the kuro in your party
        ////though this system is a bit complicated and long to get in theory you would only need one variable for each stat to communicate, filling it up with whatever koro is selected.
    //}
    //remove kuro

    public void BeChallenged(BattleStarter CurrentEnemy)//called by wild kuro and trainers thorugh battlestarter.
    {
        Currentenemy = CurrentEnemy;

        //turn off overworld input 1
        PlayerMovement.instance.ControlOn(false);

        //call entercombat function in game manager
        OWMatchManager.instance.EnterCombat(Currentenemy);//this tells the overworld match manager to turn off overworld things and turn on combat scene things.

    }

    public void SendKuroToCombat()//called by Match Manager when combat scene is loaded .
    {
        //this code cycles through the current party reading out the names of the game objects. succesful test, use for later stuff
        //for (int i = 0; i < CurrentParty.Count; i++)
        //{
        //    Debug.Log("party member is " + CurrentParty[i].name);
        //}


        Currentenemy.SendEnemyKuroToCombat();//calls for current enemy script to send its kuros as well
        Currentenemy = null;//empties current enemy variable to make room for future enemies


        //add a for loop so that when all kuros have been sent send a signal to switchkuro that teamis done.


        Kuro.transform.parent = SwitchKuro1.instance.transform;//this sends the child kuro to under the player brain object.
        KuroConnector.ConnectToBrain(SwitchKuro1.instance);//sends signal to kuro connector to relocate and connect to player brain.

        SwitchKuro1.instance.AllKuroSent();//tells switch kuro that all kuros have been sent and the next steps can begin.
    }

}
