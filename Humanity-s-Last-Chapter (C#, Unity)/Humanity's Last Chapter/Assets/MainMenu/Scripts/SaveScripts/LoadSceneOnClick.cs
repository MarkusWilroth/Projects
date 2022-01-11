using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    private int saveSlot;

    //public void LoadScene(string sceneName)
    //{
    //    saveSlot = GameObject.Find("SlotManager").GetComponent<SaveSlotManager>().activeSave;
    //    SaveSystem.ActiveSave(saveSlot);
    //    SceneManager.LoadScene(sceneName);
    //}

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

