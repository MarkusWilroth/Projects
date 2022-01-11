using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MakeClothItem {
    //[MenuItem("Assets/Create/Cloth Item")]
    public static void CreateClothItem() {
        //ClothItemObject asset = ScriptableObject.CreateInstance<ClothItemObject>();
        //int itemCounter = 0;
        //itemCounter = Directory.GetFiles("Assets/ItemsFolder/Cloths/").Length;

        //if (!(itemCounter == 0)) {
        //    itemCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/ItemsFolder/Cloths/cloth" + itemCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
