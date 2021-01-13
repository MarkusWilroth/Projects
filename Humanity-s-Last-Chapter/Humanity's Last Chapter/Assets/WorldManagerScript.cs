using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManagerScript : MonoBehaviour {

    public GameObject pauseMenu, parent;
    private GameObject holder;
    private bool isMenuUp;

    void Update() {
        if (holder == null) { //Kollar om något annat fönster är öppet som är skapat härifrån
            if (Input.GetKeyDown(KeyCode.Escape)) {
                holder = Instantiate(pauseMenu);
                holder.transform.SetParent(parent.transform, false);
            }
        }

        if (Input.GetKeyDown(KeyCode.F5)) {
            WorldScript.world.SaveHub(true);
        }
    }
}
