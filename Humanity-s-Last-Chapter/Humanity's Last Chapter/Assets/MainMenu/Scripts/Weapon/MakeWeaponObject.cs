using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class MakeWeaponObject {

    //[MenuItem("Assets/Create/Weapon Object")]
    public static void CreateWeaponObject() {
        //WeaponObject asset = ScriptableObject.CreateInstance<WeaponObject>();
        //int weaponCounter = Directory.GetFiles("Assets/WeaponFolder/").Length;

        //if (!(weaponCounter == 0)) {
        //    weaponCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/WeaponFolder/wp" + weaponCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
