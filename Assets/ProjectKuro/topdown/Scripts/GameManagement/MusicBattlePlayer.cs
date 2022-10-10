using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBattlePlayer : MonoBehaviour
{
    public AudioClip PlainsStage;

    void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    //SceneManager_activeSceneChanged is a built-in Unity function that gets called whenever the active scene changes.
    private void SceneManager_activeSceneChanged(Scene previousScene, Scene currentScene)
    {
        //If the currentScene.name is a valid battle scene, the appropriate music will play, otherwise don't play any music.
        if(currentScene.name == "PlainsStage"){
            this.gameObject.GetComponent<AudioSource>().clip = PlainsStage;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else{
            this.gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
