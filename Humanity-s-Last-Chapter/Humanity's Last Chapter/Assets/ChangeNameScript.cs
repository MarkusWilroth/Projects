using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeNameScript : MonoBehaviour {

    public InputField input;

    public void BtnReturn() {
        if(input.text != "") {
            WorldScript.world.saveName = input.text;
        }
        Destroy(gameObject);
    }
}
