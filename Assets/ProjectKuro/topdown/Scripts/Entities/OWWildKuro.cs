using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWWildKuro : MonoBehaviour
{
    //this script is meant to handle wild kuros behaviour in the overworld

    //add enum for various states?
  //timid
  //territorial
  //stationary
  //wandering
  //curious
  //predatory
  //sleeping

    //components
    public Transform target;//a local transform variable meant to be filled in with the players transform.
    public Transform homePosition;// a position on the map the wildkuro prefers to be stationed at

    //Variables
    public string enemyName;//simple string for enemy name, not currently in use.
    public float MoveSpeed;//float for overworld move speed
    public float chaseRadius;//radius where the wild kuro can sense the player

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        }
    }
}
