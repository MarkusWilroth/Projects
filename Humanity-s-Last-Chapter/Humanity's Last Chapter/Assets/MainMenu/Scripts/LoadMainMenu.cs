using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadMainMenu : MonoBehaviour {
    
    void Start() {

        if (!Directory.Exists(Application.persistentDataPath + "/Saves")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        
        if (DataHolder.dataHolder == null) {
            DataHolder.dataHolder = new DataHolder();
            DataHolder.dataHolder.NewHolder();
        }

        DataHolder.dataHolder.CheckButtonPossible();
    }

    public void BtnContinue() {
        DataHolder.dataHolder.BtnContinue();
    }

    public void BtnNewGame() {
        DataHolder.dataHolder.BtnNewGame();
    }

    public void BtnPlay() {
        DataHolder.dataHolder.BtnPlay();
    }

    public void BtnDelete() {
        DataHolder.dataHolder.BtnDelete(); //tar bort save

        GameObject PlayMenu = GameObject.Find("PlayMenu");
        PlayMenu.GetComponent<SaveSlotManager>().PrepareSlots();
    }
}
