using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvent : MonoBehaviour { //Flyttar NPC character till dit man vill

    private string id;
    private Transform destination;
    private GameObject[] gameObjects;
    private GameObject prefab;
    private GameObject holder;
    private bool isMoving;
    private CharacterScript.Faction faction;
    private string tag;

    public void GetEvent(MoveObject move) {
        id = move.id;
        destination = move.destination;
        prefab = move.prefab;

        gameObjects = GameObject.FindGameObjectsWithTag(prefab.tag);
        foreach (GameObject obj in gameObjects) {
            if (obj.name == id) {
                holder = obj;
            }
        }
    }
}
