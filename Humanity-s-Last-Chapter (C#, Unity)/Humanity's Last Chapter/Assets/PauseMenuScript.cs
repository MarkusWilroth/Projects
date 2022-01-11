using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour {

    public GameObject saveWindow, hub;
    private GameObject holder;
    private Transform parent;

    public void GetHub(GameObject hub) {
        this.hub = hub;
    }

    public void BtnSave() {
        holder = Instantiate(saveWindow);
        holder.transform.SetParent(gameObject.transform.parent.transform, false);
        holder.GetComponent<SaveGameScript>().hub = hub;
        Destroy(gameObject);
    }

    public void BtnResume() {
        Debug.Log("Hub: " + hub.name);
        hub.SetActive(true);
        Destroy(gameObject);
    }

    public void BtnQuit() {
        WorldScript.world.SaveHub(true);
        Application.Quit();
    }

    public void BtnMainMenu() {
        SceneManager.LoadScene("SaveScreen");
    }
}
