
using UnityEngine;
using UnityEditor;
using System.IO;

public class MakeQuirk : MonoBehaviour
{
    //[MenuItem("Assets/Create/Quirk Object")]
    public static void CreateQuirkObject()
    {
        //QuirkObject asset = ScriptableObject.CreateInstance<QuirkObject>();
        //int quirkCounter = 0;
        //quirkCounter = Directory.GetFiles("Assets/QuirkFolder/").Length;

        //if (!(quirkCounter == 0))
        //{
        //    quirkCounter /= 2; //Vet inte varför den räknar dubbelt så fixade detta... om någon vet vad man kan göra så fixa det snyggare
        //}
        //AssetDatabase.CreateAsset(asset, "Assets/QuirkFolder/qu" + quirkCounter + ".asset");
        //AssetDatabase.SaveAssets();
        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = asset;
    }
}
