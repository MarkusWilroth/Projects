using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveScript : MonoBehaviour {

    public string characterName, id;
    public bool isInteracted;
    private GameObject[] characters;
    private float distance;
    private float range;
    private int charactersInRange;
    private int inventorySize;
    private bool isActive;

    private void Start() {
        isInteracted = false;
        range = 10f;
        characters = GameObject.FindGameObjectsWithTag("Character");
    }

    public void SetActive() {
        isActive = true;
    }

    //public void OnTriggerExit2D(Collider2D collision) {
    //    isInteracted = false;
    //}

    //public void OnTriggerStay2D(Collider2D collision) {
    //    //Något som säger att man ska klicka på E för att prata kanske...

    //    if (Input.GetKeyDown(KeyCode.E)) {
    //        isInteracted = true;
    //    }
    //}

    private void OnMouseDown() {        
        CheckRange();
    }

    private void CheckRange() {
        charactersInRange = 0;
        if (isActive) {
            foreach (GameObject character in characters) {
                if (character != null) {
                    distance = Vector3.Distance(character.transform.position, transform.position);
                    if (distance <= range) {
                        charactersInRange = 1;
                    }
                }
            }
        }
        if (charactersInRange > 0) {
            isActive = false;
            isInteracted = true;
        } 
    }
}
