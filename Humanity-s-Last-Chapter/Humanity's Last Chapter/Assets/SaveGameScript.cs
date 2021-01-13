using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameScript : MonoBehaviour { //Markus Wilroth

    public InputField input;
    public GameObject savesParent, saveBox, Menu, hub;
    private GameObject holder;
    private Transform windowParent;
    private string path, inSaveName;
    private int saveCounter;

    List<string> saveNameList;

    private void Start() {
        windowParent = transform.parent;
        saveNameList = new List<string>();
        input.text = WorldScript.world.saveName;

        path = Application.persistentDataPath + "/Saves/";
        saveCounter = Directory.GetFiles(path).Length;

        AutoSave();
        AllSaves();
    }

    private bool CheckSaves() { //Kollar om det finns en sparning med det namnet
        foreach (string saveName in saveNameList) {
            if (saveName == inSaveName) {
                return false;
            }
        }
        return true;
    }

    public void BtnSave() {
        inSaveName = input.text;
        if (inSaveName == WorldScript.world.saveName) {
            WorldScript.world.SaveHub(false);
        } else {
            if (CheckSaves()) { //Finns inget annat save med det namnet
                WorldScript.world.saveName = inSaveName;
                //WorldScript.world.saveId = "save" + saveCounter;
                WorldScript.world.SaveHub(false);
            } else {
                WorldScript.world.saveName = inSaveName;
                WorldScript.world.saveId = "save" + saveCounter;
                WorldScript.world.SaveHub(false);
            }
            //WorldScript.world.saveName = inSaveName;
            //WorldScript.world.saveId = "save" + saveCounter;
            //WorldScript.world.Save(false);
        }
        Destroy(gameObject);
        
    }

    public void BtnReturn() {
        holder = Instantiate(Menu, transform.parent.transform);
        holder.GetComponent<PauseMenuScript>().GetHub(hub);
        Destroy(gameObject);
    }

    #region GetSaves
    private void AllSaves() {
        for (int i = 0; i < saveCounter; i++) {
            string saveId = "save" + i;
            if (!File.Exists(path + saveId + ".save")) {
                saveCounter++;
            } else {
                SaveData data = SaveSystem.LoadWorld(saveId, false);
                holder = Instantiate(saveBox);
                holder.GetComponent<SaveBoxScript>().input = input;
                holder.GetComponent<SaveBoxScript>().saveName = data.worldData.saveName;
                holder.transform.SetParent(savesParent.transform, false);
                saveNameList.Add(data.worldData.saveName);
            }
        }
    }

    private void AutoSave() {
        if (File.Exists(path + "AutoSave.save")) {
            saveCounter--;
            SaveData data = SaveSystem.LoadWorld("AutoSave", true);
            holder = Instantiate(saveBox);
            holder.GetComponent<SaveBoxScript>().input = input;
            holder.GetComponent<SaveBoxScript>().saveName = "AutoSave";
            holder.transform.SetParent(savesParent.transform, false);
        }
    }
    #endregion
}
