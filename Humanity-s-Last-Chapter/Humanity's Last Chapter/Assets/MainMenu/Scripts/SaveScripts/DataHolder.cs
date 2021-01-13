using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataHolder {

    public static DataHolder dataHolder;

    public int activeSave, saveCounter;

    private string path, saveName;
    public string saveId;

    private List<CharacterScript> characterList, chaBarrackList;
    private List<Stats> statsList, staBarrackList;
    private List<ScriptableQuest> avalibleQuests;
    private List<ScriptableQuest> completedQuests;
    private List<ScriptableQuest> failedQuests;
    public GameObject btnContinue, btnLoad;

    public void NewHolder() {
        characterList = new List<CharacterScript>();
        chaBarrackList = new List<CharacterScript>();
        statsList = new List<Stats>();
        staBarrackList = new List<Stats>();
        avalibleQuests = new List<ScriptableQuest>();
        completedQuests = new List<ScriptableQuest>();
        failedQuests = new List<ScriptableQuest>();

        if (Assets.assets == null) {
            Assets.assets = new Assets();
            Assets.assets.GetAssets();
        }

        btnContinue = GameObject.FindGameObjectWithTag("BtnContinue");
        btnLoad = GameObject.FindGameObjectWithTag("BtnLoad");
        path = Application.persistentDataPath + "/Saves/";
        saveCounter = Directory.GetFiles(path).Length;

        if (!File.Exists(path + "AutoSave.save")) {
            btnContinue.SetActive(false);
        } else {
            saveCounter++;
        }
        
        if (saveCounter <= 0) {
            btnLoad.SetActive(false);
        }
    } //Förberreder en ny holder

    public void BtnContinue() {
        if (File.Exists(path + "AutoSave.save")) {
            WorldData("AutoSave", true);
            StartGame();
        }
    }

    public void CheckButtonPossible() { //Kollar om någon knapp ska inaktiveras
        saveCounter = Directory.GetFiles(path).Length;
        if (btnContinue == null) {
            btnContinue = GameObject.FindGameObjectWithTag("BtnContinue");
            btnLoad = GameObject.FindGameObjectWithTag("BtnLoad");
        }
        
        if (!File.Exists(path + "AutoSave.save")) { //Som det fungerar nu laddar man från autoSave när man klickar continue, om den inte finns... kan man inte ladda
            btnContinue.SetActive(false); //Sätter knappen så att den är inaktiv
        } else {
            btnContinue.SetActive(true);
        }
        if (saveCounter <= 0) { //Om det inte finns några saves finns det ingen mening att ladda
            btnLoad.SetActive(false);
        } else {
            btnLoad.SetActive(true);
        }
    }

    public void BtnNewGame() {
        bool isFree = true;
        int testSave = 0;
        while(isFree) {
            if(!File.Exists(path + "save"+testSave+".save")) {
                WorldScript.world = new WorldScript();
                WorldScript.world.Reset();
                WorldScript.world.saveId = "save" + testSave;
                isFree = false;
            }
            else {
                testSave++;
            }
        }
        StartGame();
    }

    public void BtnPlay() {
        if (File.Exists(path + saveId + ".save")) {
            WorldData(saveId, false);
            StartGame();
        } else if (File.Exists(path + "AutoSave.save")) {
            WorldData("AutoSave", true);
            StartGame();
        } else { 
            Debug.Log("Can't find Save" + saveId);
        }
        
    }

    public void BtnDelete() {
        if(File.Exists(path + saveId + ".save")) {
            File.Delete(path + saveId + ".save");
            Debug.Log("Deleted: " + saveId);
        } else {
            Debug.Log("Can't find Save: " + saveId);
        }

    }

    private void WorldData(string saveId, bool isAuto) {
        chaBarrackList.Clear();
        staBarrackList.Clear();
        SaveData data = SaveSystem.LoadWorld(saveId, isAuto);

        WorldScript.world = new WorldScript();
        WorldScript.world.gold = data.worldData.gold;
        WorldScript.world.rs = data.worldData.rs;
        WorldScript.world.saveId = data.worldData.saveId;
        WorldScript.world.storageSize = data.worldData.storageSize;
        WorldScript.world.stockSize = data.worldData.shopSize;
        WorldScript.world.shopLevel = data.worldData.shopLevel;
        WorldScript.world.storageArr = data.worldData.storageArr;
        WorldScript.world.shopArr = data.worldData.shopArr;
        WorldScript.world.isActive = data.worldData.isActive;
        WorldScript.world.saveName = data.worldData.saveName;
        WorldScript.world.partySize = data.worldData.partySize;
        WorldScript.world.spawnedBoiz = data.worldData.spawnedBoiz;

        foreach (BarrackData barrackData in data.barrackDataList) {
            CharacterScript character = new CharacterScript();
            character.id = barrackData.id;
            character.clothId = barrackData.clothId;
            character.headId = barrackData.headId;
            character.strName = barrackData.strName;
            character.rangedId = barrackData.wpId;
            character.combatId = barrackData.combatId;
            character.healingId = barrackData.healingId;
            character.inventory = barrackData.itemID;
            chaBarrackList.Add(character);

            Stats stats = new Stats();
            stats.hp = barrackData.hp;
            stats.maxHp = barrackData.maxHp;
            stats.quirkIDList = barrackData.quirkID;
            stats.shit = barrackData.shit;
            stats.str = barrackData.str;
            stats.def = barrackData.def;
            stats.Int = barrackData.Int;
            stats.dex = barrackData.dex;
            stats.cha = barrackData.cha;
            stats.ldr = barrackData.ldr;
            stats.snt = barrackData.snt;
            stats.exp = barrackData.exp;
            staBarrackList.Add(stats);
        }

        foreach (CharacterData characterData in data.characterDataList) {
            CharacterScript character = new CharacterScript();
            character.id = characterData.id;
            character.clothId = characterData.clothId;
            character.headId = characterData.headId;
            character.strName = characterData.strName;
            character.rangedId = characterData.wpId;
            character.combatId = characterData.combatId;
            character.healingId = characterData.healingId;
            character.inventory = characterData.itemID;
            characterList.Add(character);

            Stats stats = new Stats();
            stats.hp = characterData.hp;
            stats.maxHp = characterData.maxHp;
            stats.quirkIDList = characterData.quirkID;
            stats.shit = characterData.shit;
            stats.str = characterData.str;
            stats.def = characterData.def;
            stats.Int = characterData.Int;
            stats.dex = characterData.dex;
            stats.cha = characterData.cha;
            stats.ldr = characterData.ldr;
            stats.snt = characterData.snt;
            stats.exp = characterData.exp;
            statsList.Add(stats);
        }

        foreach (QuestData questData in data.questDataList) {
            ScriptableQuest quest = FindQuest(questData.id);
            if (questData.isComplet) {
                completedQuests.Add(quest);
            } else if (questData.isFailed) {
                failedQuests.Add(quest);
            } else {
                avalibleQuests.Add(quest);
            }
        }
        //Sätter så att att alla listor i worldScript får som de ska ha det
        WorldScript.world.avalibleQuests = avalibleQuests;
        WorldScript.world.completedQuests = completedQuests;
        WorldScript.world.characterList = characterList;
        WorldScript.world.statsList = statsList;
        WorldScript.world.charBarrackPepList = chaBarrackList;
        WorldScript.world.staBarrackPepList = staBarrackList;
    } //Förberreder WorldScript med data

    private ScriptableQuest FindQuest(string questId) {
        foreach (ScriptableQuest quest in Assets.assets.questTemp) {
            if (quest.name == questId) {
                return quest;
            }
        }
        Debug.Log("Error! Quest: " + questId + " not found!");
        return null;
    }

    private void StartGame() {
        SceneManager.LoadScene("Hub"); //Kanske ska worldScript ha koll på active scene?
    }
}
