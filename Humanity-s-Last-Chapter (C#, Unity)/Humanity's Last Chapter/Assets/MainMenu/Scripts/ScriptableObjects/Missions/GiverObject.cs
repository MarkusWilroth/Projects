using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GiverObject : ScriptableObject {

    public string characterName;
    public ScriptableObject quest;
    public GameObject QuestGiver;
}
