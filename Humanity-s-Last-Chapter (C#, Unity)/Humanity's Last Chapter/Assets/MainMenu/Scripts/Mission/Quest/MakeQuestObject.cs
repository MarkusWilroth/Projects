using UnityEngine;
using UnityEditor;
using System.IO;
using QuestSystem;

public class MakeQuestObject {

    //[MenuItem("Assets/Create/Quest Objective")]
    public static void CreateQuestObject() {
        //ScriptableQuest asset = ScriptableObject.CreateInstance<ScriptableQuest>();
        //int missionCounter = 0;
        //missionCounter = Directory.GetFiles("Assets/MissionFolder/Quests/").Length;

        //if (!(missionCounter == 0)) {
        //    missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //Debug.Log("MissionCounter: " + missionCounter);
        //AssetDatabase.CreateAsset(asset, "Assets/MissionFolder/Quests/mi" + missionCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }

}
