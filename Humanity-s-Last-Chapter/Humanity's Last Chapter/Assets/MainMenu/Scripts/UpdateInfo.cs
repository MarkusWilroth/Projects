using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateInfo : MonoBehaviour {

    public Text txtName, txtDesc;

    public void UpdateText(string name, string desc) {
        txtName.text = name;
        txtDesc.text = desc;
    }
}
