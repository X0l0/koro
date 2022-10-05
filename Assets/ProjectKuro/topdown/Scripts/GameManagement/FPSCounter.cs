using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public int fps;
    public Text fpsText;
    private float currentTime;

    void Start(){
        currentTime = 1;
    }

    void Update()
    {
        float current = 0;
        current = (int)(1f/Time.unscaledDeltaTime);
        fps = (int)current;
        currentTime -= Time.deltaTime;
        if(currentTime <= 0){
            currentTime = 1;
            fpsText.text = fps.ToString();
        }
    }
}
