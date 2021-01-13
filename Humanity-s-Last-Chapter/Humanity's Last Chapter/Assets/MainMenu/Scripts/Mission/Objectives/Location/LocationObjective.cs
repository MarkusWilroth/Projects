using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem {
    public class LocationObjective : MonoBehaviour {
        public string title = "t.ex. Enter the Kitchen";
        public string description = "t.ex. A distress call from the Dinner Room calls for an investigation";
        private string id = "t.ex. lo0";
        private GameObject Location;
        private GameObject holder;
        private GameObject[] clearOutTarget; //I fall man vill ha en quest som handlar om att rensa rum av fiender
        private GameObject[] characters;
        private bool isComlete;
        private bool isBonus;
        private LocationScript loScript;
        private WaveEvent waveEvent;
        private List<Transform> spawnList;
        private Object[] startEvents;
        private Object[] endEvents;

        LocationObject data;
        public List<Transform> SpawnPos() {

            return spawnList;

        }
        public void GetData(LocationObject data, WaveEvent waveEvent) {
            spawnList = new List<Transform>();
            this.data = data;
            this.waveEvent = waveEvent;
            title = data.titel;
            description = data.description;
            id = data.id;
            Location = data.Location;
            clearOutTarget = data.clearOutTarget;
            isComlete = false;
            isBonus = data.isBonus;

            

            characters = GameObject.FindGameObjectsWithTag("Character");

            //GameObject[] Locations = GameObject.FindGameObjectsWithTag("Location");
            //foreach (GameObject location in Locations)
            //{
            //    if (location.name == data.Location.name)
            //    {
            //        Location = location;
            //        break;
            //    }
            //}
            holder = Instantiate(Location);
            holder.transform.position = data.locationPos.position;
            holder.name = "LocationObjective(" + id + ")";
            loScript = holder.GetComponent<LocationScript>();

            //if (data.spawnLo)
            //{
            //    Location.transform.position = data.locationPos.position;
            //}
            //else
            //{

            //}
            if (startEvents != null) {
                StartEvent(startEvents);
            }
            spawnList.Add(holder.transform);
        }

        public bool CheckProgress() {
            if (characters == null || characters.Length == 0) {
                characters = GameObject.FindGameObjectsWithTag("Character");
            }

            foreach (GameObject character in characters) {
                //isComlete = loScript.OnTriggerEnter2D(character.GetComponent<BoxCollider2D>());
                isComlete = holder.GetComponent<LocationScript>().isInRoom;
                //Debug.Log("LocationPos: " + loScript.GetComponent<BoxCollider2D>().bounds);
                //if (loScript.GetComponent<BoxCollider2D>().bounds.Contains(character.transform.position)) {
                //    Debug.Log("It works now!");
                //    if (clearOutTarget.Length == 0) {
                //        return true;
                //    }
                //}
                if (isComlete) {
                    holder.GetComponent<LocationScript>().isInRoom = false;
                    if (endEvents != null) {
                        StartEvent(endEvents);
                    }
                    Destroy(holder);
                    break;
                }
            }
            
            return isComlete;
        }
        private void StartEvent(Object[] eventObj) {
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

