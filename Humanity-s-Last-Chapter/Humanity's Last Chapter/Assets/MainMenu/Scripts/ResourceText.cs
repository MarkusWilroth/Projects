using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceText : MonoBehaviour {

    public TMPro.TextMeshProUGUI tGold, tRs;


    void Update() {
        tGold.text = WorldScript.world.gold.ToString();
    }
}
