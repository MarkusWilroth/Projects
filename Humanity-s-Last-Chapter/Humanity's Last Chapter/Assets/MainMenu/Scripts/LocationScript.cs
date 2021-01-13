using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationScript : MonoBehaviour {

    public string location;
    private BoxCollider2D room;
    public Text txtLocation; //Ifall man vill att det ska komma upp något i höra hörnet att man har gått in i ett rum
    public bool isInRoom;
    

    void Start() {
        //room = GetComponent<BoxCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        //txtLocation.text = location;
        //return true;
        isInRoom = true;
        //Debug.Log("Location name: " + gameObject.name);
        //Enter room
        //If boss room, boos music?
    }

    public void OnTriggerExit2D(Collider2D other) {
        isInRoom = false;

        //Stop boss music?
    }
}
