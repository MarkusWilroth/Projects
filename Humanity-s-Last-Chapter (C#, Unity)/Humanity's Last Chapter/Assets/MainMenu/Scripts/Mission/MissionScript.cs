using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MissionScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { //Ska byta namn till QuestInfo eller QuestText
    private GameObject SelectedMission;
    public GameObject MissionInfo;
    public Text txtName, txtDescription, txtReward;
    public string missionName, description, gold, rs;
    public int goldReward, rsReward;
    private ScriptableQuest quest;
    

    void Start() {
        //quest = GetComponent<QuestObject>().quest;
        //txtName.text = quest.missionName;
        ////txtDescription.text = quest.description;

        //if (quest.goldReward > 0) {
        //    gold = quest.goldReward + " Gold ";
        //} else {
        //    gold = "";
        //}

        //if (quest.rsReward > 0) {
        //    rs = quest.rsReward + " RS ";
        //}
        //else {
        //    rs = "";
        //}

        ////txtReward.text = "Reward: " + gold + rs;
        //SelectedMission = GameObject.Find("forSelectedMission");
    }

    public void Pressed() {
        if(SelectedMission.transform.childCount > 0) {
            SelectedMission.GetComponent<SelectedMissionScript>().ChangeSelected();
        }
        transform.SetParent(SelectedMission.transform, false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        MissionInfo.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData) {
        MissionInfo.SetActive(false);
    }
}
//Name
//Description
//SourceID
//MissionID
//Next mission ID
//Chain mission
//Reward
//Missionstatus
//Objectives
//Bonus Objective
//Events
