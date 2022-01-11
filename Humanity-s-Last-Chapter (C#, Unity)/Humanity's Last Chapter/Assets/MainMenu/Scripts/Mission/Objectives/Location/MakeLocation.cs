using UnityEngine;
using UnityEditor;
using System.IO;
using QuestSystem;


public class MakeLocation {

    //[MenuItem("Assets/Create/Location Objective")]
    public static void CreateLocationObjective() {
        //LocationObject asset = ScriptableObject.CreateInstance<LocationObject>();
        //int missionCounter = 0;
        //missionCounter = Directory.GetFiles("Assets/MissionFolder/LocationObjectives/").Length;

        //if (!(missionCounter == 0)) {
        //    missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/MissionFolder/LocationObjectives/lo" + missionCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
