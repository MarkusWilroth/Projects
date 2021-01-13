using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {

    public string characterName, id;
    public bool isInteracted;

    private void Start() {
        isInteracted = false;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        isInteracted = false;
    }

    public void OnTriggerStay2D(Collider2D collision) {
        //Något som säger att man ska klicka på E för att prata kanske...
        
        if (Input.GetKeyDown(KeyCode.E)) {
            isInteracted = true;
        }
    }

    public void Dialog() {
        //Ha någon sorts dialog?
    }
}
