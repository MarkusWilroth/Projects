using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringUpMenu : MonoBehaviour {

    public GameObject Menu, hub;
    private GameObject holder;
    private bool active = false;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !active) {
            holder = Instantiate(Menu, gameObject.transform);
            holder.GetComponent<PauseMenuScript>().GetHub(hub);
            hub.SetActive(false);
            active = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && active)
        {
            Destroy(holder);
            hub.SetActive(true);
            active = false;
        }
    }
}
