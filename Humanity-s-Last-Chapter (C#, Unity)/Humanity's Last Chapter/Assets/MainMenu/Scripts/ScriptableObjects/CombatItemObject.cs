using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AbilityScript;
using static CombatAbility;

[System.Serializable]
public class CombatItemObject : ScriptableObject {

    public Sprite icon;
    public Sprite sprite;
    //den är för att kunna rita ut itemets range
    public Sprite rangeThingy;
    public string itemName = "t.ex. Strong Cookie";
    public string description = "t.ex. This cookie will give you strong power!!!";
    public int cost;
    public int coolDownTimer;
    public int placeRange;
    public int damage; //Grenade Damage
    public float aoe; //Area of Effect (Grenade blast)
    public AbilityType abilityType; 
    public CombatType combatType;
    public WeaponObject attachedWeapon;
    
    public GameObject AttachedAbility;
}
