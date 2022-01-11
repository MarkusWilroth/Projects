using UnityEngine;

[System.Serializable]
public class InteractObject : ScriptableObject {

    public string title = "t.ex. Talk with this person";
    public string verb = "t.ex. talk";
    public string id = "qg0";
    public string description = "It must have been that guy that made the distress call!";
    public Transform spawnPos;
    public bool isComplete, isBonus, isSpawned, isRemoved;
    public GameObject interactObjective; //Object to interact with

    public Object[] startEvents;
    public Object[] endEvents;

}
