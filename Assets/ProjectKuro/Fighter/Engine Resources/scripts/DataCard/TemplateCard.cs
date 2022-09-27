using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kuro/Create new Species Card")]

public class TemplateCard : ScriptableObject
{
    [SerializeField] int dexNo;
    [SerializeField] string Speciesname;

    [SerializeField] int hP;
    [SerializeField] int aTK;
    [SerializeField] int dEF;
    [SerializeField] int sPDEF;

    [SerializeField] int baseLevel;

    //other base species data like gender values, base happiness, etc.

    public int DexNo//this makes a property, using properties helps expose variables.property uses same name as variable but first letter is captial.
    {
        get { return dexNo; }//get is a function that retrieves and puts in the property.
    }//these expose variables outside of class.lets you use it like a variable  but performs a background function.
    public string SpeciesName
    {
        get { return Speciesname; }
    }

    public int HP
    {
        get { return hP; }
    }

    public int ATK
    {
        get { return aTK; }
    }

    public int DEF
    {
        get { return dEF; }
    }

    public int SPDEF
    {
        get { return sPDEF; }
    }

    public int BaseLevel
    {
        get { return baseLevel; }
    }
}
