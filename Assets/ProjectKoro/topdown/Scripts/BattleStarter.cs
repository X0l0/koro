using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour//this script would be put on wild koro and trainers to enable a battle.
{

    public Transform Koro;//temporary transform variable to hold koro

    public GameObject KoroRig;//temporary variable that holds rig 
    public CardHolder KoroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KoroConnector;//temporary variable that holds connector

    public void AddKoroObject(Transform newKoro)
    {
        Koro = newKoro;
        //PUT THESE EARLIER AS THEY SHOULD/COULD BE KNOWN EARLIER
        KoroRig = Koro.gameObject;//this loads a local rig variable with the local trasnforms logged rig.
        KoroConnector = KoroRig.GetComponent<MatchConnecter>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<KoroParty>().BeChallenged(this);//finds enemies health script and applies damage value. eventually going to need game objects.

        //battle start is going to need to be able to hold a rig like koro party and send it over alongside the players party.
    }

    public void SendEnemyKoroToCombat()
    {
        Koro.transform.parent = SwitchKoro2.instance.transform;//this sends the child koro to under the player brain object.
        KoroConnector.ConnectToBrain(SwitchKoro2.instance);//sends signal to koro connector to relocate and connect to player brain.

        //add a for loop so that when all koros have been sent send a signal to switchkoro that teamis done.
        SwitchKoro2.instance.AllKoroSent();

    }

}
