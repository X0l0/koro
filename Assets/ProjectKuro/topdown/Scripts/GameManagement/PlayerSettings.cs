using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private static GameObject instance;

    void Start()
    {
        //Singleton definition. Any DontDestroyOnLoad objects need to make sure they don't end up duplicating themselves.
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        //If no player preferences have been set before, they need to be defined by default values now
        if(PlayerPrefs.HasKey("InitialSetup")){
            SetToDefault();
        }
    }

    public void SetToDefault(){
        PlayerPrefs.SetString("InitialSetup", "TRUE");
        PlayerPrefs.SetInt("ShowFPS", 0);
    }
}
