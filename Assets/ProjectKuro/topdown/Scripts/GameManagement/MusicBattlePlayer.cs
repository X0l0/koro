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

    private void SceneManager_activeSceneChanged(Scene previousScene, Scene currentScene)
    {
        if(currentScene.name == "PlainsStage"){
            this.gameObject.GetComponent<AudioSource>().clip = PlainsStage;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
        else{
            this.gameObject.GetComponent<AudioSource>().Stop();
        }
    }
}
