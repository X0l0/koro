using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static GameObject instance;
    public AudioClip kurotopdown; //song for major area; if the player enters a house in a city, music generally shouldn't change
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene previousScene, Scene currentScene)
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        if(currentScene.name == "kurotopdown"){
            this.gameObject.GetComponent<AudioSource>().clip = kurotopdown;
        }
        else{
            this.gameObject.GetComponent<AudioSource>().Pause();
        }
    }
}
