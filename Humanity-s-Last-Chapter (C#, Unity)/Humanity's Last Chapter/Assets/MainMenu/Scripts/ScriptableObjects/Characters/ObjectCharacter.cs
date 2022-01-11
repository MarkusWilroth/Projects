using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCharacter : ScriptableObject {

    public string strName = "Ander Andersson";
    public int id;
    public List<WeaponObject> equpedWeapon;
    public List<ClothItemObject> cloth;
    public CharacterScript.Faction faction;
    public bool isEssential;
    public GameObject Prefab;
}
