using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event00Script : MonoBehaviour//script used in the intro cutscene
{
    //bools on the current actions of the npc, switch with enum state machine? already switched over 
    //private bool walking;
    //private bool talking;
    //private bool giving;
    //private bool inDialogue;

    //private NPC npcScript;//conncects to npc script, combining should make this null.

    [SerializeField]
    public bool isFinished;//bool responsible for enacting cutscene
    
    void Start()
    {
        //if(isFinished){//if intro is not in use destroy instead
        //    Destroy(this);
        //}
        //walking = true;
        //talking = false;
        //giving = false;
        //inDialogue = false;

        //these 2 are used in intro cutscene logic, put it in game manager?
        //GameObject.Find("Player").GetComponent<PlayerMovement>().ControlActive = false;//sets player to no movement
        //GameObject.Find("Player").GetComponent<PlayerMovement>().SetXY(0, 1);//sets players facing direction

        //conncets this script to npc, should be redundant after combination
        //npcScript = this.gameObject.GetComponent<NPC>();//fills in UI?
    }

    void Update()
    {
        //if(walking){//npc moves forward
        //    transform.position += -Vector3.up * Time.deltaTime * 2f;
        //}
        //else if(talking){//npc stops moving and starts dialogue 
        //    if(!npcScript.inDialogue && !inDialogue){
        //        npcScript.startDialogue();
        //        inDialogue = true;
        //    }
        //    else if(!npcScript.inDialogue && inDialogue){
        //        talking = false;
        //        giving = true;
        //    }
        //}
        //else if(giving){
        //    GameObject.Find("KuroMaker").GetComponent<KuroMaker>().SendKuroToPlayer();
        //    Destroy(GameObject.Find("KuroMaker"));
        //    npcScript.dialogue[0] = "You can use your Kuro to fight with other Kuro and trainers.";
        //    npcScript.dialogue[1] = "You can take up to six Kuro with you on your journeys, once you've caught them.";
        //    npcScript.dialogue[2] = "Now go out there and see what the world has to offer you!";
        //    isFinished = true;
        //    Destroy(this);
        //}
    }

    //void OnTriggerEnter2D(Collider2D other){
    //    if(other.name == "Event00Collider"){
    //        Destroy(this.gameObject.GetComponent<Rigidbody2D>()); //needed to collide with the stopping point, but no longer needed
    //        GameObject.Find("KuroMaker").GetComponent<SpriteRenderer>().enabled = true;
    //        this.gameObject.GetComponent<Animator>().enabled = false;
    //        walking = false;
    //        talking = true;
    //    }
    }

