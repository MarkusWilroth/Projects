using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityTitle : MonoBehaviour {

    public Text title;
    public GameObject ChangeWindow, parent;
    private GameObject holder;

    private void Start() {
        //title.text = WorldScript.world.saveName;
    }

    public void BtnChangeName() {
        //holder = Instantiate(ChangeWindow);
        //holder.transform.SetParent(parent.transform, false);
    }

    private void Update() {
        title.text = WorldScript.world.saveName;
    }
}
