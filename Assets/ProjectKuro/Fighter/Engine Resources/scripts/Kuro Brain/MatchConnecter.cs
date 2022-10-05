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


	//KuroRig Variables
	public GameObject KuroRig;//holds KuroRig that is underneath the KuroBrain.
	public Transform KuroRigTransform;//holds the Rigs Transform to move its position and parents.

	public Player Player;//connects to player for passing in the Player brains inputhandler
	public KuroCore KuroCore;

	public MoveCooldown MoveCooldown;//used to pass in cooldown UI
	public Health Health;//used to pass in cooldown UI

	//player brain variables
	public InputHandler InputHandler;
	public SwitchKuro SwitchKuro;

	public bool P1 = false;

	void Start()//this start is called only once when the rig is created thus filing all relevant variables on creation.
	{
		//Debug.Log("MAtch connector starting");
		KuroRig = transform.GetChild(0).gameObject;//this fills the current kuro variable with the one directly under it.
		KuroRigTransform = (KuroRig.transform);//this fills a transform variable with the transform of the current kuro.
		//Player = KuroRig.GetComponent<Player>();//this is normally player but is changed to rigo core for testing.
		KuroCore = KuroRig.GetComponent<RigoCore>();//in the future you will need some way to load this variable despite having different cores
		MoveCooldown = KuroRig.GetComponent<MoveCooldown>();
		Health = KuroRig.GetComponent<Health>();

		//ConnectToPlayerBrain();
	}


	public void ConnectToBrain(SwitchKuro Brain)//this is called by the KuroParty after changing its parent to the playerbrain, initiated combat.
	{
		//fills in what brain it is connecting to 
		PlayerBrain = Brain.transform.gameObject;

		//placement of rig to brains location
		KuroRigTransform.transform.position = PlayerBrain.transform.position;

		//Connect To Components in PlayerBrain
		InputHandler = GetComponentInParent<InputHandler>();//these use a getcomponent, use given brain variable?
		SwitchKuro = GetComponentInParent<SwitchKuro>();

		//log self to switch Kuro.
		SwitchKuro.LoadKuroTeam(this.transform);

			//BringOnline();//after initial placement, connecting, and logging. it would intiate the next phase. later on only have one kuro at a time be brought online.

	}
	public void BringOnline()//this would be called when sending the kuro out for battle. it will do it once when first entering comabt to the players selected kuro, and when switching.
	{

		//reset position play animation for setting kuro out
		KuroRigTransform.transform.position = PlayerBrain.transform.position;

		//activate rig
		//Debug.Log("Activating rig");
		 KuroRig.SetActive(true);//on activation intiate intro state

		//load Camera
		SwitchKuro.LoadCamera(KuroRigTransform);

		//load UI
		Health.LoadHealthBar(SwitchKuro.healthBar);

		if (P1 == true)
		{
			MoveCooldown.LoadCoolDownUI(SwitchKuro.Atk1imageCooldown, SwitchKuro.Atk1textCooldown, SwitchKuro.Atk2imageCooldown, SwitchKuro.Atk2textCooldown, SwitchKuro.Atk3imageCooldown, SwitchKuro.Atk3textCooldown, SwitchKuro.Atk4imageCooldown, SwitchKuro.Atk4textCooldown);
		}
		else if(P1 == false)
        {
			KuroRig.tag = "Enemy";
		}

		//turn input on.
		InputHandler.ConnectCore(KuroCore);
	}

	public void KuroDead()
    {
		SwitchKuro.KuroLost();
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

		//play returning kuro animation.

		//UnLoad Camera
		SwitchKuro.UnloadCamera(KuroRigTransform);


		//DeActivate rig
		KuroRig.SetActive(false);

		//reset position play animation for setting kuro out
		KuroRigTransform.transform.position = PlayerBrain.transform.position;

		//DisconnectPlayerBrain();//change to only disconencting from player when changing back to overworld scene but not losing or switching?
	}

	public void DisconnectPlayerBrain()
	{
		//Disconnect components
		InputHandler = null;
		//SwitchKuro.KuroLost();//notify player brain that match is lost, make it so its only 1 kuro at a time.
		SwitchKuro = null;
		PlayerBrain = null;

		//Reposition Rig
		KuroRigTransform.transform.position = KuroParty.instance.transform.position;

		//this next step would require the switch kuro script to be able to deterime a loss and send signals to the game manager and the kuros to switch back.
	}

}
