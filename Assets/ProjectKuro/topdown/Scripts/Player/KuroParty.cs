using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KuroParty : MonoBehaviour
{
    public GameObject[] KuroSlot;

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


    //this script is in charge of holding the players kuro party, showing there data, and sending them to and from combat.
  
    [SerializeField] List<GameObject> CurrentParty;//party sytem, replace with transforms?


    public Transform Kuro;//temporary transform variable to hold kuro

    public GameObject KuroRig;//temporary variable that holds rig 
    public CardHolder KuroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KuroConnector;//slot that holds a kuros connector script. used in loading and unloading into combat.

    public BattleStarter Currentenemy;


    //add function to switch order of kuro in current party
    public void AddKuro(GameObject NewKuro)//this is the new add koro to party function.
    {
        if (CurrentParty.Count >= 6)//checks if there is room
        {
            Debug.Log("Not Enough Room");
            return;
        }
        NewKuro.GetComponent<MatchConnecter>().P1 = true;//defines picked up koro as p1 aka player koro
        CurrentParty.Add(NewKuro);//adds kuro to list

    }

    //public void AddKuroObject(Transform newKuro)//function used in adding kuro, edited later?
    //{
    //    Kuro = newKuro;

    //    KuroRig = Kuro.gameObject;//this loads a local rig slot with the rig connected to the trasnform.
    //    KuroConnector = KuroRig.GetComponent<MatchConnecter>();//this loads a connector slot with the connector attached to the rig
    //    KuroConnector.P1 = true;//TEMPORARY THING, MEANS THAT ONLY THE PLAYER KURO LOADS INTO THE UI
    //}

    public void ShowParty()// This function is activated by opening the inventory. 
    {

        //this code cycles through the current party displaying their information in the party menu
        for (int i = 0; i < CurrentParty.Count; i++)
        {
            KuroCardHolder = CurrentParty[i].GetComponent<CardHolder>();//this fills a local cardholder variable with the one from the selected rig

            if(KuroCardHolder.KuroData.NickName != "unnamed"){
                KuroSlot[i].transform.GetChild(0).GetComponent<Text>().text = KuroCardHolder.KuroData.NickName;
            }
            else{
                KuroSlot[i].transform.GetChild(0).GetComponent<Text>().text = KuroCardHolder.KuroData.SpeciesName;
            }
            
            KuroSlot[i].transform.GetChild(1).GetComponent<Text>().text = KuroCardHolder.KuroData.LVL.ToString();

            KuroSlot[i].transform.GetChild(2).GetComponent<Text>().text = KuroCardHolder.KuroData.CurrHP.ToString() + "/" + KuroCardHolder.KuroData.MaxHP.ToString();
        }
    }
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
        //this is here as a way of confirming that the player can indeed fight.


        Currentenemy.SendEnemyKuroToCombat();//calls for current enemy script to send its kuros as well
        Currentenemy = null;//empties current enemy variable to make room for future enemies


        //add a for loop so that when all kuros have been sent send a signal to switchkuro that teamis done.
        for (int i = 0; i < CurrentParty.Count; i++)
        {
            //add a for loop so that when all koros have been sent send a signal to switchkoro that teamis done.
            //transforms are required for placemnt
            CurrentParty[i].transform.parent = SwitchKuro1.instance.transform;//this sends the child koro to under the player brain object.

            //finds koro in first slot and sends that one as first active.
            KuroConnector = CurrentParty[0].GetComponent<MatchConnecter>();

            KuroConnector.ConnectToBrain(SwitchKuro1.instance);//sends signal to koro connector to relocate and connect to player brain.
        }

        SwitchKuro1.instance.AllKuroSent();//tells switch kuro that all kuros have been sent and the next steps can begin.
    }

}
