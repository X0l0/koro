using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KoroMaker : MonoBehaviour//rename to koromaker 
{
    [SerializeField]
    public TemplateCard SpeciesCard;//creates a serialized field for the species card
    [SerializeField]
    public GameObject KoroRig;//this makes a reference and slot for a koro rig
    [SerializeField]
    public BattleStarter Enemy;//this makes a reference and slot for a koro rig

    public DataCard KoroDataCard;//creates reference and holding space for a data card called korodata card.
    public CardHolder KoroCardHolder;//this makes a slot for a cardholder, this is so this script can get the cardholder on the rig and fill it with the loaded data card.

    public Transform KoroHoldPoint;//this is a physical point used in placing the rig somewhere rather then having it nowhere.

    private GameObject Createdkoro;

    private bool KoroSent;

    public GameObject dialogueBox;
    public Text dialogueText;
    public string dialogue;
    public bool PlayerInRange;

    void Start()
    {
        //creating data card from species card
        KoroDataCard = new DataCard(SpeciesCard);
        CreateKoro();

        if(Enemy != null)
        {
            SendKoroToEnemy();
        }
    }

    void Update()
    {
        if(!KoroSent){ //Essentially, if this script isn't attached to an enemy
            if (Input.GetKeyDown(KeyCode.Space) && PlayerInRange)
            {
                if(dialogueBox.activeInHierarchy)
                {
                    dialogueBox.SetActive(false);
                    SendKoroToPlayer();
                    GameObject.Find("player").GetComponent<OWPlayerMovement>().ControlActive = true;
                    Destroy(gameObject);
                }
                else
                {
                    dialogueBox.SetActive(true);
                    dialogueText.text = dialogue;
                    GameObject.Find("player").GetComponent<OWPlayerMovement>().ControlActive = false;
                }
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
        if (KoroSent == true)
        {
            return;
        }
        else
        {
        SendKoroToPlayer();
        }
    }*/

    public void CreateKoro()//activate rig and then de activate to load all rig variables?
    {
        //creating and placing Koro Object
        GameObject CreatedKoro = Instantiate(KoroRig, KoroHoldPoint.position, transform.rotation);//this instantiates a copy of the rig from the rig slot.
        CreatedKoro.transform.parent = this.transform;//this sets the newly instantiated rig as a child of the koro maker.

        //Loads created rig's data card holder with the created data card
        KoroCardHolder = CreatedKoro.GetComponent<CardHolder>();
        KoroCardHolder.LoadRig(KoroDataCard);//find rig based on data card?

        Createdkoro = CreatedKoro;

        KoroSent = false;
    }


    public void SendKoroToPlayer()
    {

        //Load Koro Object into player KoroParty
        Createdkoro.transform.parent = KoroParty.instance.transform;//this places the newly created Koro as a child to the player.

        KoroParty.instance.AddKoro(Createdkoro);//this is the new add koro to party script as it passses in game object
        //KoroParty.instance.AddKoroObject(Createdkoro.transform);//this logs the transform of the created Koro in the player inventory.

        KoroSent = true;
    }

    public void SendKoroToEnemy()
    {
        //Load Koro Object into player KoroParty
        Createdkoro.transform.parent = Enemy.transform;//this places the newly created Koro as a child to the player.
        Enemy.AddKoroObject(Createdkoro.transform);//this logs the transform of the created Koro in the player inventory.
        KoroSent = true;
    }

}
