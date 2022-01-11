using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour {
    public ScriptableQuest quest;
    public GameObject QuestHolder;
    private GameObject QuestHolderO;

    void Start() { //Start ska sen bort!
        //QuestHolderO = Instantiate(QuestHolder);
        //QuestHolderO.transform.SetParent(GameObject.Find("forMissions").transform, false);
        //QuestHolderO.transform.localScale.Scale(new Vector3(1, 1, 1));
        //QuestHolderO.GetComponent<QuestObject>().GetQuest(quest);
    }

    public void ActivateQuest() {
        //Debug.Log("Does it get here?");
        //QuestHolderO = Instantiate(QuestHolder);
        //QuestHolderO.GetComponent<QuestObject>().GetQuest(quest);
    }

    public void InteractWithQuestgiver() {

    }
}
