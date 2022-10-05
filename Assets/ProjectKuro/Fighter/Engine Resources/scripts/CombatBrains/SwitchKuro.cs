using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;
public class SwitchKuro : MonoBehaviour
{

    //switch kuro would sit on the player brain and would be able to affect the states of kuro, make them inactive or active. it would keep track of kuro data as well.
    public bool TeamLoaded;

    public CinemachineTargetGroup targetbrain;//this takes in the Camera Group to add and remove kuro.

    public Transform Kuro;//stores the transforms of the loaded team in combat.


    public GameObject KuroRig;//temporary variable that holds rig 
    public CardHolder KuroCardHolder;//temporary variable that holds card holder
    public MatchConnecter KuroConnector;//temporary variable that holds connector

    private bool Kuro1Alive = false;

    public bool NoKuroLeft = false;

    #region UI
    public HealthBar healthBar; // UI

    [Header("Attack 1")]//eventually the graphics of the cooldowns would need to be attached to the kuro and be able to be loaded in and out. the other types of cooldowns will also need to be implemented.
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


    public void BringKuroOnline()
    {
        KuroConnector.BringOnline();
    }

    public void BringKuroOffline()
    {
        KuroConnector.BringOffline();

        //UnloadKuroTeam();
    }

    public void LoadKuroTeam(Transform NewKuro)//this is called by the Kuro party and passes in the transform data of the kuros its going to send over.
    {
        Kuro = NewKuro;
        KuroRig = Kuro.gameObject;//this loads a local rig variable with the local trasnforms logged rig.
        KuroConnector = KuroRig.GetComponent<MatchConnecter>();
        Kuro1Alive = true;
    }

    public void UnloadKuroTeam()
    {
        Kuro.transform.parent = KuroParty.instance.transform;//return parentage to kuroparty

        KuroConnector.DisconnectPlayerBrain();//disconnect rig from player brain.

        Kuro = null;//as the player brain sends over the kuro back to the overworld and prepares to tell the match manager to switch over, it emptys its kuro variable
        //this is to allow new kuro and teams to be loaded here when re engaging in battle again
        KuroRig = null;
        KuroConnector = null;
  

    }
    public void LoadCamera(Transform CurrentKuro)
    {
        targetbrain.AddMember(CurrentKuro, 1f, 5f);
    }

    public void UnloadCamera(Transform CurrentKuro)
    {
        targetbrain.RemoveMember(CurrentKuro);
    }
    public void Callback()
    {
        //it would change the kuros current state to exit and then make it inactive, 
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

    public void KuroLost()
    {

        Kuro1Alive = false;

        if(Kuro1Alive == false)//move to update?
        {
            NoKuroLeft = true;
        }

    }


}
