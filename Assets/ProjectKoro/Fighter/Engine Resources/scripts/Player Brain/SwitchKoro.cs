using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;
public class SwitchKoro : MonoBehaviour
{

    //switch koro would sit on the player brain and would be able to affect the states of koro, make them inactive or active. it would keep track of koro data as well.
    public bool TeamLoaded;

    public CinemachineTargetGroup targetbrain;//this takes in the Camera Group to add and remove koro.

    public Transform Koro;//stores the transforms of the loaded team in combat.


    public GameObject KoroRig;//temporary variable that holds rig 
    public CardHolder KoroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KoroConnector;//temporary variable that holds connector

    private bool Koro1Alive = false;

    public bool NoKoroLeft = false;

    #region UI
    public HealthBar healthBar; // UI

    [Header("Attack 1")]//eventually the graphics of the cooldowns would need to be attached to the koro and be able to be loaded in and out. the other types of cooldowns will also need to be implemented.
    //attack 1 variables
    [SerializeField]
    public Image Atk1imageCooldown;//image that covers graphic and dissapears with time
    [SerializeField]
    public TMP_Text Atk1textCooldown;//the number showing how many seconds are left

    [Header("Attack 2")]
    //attack 2 variables
    [SerializeField]
    public Image Atk2imageCooldown;
    [SerializeField]
    public TMP_Text Atk2textCooldown;

    [Header("Attack 3")]
    //attack 3 variables
    [SerializeField]
    public Image Atk3imageCooldown;
    [SerializeField]
    public TMP_Text Atk3textCooldown;

    [Header("Attack 4")]
    //attack 4 variables
    [SerializeField]
    public Image Atk4imageCooldown;
    [SerializeField]
    public TMP_Text Atk4textCooldown;

    #endregion


    public void BringKoroOnline()
    {
        KoroConnector.BringOnline();
    }

    public void BringKoroOffline()
    {
        KoroConnector.BringOffline();

        //UnloadKoroTeam();
    }

    public void LoadKoroTeam(Transform NewKoro)//this is called by the Koro party and passes in the transform data of the koros its going to send over.
    {
        Koro = NewKoro;
        KoroRig = Koro.gameObject;//this loads a local rig variable with the local trasnforms logged rig.
        KoroConnector = KoroRig.GetComponent<MatchConnecter>();
        Koro1Alive = true;
    }

    public void UnloadKoroTeam()
    {
        Koro.transform.parent = KoroParty.instance.transform;//return parentage to koroparty

        KoroConnector.DisconnectPlayerBrain();//disconnect rig from player brain.

        Koro = null;//as the player brain sends over the koro back to the overworld and prepares to tell the match manager to switch over, it emptys its koro variable
        //this is to allow new koro and teams to be loaded here when re engaging in battle again
        KoroRig = null;
        KoroConnector = null;
  

    }
    public void LoadCamera(Transform CurrentKoro)
    {
        targetbrain.AddMember(CurrentKoro, 1f, 5f);
    }

    public void UnloadCamera(Transform CurrentKoro)
    {
        targetbrain.RemoveMember(CurrentKoro);
    }
    public void Callback()
    {
        //it would change the koros current state to exit and then make it inactive, 
        //it would call the pick new member function
    }

    public void picknewmember()
    {
        //display menu for player to select.
        //depending on what they select a member will be selected
        //this member will be loaded and then set to enter state
        //things like the input handler and camera would be rewired to this new member.
    }

    public void Lose()
    {
        //this would be called in update 
        //play a graphic
        //send signal to game manager.
    }

    public void KoroLost()
    {

        Koro1Alive = false;

        if(Koro1Alive == false)//move to update?
        {
            NoKoroLeft = true;
        }

    }


}
