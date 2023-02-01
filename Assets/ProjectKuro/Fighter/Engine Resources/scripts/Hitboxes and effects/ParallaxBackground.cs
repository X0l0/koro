using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startX;
    public GameObject gameCamera;
    public float parallaxEffect; //The closer the object, the higher the value
    private float cameraStartX;
    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        startX = transform.position.x;
        cameraStartX = gameCamera.transform.position.x;
    }

    void Update()
    {
        float dist = 0;
        if(gameCamera.activeSelf){ //Only move background if the camera is active, i.e. match has started
            if(player1 == null || player2 == null){ //If not already assigned, assign them to each player
                player1 = GameObject.Find("PlayerBrain").transform.GetChild(0).GetChild(0).gameObject;
                player2 = GameObject.Find("EnemyBrain").transform.GetChild(0).GetChild(0).gameObject;
            }
            //If one player is right of the start position, and the other player is left of the start position...
            if(player1.transform.position.x > cameraStartX && player2.transform.position.x < cameraStartX || 
            player1.transform.position.x < cameraStartX && player2.transform.position.x > cameraStartX){
                float dist1 = Mathf.Abs(player1.transform.position.x) + Mathf.Abs(cameraStartX);
                float dist2 = Mathf.Abs(player2.transform.position.x) + Mathf.Abs(cameraStartX);
                //And player1 is furthest away from the center...
                if(dist1 > dist2){
                    //Move the background based on player1's position
                    dist = ((Mathf.Abs(player1.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
                //And player2 is furthest away from the center...
                else{
                    //Move the background based on player2's position
                    dist = ((Mathf.Abs(player2.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
            }
            //else If a player is right of the start position...
            else if(player1.transform.position.x > cameraStartX || player2.transform.position.x > cameraStartX){
                //And player1 is further right than player2...
                if(player1.transform.position.x > player2.transform.position.x){
                    //Move the background based on player1's position
                    dist = ((Mathf.Abs(player1.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
                //Else if player2 is further right than player1...
                else{
                    //Move the background based on player2's position
                    dist = ((Mathf.Abs(player2.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
            }
            //Else if a player is left of the start position...
            else{
                //And player1 is further left than player2...
                if(player1.transform.position.x < player2.transform.position.x){
                    //Move the background based on player1's position
                    dist = ((Mathf.Abs(player1.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
                //Else if player2 is further left than player1...
                else{
                    //Move the background based on player2's position
                    dist = ((Mathf.Abs(player2.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);
                }
            }
        }

        //float dist = ((Mathf.Abs(gameCamera.transform.position.x) - Mathf.Abs(cameraStartX)) * parallaxEffect);

        //Formula to move the background's transform in a parallax fashion
        transform.position = new Vector3(startX - dist, transform.position.y, transform.position.z);
    }

    void OnDisable(){
        //When the battle is over, reset these variables
        player1 = null;
        player2 = null;
    }
}
