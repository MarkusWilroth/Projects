using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {
    public List<CharacterData> characterDataList;
    public List<BarrackData> barrackDataList;
    public List<QuestData> questDataList;
    public WorldData worldData;

    public SaveData(List<CharacterData> characterDataList, List<BarrackData> barrackDataList, List<QuestData> questDataList, WorldData worldData) {
        this.characterDataList = characterDataList;
        this.barrackDataList = barrackDataList;
        this.questDataList = questDataList;
        this.worldData = worldData;
    }
}
