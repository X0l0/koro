using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractRange : MonoBehaviour
    //this script is put on a trigger hitbox surrounding the player, it looks for interactable objects like npcs, items, or unique observations.
    //once it finds one it displays a interactable notice in the UI, and helps facilitate the interaction from there with the player script

{
    [SerializeField]
    private GameObject InteractableNotice;

    //bool for detecting?
    //pickup item variables?

    private void Start()
    {
        InteractableNotice.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NonTriggeringCollider")) //tell the NPC.cs component which direction the player is interacting from
        {

            InteractableNotice.SetActive(true);

            //npcScript.PlayerInRange = true;

        }

        //if other compare tag ITEM

        //if other compare tag OBSERVATIONS
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NonTriggeringCollider")) //tell the NPC.cs component which direction the player is interacting from
        {

            InteractableNotice.SetActive(true);

            //npcScript.PlayerInRange = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NonTriggeringCollider")) //reset dialogue box conditions if player exits the collider
        {
            InteractableNotice.SetActive(false);
        }
    }
}
