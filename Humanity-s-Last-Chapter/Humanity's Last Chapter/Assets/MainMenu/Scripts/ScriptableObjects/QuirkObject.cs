using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuirkScript;

[System.Serializable]
public class QuirkObject : ScriptableObject
{
    public string quirkName;    //Namnet på quirken
    public string quirkDescription; //Förklaring på quirken
    public int hp, def, dex;  //Stats som ändras av quirken
    public QuirkObject[] quirkPair; //Quirks man inte kan ha samtidigt
    public int quirkLevel;  //Vilken nivå quirken är (vissa typer av quirk kan bli mer avancerade)
    public QuirkType quirkType; //Vilken typ av quirk det är
    public BodyPart bodyPart;   //Vilken del av kroppen som påverkas
}
