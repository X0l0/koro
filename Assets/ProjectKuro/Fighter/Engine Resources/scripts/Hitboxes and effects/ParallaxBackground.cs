using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startX;
    public GameObject gameCamera;
    public float parallaxEffect; //The closer the object, the higher the value
    private float cameraStartX;

    void Start()
    {
        startX = transform.position.x;
        cameraStartX = gameCamera.transform.position.x;
    }

    void Update()
    {
        float dist = ((Mathf.Abs(gameCamera.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);

        transform.position = new Vector3(startX - dist, transform.position.y, transform.position.z);
        
        /*
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, startY, transform.parent.transform.position.z);
        if(temp > startX + length){
            startX += length;
        }
        else if(temp < startX - length){
            startX -= length;
        }
        */
    }
}
