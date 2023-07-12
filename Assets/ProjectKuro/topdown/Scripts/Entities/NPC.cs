using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCState
{
    Idle,//set to on startup, when stopping movement, and after finished talking
    walk,//set to when walking bool is set to true
    Interact,//set to when dialouge is started 
}

public class NPC : MonoBehaviour//this script is a base class for npcs. it is meant to handle npc movement and dialogue 
{
    //components
    private Animator animator;
    private Rigidbody2D myRigidbody2D;
    private PlayerMovement playerMovement;//connects to player for changing player control, change with dedicated code in player?

    //npc variables
    public bool PlayerInRange;//if player is in range for interacting, this is defined in the npc child collider script
    public bool isEnemy;//decides whether npc is enemy or not, switch with inheritated prefab?
    public string faceDirection;//defined in npc child script, can be up down left or right
    public bool KuroToGive = true;

    //state control
    public NPCState currentstate;//current state of the enum
    public bool walking = false;
    public bool inDialogue = false;

    //animator communication
    public float MoveX;//responsible for managing input to animators
    public float MoveY;

    //Ui elements
    public GameObject dialogueBox;
    public Text dialogueText;
    public GameObject DialoguePortrait;//dialogue portrait ui element

    //dialogue content
    public int index;//used in cycling through dialogues
    public string[] dialogue;
    public string[] defeatedDialogue;
    public Sprite portraitSprite;//dialogue portrait sprite

    void Start()
    {
        //gets components
        animator = GetComponent<Animator>();
        //myRigidbody2D = GetComponent<Rigidbody2D>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();//connects to playermovement

        //initilizes state
        currentstate = NPCState.Idle;

        //sets animator to look down and not move.
        animator.SetFloat("MoveX", 0f);
        animator.SetFloat("MoveY", -1f);
        animator.SetBool("Moving", false);

        //sets index at 0
        index = 0;
    }

    void Update()
    {
        if(!walking && !inDialogue && currentstate == NPCState.Idle)//this code is used to prevent a loading bug when returning from combat as well as a general reset to make things look nice
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", -1);
        }

        //starts dialogue
        if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange && playerMovement.ControlActive && !inDialogue) //start dialogue if in range and dialogue hasn't already started, replace input with direct pick up signal from player?
        {
            startDialogue();
        }

        //continues dialouge
        else if (inDialogue && Input.GetKeyDown(KeyCode.Space))//if talking and pressing space
        {
            index++;//goes to the next index number

            if (index < dialogue.Length)//if the index is less then aka not finished with the number of dialouge indexes
            {
                dialogueText.text = dialogue[index];//fills in UI text with current index.
            }

            //ends dialogue
            else//when indexes run out aka no more dialouge
            {
 
                if(KuroToGive == true)
                {
                    GiveKuro();
                }


                stopDialogue();


                    //if the npc is an enemy, it will start combat after dialogue.
                if (isEnemy && !this.gameObject.GetComponent<BattleStarter>().isDefeated)
                {
                    this.gameObject.GetComponent<BattleStarter>().Challenge();
                }


          
         

            }
        }

        else if (walking == true)//if walking is set to true, walks downwards, stops walking if runs into event collider, see bottom of script 
        {
            currentstate = NPCState.walk;
            animator.SetBool("Moving", true);
            animator.SetFloat("MoveX", 0f);
            animator.SetFloat("MoveY", -1f);
            transform.position += Vector3.down * Time.deltaTime * 2f;
        }
    }

    public void startDialogue()
    { //initiates dialogue note: when called by BattleStarter, this runs before Start() upon returning from a battle
        StopMoving();
        inDialogue = true;//activates bool
        currentstate = NPCState.Interact;
      
        playerMovement.ControlActive = false;//have talking movement bool in player itself

        //turns on dialogue elements
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue[index];//fills in ui with text from the current index
        DialoguePortrait.SetActive(true);//sets dialouge portrait to active
        DialoguePortrait.GetComponent<Image>().sprite = portraitSprite;//sets current dialogue portrait as the one attached to the current npc




        turnToFacePlayer();
    }

    public void stopDialogue()
    {
        //turns Ui elements off 
        dialogueBox.SetActive(false);
        DialoguePortrait.SetActive(false);

        //reset index
        index = 0;

        //resets bool
        inDialogue = false;

        //sets current state to idle as talking is done
        currentstate = NPCState.Idle;

        //sets animators to look back down
        animator.SetFloat("MoveX", 0f);
        animator.SetFloat("MoveY", -1f);

        //gives control back to player, replace with control from player enum?
        playerMovement.ControlActive = true;
    }

    private void turnToFacePlayer()
    { //turn to face the player based on the variables derived from ChildCollider components, replace with statemachine?
        if (faceDirection == "up")
        {
            animator.SetFloat("MoveX", 0f);
            animator.SetFloat("MoveY", 1f);
        }
        else if (faceDirection == "right")
        {
            animator.SetFloat("MoveX", 1f);
            animator.SetFloat("MoveY", 0f);
        }
        else if (faceDirection == "left")
        {
            animator.SetFloat("MoveX", -1);
            animator.SetFloat("MoveY", 0);
        }
        else
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", -1);
        }
    }

    public void sayDefeated()
    {//called by overworld game manager after leaving combat, if player wins calls this from whatver current enemy npc that was just fought. 
        dialogue = defeatedDialogue;
    }

    public void StopMoving()
    {
        walking = false;
        currentstate = NPCState.Idle;
        animator.SetBool("Moving", false);
    }

    public void GiveKuro()
    {
        KuroToGive = false;
        GameObject.Find("KuroPickup").GetComponent<KuroMaker>().SendKuroToPlayer();//sends kuro to plyaer
        Destroy(GameObject.Find("KuroPickup"));//destroys kuromaker gameobject

        //updates with some new dialogue
        dialogue[0] = "You can use your Kuro to fight with other Kuro and trainers.";
        dialogue[1] = "You can take up to six Kuro with you on your journeys, once you've caught them.";
        dialogue[2] = "Now go out there and see what the world has to offer you!";

        //goes to idle?
    }

    void OnTriggerEnter2D(Collider2D other)//when colliding with an event collider, in this specific case the npc is walking up, showing a kuro, and t
    {
        if (other.name == "Event00Collider")
        {
            Destroy(this.gameObject.GetComponent<Rigidbody2D>()); //needed to collide with the stopping point, but no longer needed removes rgb not needed?

            playerMovement.SetXY(0f, 1f);//has player looking up at npc, uses hardcoding in oppose to orientation logic.

            //myRigidbody2D.GetComponent<Rigidbody2D>().simulated = false;
            //this.gameObject.GetComponent<Animator>().enabled = false;//disables animator not needed
            //talking = true;//is talking now
            //walking = false;//no more walking

            GameObject.Find("KuroPickup").GetComponent<SpriteRenderer>().enabled = true;//finds kuro maker and makes it visible

            StopMoving();
            startDialogue();
        }
    }
}
