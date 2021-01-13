using System.Collections;
using System.Collections.Generic;
using System.IO;
//using UnityEditor;
using UnityEngine;

public class Assets {
    //Items
    public WeaponObject[] weaponTemp;
    public HealingItemObject[] healingTemp;
    public CombatItemObject[] combatTemp;
    public ClothItemObject[] clothTemp;

    //Quest
    public ScriptableQuest[] questTemp; //Har alla quests som mall
    public ScriptableCollection[] coTemp;
    public LocationObject[] loTemp;
    public InteractObject[] ioTemp;

    public QuirkObject[] quirkArray;


    public static Assets assets;

    public void GetAssets() { //Får alla assets
        weaponTemp = GetAtPath<WeaponObject>("WeaponFolder");
        healingTemp = GetAtPath<HealingItemObject>("ItemsFolder/HealingItems");
        combatTemp = GetAtPath<CombatItemObject>("ItemsFolder/CombatItems");
        clothTemp = GetAtPath<ClothItemObject>("ItemsFolder/Cloths");

        questTemp = GetAtPath<ScriptableQuest>("MissionFolder/Quests");
        loTemp = GetAtPath<LocationObject>("MissionFolder/LocationObjectives");
        coTemp = GetAtPath<ScriptableCollection>("MissionFolder/CollectionObjectives");
        ioTemp = GetAtPath<InteractObject>("MissionFolder/InteractObjectives");
        
        //Debug.Log("healingTemP Lenght: " + weaponTemp.Length);

        quirkArray = GetAtPath<QuirkObject>("QuirkFolder");
    }

    public static T[] GetAtPath<T>(string path) { //Hittar assets i deras folders
        //ArrayList al = new ArrayList();
        //string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
        //foreach (string fileName in fileEntries) {

        //    int index = fileName.LastIndexOf("Assets");
        //    string localPath = "";

        //    if (index > 0) {
        //        localPath += fileName.Substring(index);
        //    }
        //    //Debug.Log("localPath: " + localPath);

        //    //Object t = AssetDatabase.LoadAssetAtPath(fileName.Substring(index), typeof(T));
        //    //Debug.Log("fíleName: " + fileName.Substring(index));
        //    //object t = Resources.LoadAll(path, typeof(T));
        //    //Object t = Resources.Load(fileName.Substring(index), typeof(T));
        //    //Object t = AssetBundle.LoadAsset(fileName.Substring(index), typeof(T));
        //    //Debug.Log("ssss");

        //    //if (t != null) {
        //    //    al.Add(t);
        //    //}
        //}

        //T[] result = new T[al.Count];
        object[] t = Resources.LoadAll(path, typeof(T));
        //T[] reult = (T)t;
        T[] result = new T[t.Length];
        for (int i = 0; i < t.Length; i++) {
            result[i] = (T)t[i];
        }
        //for (int i = 0; i < al.Count; i++) {
        //    result[i] = (T)al[i];
        //}
        return result;
    }
}
