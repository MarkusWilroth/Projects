using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClothScript;

[System.Serializable]
public class ClothItemObject : ScriptableObject {

    public Sprite icon;
    public Sprite portrait;
    public string itemName = "Red Jacket";
    public string description = "A warm red jacket";
    public int cost = 10;
    public ClothType clothType; //If it is headGear or cloth
    public ClothCategory clothCategory; //When this item can be aquired

    //Boost the Item gives while wearing it
    public int maxHp = 0;
    public int str = 0;
    public int def = 0;
    public int Int = 0;
    public int dex = 0;
    public int cha = 0;
    public int ldr = 0;
    public int nrg = 0;
    public int snt = 0; 
}
