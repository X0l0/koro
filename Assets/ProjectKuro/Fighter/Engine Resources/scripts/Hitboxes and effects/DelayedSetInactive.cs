using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedSetInactive : MonoBehaviour
{
    private float currentTime;
    private float initialTime;

    void Start()
    {
        currentTime = 0.222f;    
        initialTime = currentTime;
    }

    //Simple timer script that, after currentTime seconds, makes the attached GameObject inactive

    void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0){
            currentTime = initialTime;
            gameObject.SetActive(false);
        }
    }
}
