using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    #region singleton
    public static HitStop instance;
    //static variables are variables that are shared in every instance of a class.
    //when starting the game you set the static variable to this script, which means there will only ever be one kuroparty
    //and you can call it easily by just calling PlayerMovement.instance
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogWarning("More then one instance of MatchManager found");
            return;
        }
        instance = this;
    }
    #endregion

    bool waiting;
    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.1f;
        //Debug.Log("hitstopactivated");
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
