using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public DataCard KuroData;

    [SerializeField]
    public TemplateCard SpeciesCard;//creates a serialized field for the species card that is used in creating a preset data card

    [SerializeField]
    public MatchConnecter MatchConnecter;

    // Start is called before the first frame update
    void Start()
    {
        //KuroData = new DataCard(SpeciesCard);//this fills the Data card variable with a new data card and whatever species card is plugged in
        //MatchConnecter.ConnectToPlayerBrain();//USUALLY CALLED BY KURO PARTY, SET THIS WAY FOR COMBAT TESTING.
        //Debug.Log("connecting to player brain");

        //the data card is loaded usually wayy before the rig is activated and connected. however in combat testing this is not the case
        //as everything is loaded on start, to comabt this, this start code is implememented to make the data card then
        //initiate the connectig protocols as to prevent loading errors.
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void LoadRig(DataCard NewKuroData)//this fills the data card variable with an already made data card
    {
        KuroData = NewKuroData;
    } 


}
