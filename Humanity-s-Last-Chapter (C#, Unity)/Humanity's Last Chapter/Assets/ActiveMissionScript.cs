using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMissionScript : MonoBehaviour {  //Markus - Används för att spelaren ska kunna se vilken quest som är aktiv, byta bort mote glow?

    public GameObject MissionBox;   //Prefab MissionBox (Visar namn och har info om questen)
    private GameObject holder;      //Används för att ge värden till skapad missionBox
    private ScriptableQuest activeQuest, newQuest;  //Används för att se om questen som kollas är samma som innan

    private void Start() {
        if (WorldScript.world.activeQuest != null) {
            activeQuest = WorldScript.world.activeQuest;
            holder = Instantiate(MissionBox);
            holder.GetComponent<MissionBoxScript>().GetQuest(activeQuest, gameObject);
            holder.transform.SetParent(gameObject.transform, false);
        }
    }

    public void GetActiveQuest() {
        newQuest = WorldScript.world.activeQuest;

        if (activeQuest != null) {
            if (!newQuest == activeQuest) {
                holder = Instantiate(MissionBox);
                holder.GetComponent<MissionBoxScript>().GetQuest(newQuest, gameObject);
                holder.transform.SetParent(gameObject.transform, false);
                activeQuest = newQuest;
            }
        } else {
            holder = Instantiate(MissionBox);
            holder.GetComponent<MissionBoxScript>().GetQuest(newQuest, gameObject);
            holder.transform.SetParent(gameObject.transform, false);
            activeQuest = newQuest;
        }


    }
}
