using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip PlainsStage;
    public AudioClip VictoryJingle;
    public AudioClip kurotopdown; //song for major area; if the player enters a house in a city, music generally shouldn't change

    void Start()
    {
        PlayOverworldMusic();
    }


    //play overworld music
    public void PlayOverworldMusic()
    {
        this.gameObject.GetComponent<AudioSource>().clip = kurotopdown;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    //pause overworld music
    public void PauseOverworldMusic()
    {
        this.gameObject.GetComponent<AudioSource>().Pause();
    }

    //Play Combat music
    public void PlayCombatMusic()
    {
        this.gameObject.GetComponent<AudioSource>().clip = PlainsStage;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    //Stop Combat music.
    public void StopCombatMusic()
    {
        this.gameObject.GetComponent<AudioSource>().Stop();
    }

    //play victory music
    public void PlayVictoryJingle(){
        this.gameObject.GetComponent<AudioSource>().clip = VictoryJingle;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    //unpause overworld music.
    public void UnPauseOverworldMusic()
    {
        this.gameObject.GetComponent<AudioSource>().clip = kurotopdown;
        this.gameObject.GetComponent<AudioSource>().UnPause();
    }


    //Change overworld music
    //Change combat music


}
