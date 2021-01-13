using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEvent : MonoBehaviour { //Markus - Spawnar massor med fiender med en timer

    private Transform[] spawnPosArr;    //Där sakerna ska spawna
    private GameObject prefab;          //Vad som ska spawna
    private GameObject holder;
    private bool isActive;              //Håller koll på om den ska spawna saker
    private bool isRandomSpawn;
    private bool isTimerBased;          //Om det ska spawna saker med timmer eller om det håller på tills x fiender är spawnade
    private float rateStamp;            //Håller koll på när nästa fiende ska spawna på spawnpointen
    private float spawnRate;            //Hur snabbt de spawnar

    private float waveDuration;         //Om isTimerBased hur länge vågen håller på
    private float waveStamp;            //Håller koll på tiden som gått sedan start
    private int enemiesAmount;         //Hur många fiender som spawnar om !isTimerBased
    private int counter;                //Räknar fiender som spawnat

    public void GetEvent(WaveObject wave) { //Får värderna
        spawnPosArr = wave.spawnPosArr;
        prefab = wave.prefab;
        isTimerBased = wave.isTimerBased;
        spawnRate = wave.spawnRate;
        waveDuration = wave.waveDuration;
        enemiesAmount = wave.enemiesAmmount;

        rateStamp = 0;
        waveStamp = 0;

        isRandomSpawn = wave.isRandomSpawn;
        isActive = true;
    }

    private void Update() {
        if(isActive) {
            rateStamp += Time.deltaTime;
            if (rateStamp >= spawnRate) {
                rateStamp = 0;
                if (isTimerBased) {
                    TimerSpawner();
                } else {
                    CounterSpawner();
                }
            }
            
        }
    }

    private void TimerSpawner() {
        waveStamp += Time.deltaTime;
        if (waveStamp >= waveDuration) {
            isActive = false;
        } else {
            foreach (Transform spawnPos in spawnPosArr) {
                holder = Instantiate(prefab);
                holder.transform.position = spawnPos.position;
            }
        }

    }

    private void CounterSpawner() {
        if (isRandomSpawn)
        {
            for (int i = 0; i < enemiesAmount; i++)
            {
                holder = Instantiate(prefab);
                holder.transform.position = spawnPosArr[Random.Range(0, spawnPosArr.Length)].position;
            }
            isActive = false;
        }
        else
        {
            foreach (Transform spawnPos in spawnPosArr)
            {
                holder = Instantiate(prefab);
                holder.transform.position = spawnPos.position;
                counter++;
                if (counter >= enemiesAmount)
                {
                    isActive = false;
                }
            }
        }       
    }
}
