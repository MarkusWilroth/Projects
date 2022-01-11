using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : ScriptableObject {

    public string WaveInfo = "T.ex. fyller missionMap1, wave runt start etc..";
    public Transform[] spawnPosArr; //Där sakerna ska spawna
    public GameObject prefab; //Vad som ska spawna
    public bool isRandomSpawn;
    public bool isTimerBased; //Om det ska spawna saker med timmer eller om det håller på tills x fiender är spawnade
    public float spawnRate; //Hur snabbt de spawnar

    public float waveDuration; //Om isTimerBased hur länge vågen håller på
    public int enemiesAmmount; //Hur många fiender som spawnar om !isTimerBased - Ändra till hur många som ska spawna per spawnPoint?
}
