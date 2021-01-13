using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem {
    public class InteractionObjective : MonoBehaviour {
        public string title = "t.ex. Talk with this person";
        private string verb = "t.ex. talk";
        private string id = "qg0";
        public string description = "It must have been that guy that made the distress call!";
        private bool isComplete, isBonus, isSpawned;
        private Transform spawnPos;
        private GameObject interactObjective; //Object to interact with
        private GameObject holder;
        private GameObject[] gameObjects;
        private WaveEvent waveEvent;
        private List<Transform> spawnList;
        private InteractObject data;
        private Object[] startEvents;
        private Object[] endEvents;

        public List<Transform> SpawnPos() {

            return spawnList;
        }
        public void GetData(InteractObject ioQuest, WaveEvent waveEvent) {
            this.waveEvent = waveEvent;
            spawnList = new List<Transform>();
            description = ioQuest.description;
            startEvents = ioQuest.startEvents;
            endEvents = ioQuest.endEvents;
            data = ioQuest;
            title = data.title;
            id = ioQuest.id;
            isSpawned = ioQuest.isSpawned;
            interactObjective = ioQuest.interactObjective;
            spawnPos = ioQuest.spawnPos;

            spawnList.Add(spawnPos);


            if (!isSpawned) {   //Om den inte ska spawnas (Den redan finns) ska den gamla raderas
                gameObjects = GameObject.FindGameObjectsWithTag(data.interactObjective.tag);
                foreach (GameObject objects in gameObjects) {
                    if (objects.GetComponent<InteractiveScript>().id == id) {
                        Destroy(objects);
                    }
                }
            } 
            holder = Instantiate(interactObjective);    //Skapar ett nytt interactive object
            holder.transform.position = spawnPos.position;
            holder.GetComponent<InteractiveScript>().id = id;
            holder.name = "InteractiveObject(" + id + ")";
            foreach (Transform child in holder.transform) {
                child.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            holder.GetComponent<InteractiveScript>().SetActive();
            //interactObjective = GameObject.FindGameObjectWithTag(data.interactObjective.tag);

            if (startEvents != null) {
                StartEvent(startEvents);
            }
        }

        public bool CheckProgress() {   //Blir kollad ifall objectivet är intergerat med
            isComplete = holder.GetComponent<InteractiveScript>().isInteracted;
            if (isComplete) {
                foreach (Transform child in holder.transform) {
                    child.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                if (endEvents != null) {
                    StartEvent(endEvents);
                }
            }
            holder.GetComponent<InteractiveScript>().isInteracted = false;
            return isComplete;
        }

        private void StartEvent(Object[] eventObj) {    //Kollar om det finns något event som ska starta när denna del av questen börjar
            foreach (Object obj in eventObj) {
                switch (obj.name[0]) {
                    case 'w':
                        waveEvent.GetEvent(obj as WaveObject);
                        break;
                }
            }
        }

    }
}

