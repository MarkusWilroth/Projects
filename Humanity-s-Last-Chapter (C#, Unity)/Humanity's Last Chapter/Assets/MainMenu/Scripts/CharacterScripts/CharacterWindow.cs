using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterWindow : MonoBehaviour {
    public GameObject btnSelectO;
    private GameObject partyView;
    private Texture2D imgPortrait;
    public Text txtInfo, txtName, txtTraits;
    public string info, strName, traits;
    public string ID;

    private void Start() {
        partyView = GameObject.Find("selectPartyView");
        info = "C: - S: - R: - N: - E: -";
        traits = "Traits: -";
        txtInfo.text = info;
        txtTraits.text = traits;
    }

    public void GetInfo(string strName, string ID) {
        this.strName = strName;
        this.ID = ID;
        txtName.text = strName;
    }

    public void btnSelect() {
        partyView.GetComponent<partySelectorScript>().SelectCharacter(ID);
    }

    public void ShowBtn(bool isSelected) {
        if(isSelected) {
            btnSelectO.SetActive(false);
        }
        else {
            btnSelectO.SetActive(true);
        }
    }
}
