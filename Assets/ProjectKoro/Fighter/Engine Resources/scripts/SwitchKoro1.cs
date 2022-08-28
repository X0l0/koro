using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;

public class SwitchKoro1 : SwitchKoro
{
    #region singleton
    public static SwitchKoro1 instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one koroparty
    //and you can call it easily by just calling KoroParty.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of SwitchKoro2 found");
            return;
        }
        instance = this;
    }
    #endregion

    private void Update()
    {
        if(NoKoroLeft == true)
        {
            Player1Lost();
        }
    }

    public void AllKoroSent()
    {
        TeamLoaded = true;
        MatchManager.instance.P1Loaded();
    }

    public void Player1Lost()
    {
        MatchManager.instance.MatchSet(false);//this means playe 1 wins, false means player 2 wins
        //something something to match manager
        //MatchManager.instance.ExitCombat();

    }

}
