using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static GameObject instance;
    public AudioClip korotopdown; //song for major area; if the player enters a house in a city, music generally shouldn't change
    
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
        Debug.Log("Scene changed.");
        if(currentScene.name == "korotopdown"){
            this.gameObject.GetComponent<AudioSource>().clip = korotopdown;
        }
    }
}
