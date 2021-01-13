using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedMissionScript : MonoBehaviour {

    private GameObject MissionList;
    private Transform SelectedMission;

    void Start() {
        MissionList = GameObject.Find("forMissionList");
        
    }

    public void ChangeSelected() {
        SelectedMission = transform.GetChild(0);
        SelectedMission.SetParent(MissionList.transform, false);
    }
}
