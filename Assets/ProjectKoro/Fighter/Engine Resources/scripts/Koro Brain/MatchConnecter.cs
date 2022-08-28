using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MatchConnecter : MonoBehaviour
{
	//loaded rigs would have a majority of things on it disabled, with the exception of this script and the CardHolder. This script
	//would be responsible for loading and unloading itself when engaging in combat and switching. 
	
	[SerializeField]
	public GameObject PlayerBrain;//serialized field for player brain, used in physical placement.


	//KoroRig Variables
	public GameObject KoroRig;//holds KoroRig that is underneath the KoroBrain.
	public Transform KoroRigTransform;//holds the Rigs Transform to move its position and parents.

	public Player Player;//connects to player for passing in the Player brains inputhandler
	public KoroCore KoroCore;

	public MoveCooldown MoveCooldown;//used to pass in cooldown UI
	public Health Health;//used to pass in cooldown UI

	//player brain variables
	public InputHandler InputHandler;
	public SwitchKoro SwitchKoro;

	public bool P1 = false;

	void Start()//this start is called only once when the rig is created thus filing all relevant variables on creation.
	{
		Debug.Log("MAtch connector starting");
		KoroRig = transform.GetChild(0).gameObject;//this fills the current koro variable with the one directly under it.
		KoroRigTransform = (KoroRig.transform);//this fills a transform variable with the transform of the current koro.
		//Player = KoroRig.GetComponent<Player>();//this is normally player but is changed to rigo core for testing.
		KoroCore = KoroRig.GetComponent<RigoCore>();//in the future you will need some way to load this variable despite having different cores
		MoveCooldown = KoroRig.GetComponent<MoveCooldown>();
		Health = KoroRig.GetComponent<Health>();

		//ConnectToPlayerBrain();
	}


	public void ConnectToBrain(SwitchKoro Brain)//this is called by the KoroParty after changing its parent to the playerbrain, initiated combat.
	{
		//fills in what brain it is connecting to 
		PlayerBrain = Brain.transform.gameObject;

		//placement of rig to brains location
		KoroRigTransform.transform.position = PlayerBrain.transform.position;

		//Connect To Components in PlayerBrain
		InputHandler = GetComponentInParent<InputHandler>();//these use a getcomponent, use given brain variable?
		SwitchKoro = GetComponentInParent<SwitchKoro>();

		//log self to switch Koro.
		SwitchKoro.LoadKoroTeam(this.transform);

			//BringOnline();//after initial placement, connecting, and logging. it would intiate the next phase. later on only have one koro at a time be brought online.

	}
	public void BringOnline()//this would be called when sending the koro out for battle. it will do it once when first entering comabt to the players selected koro, and when switching.
	{

		//reset position play animation for setting koro out
		KoroRigTransform.transform.position = PlayerBrain.transform.position;

		//activate rig
		Debug.Log("Activating rig");
		 KoroRig.SetActive(true);//on activation intiate intro state

		//load Camera
		SwitchKoro.LoadCamera(KoroRigTransform);

		//load UI
		Health.LoadHealthBar(SwitchKoro.healthBar);

		if (P1 == true)
		{
			MoveCooldown.LoadCoolDownUI(SwitchKoro.Atk1imageCooldown, SwitchKoro.Atk1textCooldown, SwitchKoro.Atk2imageCooldown, SwitchKoro.Atk2textCooldown, SwitchKoro.Atk3imageCooldown, SwitchKoro.Atk3textCooldown, SwitchKoro.Atk4imageCooldown, SwitchKoro.Atk4textCooldown);
		}
		else if(P1 == false)
        {
			KoroRig.tag = "Enemy";
		}

		//turn input on.
		InputHandler.ConnectCore(KoroCore);
	}

	public void KoroDead()
    {
		SwitchKoro.KoroLost();
    }

	public void BringOffline()
	{
		//turn input off
		InputHandler.ClearCore();

		//unload UI
		Health.UnloadHealthBar();
		if (P1 == true)
        {
		MoveCooldown.UnloadCoolDownUI();
        }

		//play returning koro animation.

		//UnLoad Camera
		SwitchKoro.UnloadCamera(KoroRigTransform);


		//DeActivate rig
		KoroRig.SetActive(false);

		//reset position play animation for setting koro out
		KoroRigTransform.transform.position = PlayerBrain.transform.position;

		//DisconnectPlayerBrain();//change to only disconencting from player when changing back to overworld scene but not losing or switching?
	}

	public void DisconnectPlayerBrain()
	{
		//Disconnect components
		InputHandler = null;
		//SwitchKoro.KoroLost();//notify player brain that match is lost, make it so its only 1 koro at a time.
		SwitchKoro = null;
		PlayerBrain = null;

		//Reposition Rig
		KoroRigTransform.transform.position = KoroParty.instance.transform.position;

		//this next step would require the switch koro script to be able to deterime a loss and send signals to the game manager and the koros to switch back.
	}

}
