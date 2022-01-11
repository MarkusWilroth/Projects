using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBoxScript : MonoBehaviour {

    public InputField input;
    public string saveName;
    public Text txtSaveName;

    private void Start() {
        txtSaveName.text = saveName;
    }

    public void Select() {
        input.text = saveName;
    }
}
