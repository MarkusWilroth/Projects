using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData {

    //public List<CharacterScript> characterList;
    public int gold, rs, storageSize, shopSize, shopLevel, partySize;
    public string saveId, saveName;
    public string[] storageArr, shopArr;
    public bool isActive, spawnedBoiz; //Den sparningen som startar om man klickar continue

    public WorldData(WorldScript world) {
        gold = world.gold;
        rs = world.rs;
        saveId = world.saveId;
        storageSize = world.storageSize;
        shopSize = world.stockSize;
        shopLevel = world.shopLevel;
        storageArr = world.storageArr;
        shopArr = world.shopArr;
        isActive = world.isActive;
        saveName = world.saveName;
        partySize = world.partySize;
        spawnedBoiz = world.spawnedBoiz;
    }
}
