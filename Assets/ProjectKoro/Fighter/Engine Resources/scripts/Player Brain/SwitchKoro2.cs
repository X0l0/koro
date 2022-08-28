using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
using UnityEngine.UI;//these libraries are added so this script can work with UI and Text
using TMPro;

public class SwitchKoro2 : SwitchKoro
{
    #region singleton
    public static SwitchKoro2 instance;
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
        if (NoKoroLeft == true)
        {
            Player2Lost();
        }
    }

    public void AllKoroSent()
    {
        TeamLoaded = true;
        MatchManager.instance.P2Loaded();
    }

    public void Player2Lost()
    {
        MatchManager.instance.MatchSet(true);//this means playe 1 wins, false means player 2 wins

        //something something to match manager
        //MatchManager.instance.ExitCombat();
    }
}

