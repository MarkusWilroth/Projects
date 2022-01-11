using System.IO;
using UnityEngine;

public class SaveSlotManager : MonoBehaviour {
    public GameObject saveSlot;
    private GameObject saveHolder, parent;

    public string save;

    private int saveCounter;
    private string path;

    private void Start() {
        PrepareSlots();        
    }

    public void PrepareSlots() {
        path = Application.persistentDataPath + "/Saves/";
        saveCounter = Directory.GetFiles(path).Length;
        parent = GameObject.FindGameObjectWithTag("forSaves");

        foreach (Transform child in parent.transform) { //Rensar alla barn som redan finns
            Destroy(child.gameObject);
        }

        AutoSave();
        AllSaves();
    }

    private void AllSaves() {
        for (int i = 0; i < saveCounter; i++) {
            string saveId = "save" + i;
            if (!File.Exists(path + saveId + ".save")) {
                saveCounter++;
            } else {
                SaveData data = SaveSystem.LoadWorld(saveId, false);
                saveHolder = Instantiate(saveSlot);
                saveHolder.GetComponent<SaveSlot>().saveId = saveId;
                saveHolder.GetComponent<SaveSlot>().saveName = data.worldData.saveName;
                saveHolder.transform.SetParent(parent.transform, false);
            }
        }
    }

    private void AutoSave() {
        if (File.Exists(path + "AutoSave.save")) {
            saveCounter--;
            SaveData data = SaveSystem.LoadWorld("AutoSave", true);
            saveHolder = Instantiate(saveSlot);
            saveHolder.GetComponent<SaveSlot>().saveId = "AutoSave";
            saveHolder.GetComponent<SaveSlot>().saveName = "AutoSave";
            saveHolder.transform.SetParent(parent.transform, false);
        }
    }

    public void BtnReturn() {
        DataHolder.dataHolder.CheckButtonPossible();
    }
}
