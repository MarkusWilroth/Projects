using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScriptableCollection : ScriptableObject {

    public string verb;
    public string description;
    public int collectionAmount; //Ammout of things that will be collected
    public int currentAmount; //starts at 0
    public bool isComplete, isBonus, isRandomSpawn;
    public GameObject itemsToCollect; //Kanske ska bytas till något annat
    public Transform[] spawnPos;

    public Object[] startEvents;
    public Object[] endEvents;

}
