using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool PlayerInRange;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GameObject.Find("player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange && playerMovement.ControlActive)
        {
            dialogueBox.SetActive(true);
            playerMovement.ControlActive = false;
            dialogueText.text = dialogue;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && PlayerInRange && dialogueBox.activeInHierarchy){
            dialogueBox.SetActive(false);
            playerMovement.ControlActive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerInRange = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
            dialogueBox.SetActive(false);
        }   
    }

}
