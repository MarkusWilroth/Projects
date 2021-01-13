using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : MonoBehaviour {
    
    public GameObject StorageWindow, WindowParent;
    private GameObject Holder;

    public void BtnStorage() {
        Holder = Instantiate(StorageWindow);
        Holder.transform.SetParent(WindowParent.transform, false);
        Holder.transform.position = Vector3.zero;
    }
}
