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
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    //SceneManager_activeSceneChanged is a built-in Unity function that gets called whenever the active scene changes.
    private void SceneManager_activeSceneChanged(Scene previousScene, Scene currentScene)
    {
        this.gameObject.GetComponent<AudioSource>().UnPause(); //.UnPause() resumes the song instead of starting over.
        //If the currentScene.name is a valid overworld scene, the appropriate music will play, otherwise don't play any music.
        if(currentScene.name == "kurotopdown"){
            this.gameObject.GetComponent<AudioSource>().clip = kurotopdown;
        }
        else{
            this.gameObject.GetComponent<AudioSource>().Pause();
        }
    }
}
