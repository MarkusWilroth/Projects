using UnityEngine;
using UnityEditor;
using System.IO;
using QuestSystem;


public class MakeQuestGiver : MonoBehaviour {

    //[MenuItem("Assets/Create/Quest Giver")]
    public static void CreateQuestGiver() {
        //GiverObject asset = ScriptableObject.CreateInstance<GiverObject>();
        //int missionCounter = 0;
        //missionCounter = Directory.GetFiles("Assets/CharacterFolder/QuestGivers").Length;

        //if (!(missionCounter == 0)) {
        //    missionCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //Debug.Log("MissionCounter: " + missionCounter);
        //AssetDatabase.CreateAsset(asset, "Assets/CharacterFolder/QuestGivers/qg" + missionCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
