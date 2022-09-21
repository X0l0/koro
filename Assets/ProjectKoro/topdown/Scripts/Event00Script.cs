using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event00Script : MonoBehaviour
{
    private bool walking;
    private bool talking;
    private bool giving;
    private bool inDialogue;
    public Sprite PlayerUpSprite;
    private NPC npcScript;
    
    void Start()
    {
        walking = true;
        talking = false;
        giving = false;
        inDialogue = false;
        GameObject.Find("player").GetComponent<PlayerMovement>().ControlActive = false;
        GameObject.Find("player").GetComponent<Animator>().enabled = false;
        GameObject.Find("player").GetComponent<SpriteRenderer>().sprite = PlayerUpSprite;
        npcScript = this.gameObject.GetComponent<NPC>();
    }

    void Update()
    {
        if(walking){
            transform.position += -Vector3.up * Time.deltaTime;
        }
        else if(talking){
            if(!npcScript.inDialogue && !inDialogue){
                npcScript.startDialogue();
                inDialogue = true;
            }
            else if(!npcScript.inDialogue && inDialogue){
                talking = false;
                giving = true;
            }
        }
        else if(giving){
            GameObject.Find("KoroMaker").GetComponent<KoroMaker>().SendKoroToPlayer();
            Destroy(GameObject.Find("KoroMaker"));
            npcScript.dialogue[0] = "You can use your Kuro to fight with other Kuro and trainers.";
            npcScript.dialogue[1] = "You can take up to six Kuro with you on your journeys, once you've caught them.";
            npcScript.dialogue[2] = "Now go out there and see what the world has to offer you!";
            GameObject.Find("player").GetComponent<Animator>().enabled = true;
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.name == "Event00Collider"){
            Destroy(this.gameObject.GetComponent<Rigidbody2D>()); //needed to collide with the stopping point, but no longer needed
            GameObject.Find("KoroMaker").GetComponent<SpriteRenderer>().enabled = true;
            this.gameObject.GetComponent<Animator>().enabled = false;
            walking = false;
            talking = true;
        }
    }
}
