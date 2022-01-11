using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealingItemObject : ScriptableObject {

    public Sprite texture;
    public string itemName = "t.ex. Weak Bandage";
    public string description = "t.ex. A bandage to coverup small wounds!!!";
    public int healPower; //Hur mycket extra hp den ger när den används
    public int cost;
    public int coolDownTimer;
    public Sprite icon;
}
