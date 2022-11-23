using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int fps;
    public Text fpsText;
    private float currentTime;

    public bool fpsOn;

    void Start(){
        currentTime = 1;
        GetShowFPS();
    }

    void OnEnable(){
        GetShowFPS();
    }

    private void GetShowFPS(){
        if(PlayerPrefs.GetInt("ShowFPS") == 1){
            fpsOn = true;
        }
        else{
            fpsOn = false;
        }
    }

    void Update()
    {
        if(fpsOn){
            float current = 0;
            current = (int)(1f/Time.unscaledDeltaTime);
            fps = (int)current;
            currentTime -= Time.deltaTime;
            if(currentTime <= 0){
                currentTime = 1;
                fpsText.text = fps.ToString();
            }
        }
        else{
            fpsText.text = "";
        }
    }

    public void ToggleFPS(){
        if(PlayerPrefs.GetInt("ShowFPS") == 1){
            fpsOn = false;
            PlayerPrefs.SetInt("ShowFPS", 0);
        }
        else{
            fpsOn = true;
            fpsText.text = "FPS";
            PlayerPrefs.SetInt("ShowFPS", 1);
        }
    }
}
