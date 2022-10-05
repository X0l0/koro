using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleStarter : MonoBehaviour//this script would be put on wild kuro and trainers to enable a battle.
{

    public Transform Kuro;//temporary transform variable to hold kuro

    public GameObject KuroRig;//temporary variable that holds rig 
    public CardHolder KuroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KuroConnector;//temporary variable that holds connector

    private Collider2D challenger;
    private float currentTime;
    private float waitTime;
    private bool waiting;

    [SerializeField]
    public bool isDefeated; //if true, battle won't initiate

    private void Start(){
        waitTime = .5f;
        currentTime = waitTime;
        waiting = false;
    }

    void Update(){
        if(waiting){
            currentTime -= Time.deltaTime;
            if(currentTime <= 0){
                waiting = false;
                Challenge();
            }
        }
    }
    
    public void AddKuroObject(Transform newKuro)
    {
        Kuro = newKuro;
        //PUT THESE EARLIER AS THEY SHOULD/COULD BE KNOWN EARLIER
        KuroRig = Kuro.gameObject;//this loads a local rig variable with the local transforms logged rig.
        KuroConnector = KuroRig.GetComponent<MatchConnecter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isDefeated && other.transform.childCount > 0){
            challenger = other;
            this.gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            if(this.gameObject.tag == "WildKuro"){
                waiting = true;
                other.gameObject.GetComponent<PlayerMovement>().ControlActive = false;
            }
            else{
                this.gameObject.GetComponent<NPC>().enabled = true;
                this.gameObject.GetComponent<NPC>().isEnemy = true;
                this.gameObject.GetComponent<NPC>().startDialogue();
            }
            
            //finds enemies health script and applies damage value. eventually going to need game objects.
        }

        //battle start is going to need to be able to hold a rig like kuro party and send it over alongside the players party.
    }

    public void Challenge(){
        challenger.GetComponent<KuroParty>().BeChallenged(this);
    }

    public void SendEnemyKuroToCombat()
    {
        Kuro.transform.parent = SwitchKuro2.instance.transform;//this sends the child kuro to under the player brain object.
        KuroConnector.ConnectToBrain(SwitchKuro2.instance);//sends signal to kuro connector to relocate and connect to player brain.

        //add a for loop so that when all kuros have been sent send a signal to switchkuro that teamis done.
        SwitchKuro2.instance.AllKuroSent();

    }

}
