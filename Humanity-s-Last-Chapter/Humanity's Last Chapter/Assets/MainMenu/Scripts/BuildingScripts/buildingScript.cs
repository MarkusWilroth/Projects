using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingScript : MonoBehaviour { //Borde heta barrackScript

    public string buildingName;
    public int level;

    public GameObject BuildingWindow, WindowParent;
    private GameObject holder;

    public void btnEnterBuilding() {
        holder = Instantiate(BuildingWindow);
        holder.transform.SetParent(WindowParent.transform, false);
    }
}
