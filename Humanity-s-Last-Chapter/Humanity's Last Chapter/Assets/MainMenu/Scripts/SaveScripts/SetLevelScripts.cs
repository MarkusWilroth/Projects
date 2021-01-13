using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLevelScripts : MonoBehaviour {
    public int setSaveSlot;
    GameObject saveManager;

    void SetSlot() {
        saveManager = GameObject.Find("SlotManager");
        //saveManager.GetComponent<SaveSlotManager>().activeSave = setSaveSlot;
    }

}
