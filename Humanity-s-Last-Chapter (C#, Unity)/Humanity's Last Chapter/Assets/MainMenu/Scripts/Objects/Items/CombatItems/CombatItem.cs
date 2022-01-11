using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatItem : MonoBehaviour {

    public bool held;
    public string id;
    public string itemName;
    public string itemDescrip;

    public void GetData(CombatItemObject cio) {
        GetComponent<Image>().sprite = cio.icon; //Ändrar bilden till den som är vald i assets
        itemName = cio.itemName;
        id = cio.name;
        itemDescrip = cio.description;
    }

    public void EquipItem() {
        held = true;
    }

    public void UnequipItem() {
        held = false;
    }
}
