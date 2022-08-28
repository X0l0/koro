using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use IO systems eventually to help with saving.

[System.Serializable]//this is important to help make sure this class type is saveable.

public class DataCard//this class is a datacard template, it has properties that pull from a species card to calculates stats. 
{
    TemplateCard SpeciesCard;//this is a scriptable object of type template card, this variable holds whatever species card is slotted in when being created and used.

    string Nickname = "unnamed";

    int Level;
    int MaxHp;
    int Atk;
    int Def;
    int Spdef;

    int CurrentHp;
   

    public DataCard(TemplateCard SCard)//this is a constructor that is used whenever this class is made, it requires a species card.
    {
        SpeciesCard = SCard;
        Level = SpeciesCard.BaseLevel;
        CalcStats();
        CurrentHp = MaxHp;//change MaxHp with HP? store all properties as variables.
    }
    public void CalcStats()
    {
        MaxHp = (((Level* SpeciesCard.HP * 2) / 100) + Level  + 10);//may need Mathf.FloorToInt
        Atk = (((Level * SpeciesCard.ATK * 2) / 100) + 5);
        Def = (((Level * SpeciesCard.DEF * 2) / 100) + 5);
        Spdef = (((Level * SpeciesCard.SPDEF * 2) / 100) + 5);
        return;
    }

    public void FullHeal()
    {
        CurrentHp = MaxHp;
    }


    public void Rename(string nickname)
    {
        Nickname = nickname;
    }

    public void SetHP(int NewHP) 
    {
        CurrentHp = NewHP;
    }

    public void LevelUp()
    {
        Level = Level + 1;
        CalcStats();
    }

    //save to json? these may be unnessary. in theory you would just save the entire game somewhere, them being able to save themselves is useful, though how you would reload them is kind of confusing.

    //load from json?

    //you would have a initial create function that takes in a species card and generates new info.
    // maybe for saving you would be able to load the data card with a scriptable object data card that would just fill in the data that way.
    //that way when saving you would just need a data card. though i think you can save whole classes in which case that is stupid.e

    #region Properties
    //these properties can be called as variables, they expose the classes information.
    public int DexNo { get { return SpeciesCard.DexNo; } }
    public string SpeciesName { get { return SpeciesCard.SpeciesName; } }
    public string NickName { get { return Nickname; } }
    public int LVL { get { return Level; } }
    public int MaxHP { get { return MaxHp; }}
    public int CurrHP { get { return CurrentHp; } }
    public int ATTACK { get { return Atk; } }
    public int DEF { get { return Def; } }
    public int SPDEF { get { return Spdef; } }
    #endregion
}
