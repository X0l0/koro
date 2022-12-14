using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogue;
    public string[] defeatedDialogue;
    public bool PlayerInRange;
    public int index;
    public bool inDialogue;

    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    private Sprite defaultSprite;
    public string faceDirection;
    public bool isEnemy;
    private PlayerMovement playerMovement;

    void Start()
    {
        index = 0;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inDialogue){ //advance one dialogue box, or close it if finished with the array
            index++;
            if(index < dialogue.Length){
                dialogueText.text = dialogue[index];
            }
            else{
                dialogueBox.SetActive(false);
                index = 0;
                inDialogue = false;
                playerMovement.ControlActive = true;
                if(isEnemy && !this.gameObject.GetComponent<BattleStarter>().isDefeated){
                    this.gameObject.GetComponent<BattleStarter>().Challenge();
                }
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange && playerMovement.ControlActive) //start dialogue if in range and dialogue hasn't already started
        {
            startDialogue();
        }
    }

    public void startDialogue(){ //initiates dialogue note: when called by BattleStarter, this runs before Start() upon returning from a battle
        inDialogue = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue[index];
        GameObject.Find("player").GetComponent<PlayerMovement>().ControlActive = false;
        turnToFacePlayer();
    }

    private void turnToFacePlayer(){ //turn to face the player based on the variables derived from ChildCollider components
        if(faceDirection == "up"){
            GetComponent<SpriteRenderer>().sprite = upSprite;
        }
        else if(faceDirection == "right"){
            GetComponent<SpriteRenderer>().sprite = rightSprite;
        }
        else if(faceDirection == "left"){
            GetComponent<SpriteRenderer>().sprite = leftSprite;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = downSprite;
        }
    }

    public void sayDefeated(){
        dialogue = defeatedDialogue;
    }
}
