using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChildCollider : MonoBehaviour
{
    public NPC npcScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) //tell the NPC.cs component which direction the player is interacting from
        {
            npcScript.PlayerInRange = true;
            if(gameObject.name == "UpCollider"){
                npcScript.faceDirection = "up";
            }
            else if(gameObject.name == "RightCollider"){
                npcScript.faceDirection = "right";
            }
            else if(gameObject.name == "LeftCollider"){
                npcScript.faceDirection = "left";
            }
            else{
                npcScript.faceDirection = "down";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) //reset dialogue box conditions if player exits the collider
        {
            npcScript.PlayerInRange = false;
            npcScript.dialogueBox.SetActive(false);
            npcScript.index = 0;
            npcScript.inDialogue = false;
        }   
    }
}
