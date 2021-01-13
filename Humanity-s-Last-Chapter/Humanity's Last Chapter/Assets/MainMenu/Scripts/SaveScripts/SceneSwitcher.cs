using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {    //ansvarig för att byta scen!

    public void GoToHub() {
        WorldScript.world.date++;
        WorldScript.world.activeQuest = null;
        WorldScript.world.SaveMission(true);
        SceneManager.LoadScene("Hub");
    }
    public void GoToMission() {
        if(WorldScript.world.activeQuest != null) {
            WorldScript.world.RefreshHub();
            WorldScript.world.SaveHub(true);
            SceneManager.LoadScene("MissionMap" + WorldScript.world.activeQuest.missionMap);
        } else {
            Debug.Log("No active mission");
        }
        
    }
}
