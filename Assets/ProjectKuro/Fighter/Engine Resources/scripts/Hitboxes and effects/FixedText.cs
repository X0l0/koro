using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedText : MonoBehaviour
{
    private GameObject Follow;

    void Start()
    {
        Follow = transform.parent.GetChild(0).gameObject;    
    }

    //Simple script to make whatever is attached follow slightly above the first sibling game object

    void Update()
    {
        transform.position = new Vector3(Follow.transform.position.x, Follow.transform.position.y + 5, Follow.transform.position.z);
    }
}
