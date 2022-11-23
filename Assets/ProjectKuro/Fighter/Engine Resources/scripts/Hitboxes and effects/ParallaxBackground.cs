using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float length, startX, startY;
    public GameObject camera;
    public float parallaxEffect;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.parent.transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;    
    }

    void Update()
    {
        float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float dist = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startX + dist, transform.position.y, transform.position.z);
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, startY, transform.parent.transform.position.z);

        if(temp > startX + length){
            startX += length;
        }
        else if(temp < startX - length){
            startX -= length;
        }
    }
}
