using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetsData {

    List<WeaponObject> weaponList;
    List<ScriptableCollection> COList;
    List<LocationObject> LOList;
    List<InteractObject> IOList;

    public AssetsData(List<WeaponObject> weaponList, List<ScriptableCollection> COList, List<LocationObject> LOList, List<InteractObject> IOList) {
        this.weaponList = weaponList;
        this.COList = COList;
        this.LOList = LOList;
        this.IOList = IOList;
    }
    //Denna ska användas vid Load av alla scener och behöver INTE uppdateras mellan scenerna
}
