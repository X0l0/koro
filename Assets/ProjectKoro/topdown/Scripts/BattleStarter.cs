using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BattleStarter : MonoBehaviour//this script would be put on wild koro and trainers to enable a battle.
{

    public Transform Koro;//temporary transform variable to hold koro

    public GameObject KoroRig;//temporary variable that holds rig 
    public CardHolder KoroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KoroConnector;//temporary variable that holds connector

    private Collider2D challenger;
    private float currentTime;
    private float waitTime;
    private bool waiting;

    [SerializeField]
    public bool isDefeated; //if true, battle won't initiate

    private void Start(){
        waitTime = 1.5f;
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
    
    public void AddKoroObject(Transform newKoro)
    {
        Koro = newKoro;
        //PUT THESE EARLIER AS THEY SHOULD/COULD BE KNOWN EARLIER
        KoroRig = Koro.gameObject;//this loads a local rig variable with the local transforms logged rig.
        KoroConnector = KoroRig.GetComponent<MatchConnecter>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isDefeated && other.transform.childCount > 0){
            challenger = other;
            this.gameObject.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            if(this.gameObject.tag == "WildKoro"){
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

        //battle start is going to need to be able to hold a rig like koro party and send it over alongside the players party.
    }

    public void Challenge(){
        challenger.GetComponent<KoroParty>().BeChallenged(this);
    }

    public void SendEnemyKoroToCombat()
    {
        Koro.transform.parent = SwitchKoro2.instance.transform;//this sends the child koro to under the player brain object.
        KoroConnector.ConnectToBrain(SwitchKoro2.instance);//sends signal to koro connector to relocate and connect to player brain.

        //add a for loop so that when all koros have been sent send a signal to switchkoro that teamis done.
        SwitchKoro2.instance.AllKoroSent();

    }

}
