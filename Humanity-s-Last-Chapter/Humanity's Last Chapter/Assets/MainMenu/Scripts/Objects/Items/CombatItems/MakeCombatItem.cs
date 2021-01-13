using UnityEngine;
using UnityEditor;
using System.IO;

public class MakeCombatItem {

    //[MenuItem("Assets/Create/Combat Item")]
    public static void CreateCombatItem() {
        //CombatItemObject asset = ScriptableObject.CreateInstance<CombatItemObject>();
        //int missionCounter = 0;
        //missionCounter = Directory.GetFiles("Assets/ItemsFolder/CombatItems/").Length;

        //if (!(missionCounter == 0)) {
        //    missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/ItemsFolder/CombatItems/ci" + missionCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
