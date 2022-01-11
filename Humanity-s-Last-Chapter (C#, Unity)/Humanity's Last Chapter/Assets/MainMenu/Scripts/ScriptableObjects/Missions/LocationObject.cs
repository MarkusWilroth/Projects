using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocationObject : ScriptableObject {

    public string titel = "t.ex. Enter the Kitchen";
    public string description = "t.ex. A distress call from the Dinner Room calls for an investigation";
    public string id = "t.ex. lo0";
    public Transform locationPos;           //Om isSpawned kommer location att skapas här
    public GameObject Location;
    public GameObject[] clearOutTarget; //I fall man vill ha en quest som handlar om att rensa rum av fiender
    public bool isComlete;
    public bool isBonus;
    public bool spawnLo;

    public Object[] startEvents;
    public Object[] endEvents;

}
