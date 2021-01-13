using UnityEngine;
using UnityEditor;
using System.IO;

public class MakeInteraction {
    //[MenuItem("Assets/Create/Interaction Objective")]
    public static void CreateInteractionObjective() {
        //InteractObject asset = ScriptableObject.CreateInstance<InteractObject>();
        //int missionCounter = 0;
        //missionCounter = Directory.GetFiles("Assets/MissionFolder/InteractObjectives/").Length;

        //if (!(missionCounter == 0)) {
        //    missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/MissionFolder/InteractObjectives/io" + missionCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
