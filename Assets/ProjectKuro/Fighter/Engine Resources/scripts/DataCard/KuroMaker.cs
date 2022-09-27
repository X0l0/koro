using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KuroMaker : MonoBehaviour//rename to kuromaker 
{
    [SerializeField]
    public TemplateCard SpeciesCard;//creates a serialized field for the species card
    [SerializeField]
    public GameObject KuroRig;//this makes a reference and slot for a kuro rig
    [SerializeField]
    public BattleStarter Enemy;//this makes a reference and slot for a kuro rig

    public DataCard KuroDataCard;//creates reference and holding space for a data card called kurodata card.
    public CardHolder KuroCardHolder;//this makes a slot for a cardholder, this is so this script can get the cardholder on the rig and fill it with the loaded data card.

    public Transform KuroHoldPoint;//this is a physical point used in placing the rig somewhere rather then having it nowhere.

    private GameObject Createdkuro;

    private bool KuroSent;

    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool PlayerInRange;

    void Start()
    {
        //creating data card from species card
        KuroDataCard = new DataCard(SpeciesCard);
        CreateKuro();

        if(Enemy != null)
        {
            SendKuroToEnemy();
        }
    }

    void Update()
    {
        if(!KuroSent){ //Essentially, if this script isn't attached to an enemy
            if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange && GameObject.Find("player").GetComponent<PlayerMovement>().ControlActive)
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
                GameObject.Find("player").GetComponent<PlayerMovement>().ControlActive = false;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && PlayerInRange && dialogueBox.activeInHierarchy){
                dialogueBox.SetActive(false);
                SendKuroToPlayer();
                GameObject.Find("player").GetComponent<PlayerMovement>().ControlActive = true;
                Destroy(gameObject);
            }
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
        }   
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (KuroSent == true)
        {
            return;
        }
        else
        {
        SendKuroToPlayer();
        }
    }*/

    public void CreateKuro()//activate rig and then de activate to load all rig variables?
    {
        //creating and placing Kuro Object
        GameObject CreatedKuro = Instantiate(KuroRig, KuroHoldPoint.position, transform.rotation);//this instantiates a copy of the rig from the rig slot.
        CreatedKuro.transform.parent = this.transform;//this sets the newly instantiated rig as a child of the kuro maker.

        //Loads created rig's data card holder with the created data card
        KuroCardHolder = CreatedKuro.GetComponent<CardHolder>();
        KuroCardHolder.LoadRig(KuroDataCard);//find rig based on data card?

        Createdkuro = CreatedKuro;

        KuroSent = false;
    }


    public void SendKuroToPlayer()
    {

        //Load Kuro Object into player KuroParty
        Createdkuro.transform.parent = KuroParty.instance.transform;//this places the newly created Kuro as a child to the player.

        //KuroParty.instance.AddKuro(Createdkuro);
        KuroParty.instance.AddKuroObject(Createdkuro.transform);//this logs the transform of the created Kuro in the player inventory.

        KuroSent = true;
    }

    public void SendKuroToEnemy()
    {
        //Load Kuro Object into player KuroParty
        Createdkuro.transform.parent = Enemy.transform;//this places the newly created Kuro as a child to the player.
        Enemy.AddKuroObject(Createdkuro.transform);//this logs the transform of the created Kuro in the player inventory.
        KuroSent = true;
    }

}
