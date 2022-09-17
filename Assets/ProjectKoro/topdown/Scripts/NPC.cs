using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string[] dialogue;
    public bool PlayerInRange;
    public int index;
    public bool inDialogue;

    public Sprite downSprite;
    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    private Sprite defaultSprite;
    public string faceDirection;

    void Start()
    {
        index = 0;
        inDialogue = false;
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && inDialogue){
            index++;
            if(index < dialogue.Length){
                dialogueText.text = dialogue[index];
            }
            else{
                dialogueBox.SetActive(false);
                index = 0;
                inDialogue = false;
                GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange)
        {
            inDialogue = true;
            dialogueBox.SetActive(true);
            dialogueText.text = dialogue[index];
            turnToFacePlayer();
        }
    }

    private void turnToFacePlayer(){
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
}
