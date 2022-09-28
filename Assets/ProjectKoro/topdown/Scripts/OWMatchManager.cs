using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class OWMatchManager : MonoBehaviour
{

    #region singleton
    public static OWMatchManager instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one koroparty
    //and you can call it easily by just calling OWPlayerMovement.instance

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More then one instance of OWMatchManager found");
            return;
        }
        instance = this;
    }
    #endregion
    //overworld aspects
    [SerializeField]
    public GameObject OWVcam;
    [SerializeField]
    public GameObject OverWorldGrid;
    //scenes 
    private Scene OverworldScene;
    private Scene CombatScene;
    public string sceneToLoad;

    public BattleStarter Currentenemy;

    public bool IsInCombat;//bool is used to track when the player is in combat

    public void EnterCombat(BattleStarter CurrentEnemy)//make sure this command is only called by player? that way theres a way to check if the player can even fight.
    {
        IsInCombat = true;

        Currentenemy = CurrentEnemy;

        //play noise and graphic

        //Set combat scene as active scene
        OverworldScene = SceneManager.GetActiveScene();
        CombatScene = SceneManager.GetSceneByName(sceneToLoad);
        SceneManager.SetActiveScene(CombatScene);//reduce to 1 line?

        //set camera off
        OWVcam.SetActive(false);

        //tell Match manager to start combat, turns on UI and camera
        MatchManager.instance.LoadCombatSystem();//this uses a singleton to communicate through classes to tell the match manager to begin entering combat this entails turning on the camera and UI

    }

    public void UnloadOverworld()//called by koroparty after all koros are sent to minimize data usage and avoid unloading the world before the rigs are moved.
    {
        //unload OW scene
        OverWorldGrid.SetActive(false);

    }

    public void loadOverworld()//called by koroparty after all koros are sent to minimize data usage and avoid unloading the world before the rigs are moved.
    {
        //load OW scene
        OverWorldGrid.SetActive(true);

    }

    public void ExitCombat(bool p1win)//can only be called by match manager on a loss win or run from a battle?
    {
        //Set Overworld Scene as active scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(OverworldScene.name));

        //set camera to on
        OWVcam.SetActive(true);

        //turn overworld input on
        OWPlayerMovement.instance.ControlOn(true);

        IsInCombat = false;

        if(p1win){
            Destroy(Currentenemy);
        }
    }

}
