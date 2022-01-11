using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealingAbility : MonoBehaviour {

    public KeyCode BoundKey;
    public Sprite defultSprite;
    private HealingItemObject healingItem;
    private GameObject Character, pivotCharacter;
    private bool isReady;

    private void Start() {
        healingItem = null;
    }

    public void GetItem(GameObject Character, GameObject pivotCharacter) {
        if (Character != null) {
            this.Character = Character;
            this.pivotCharacter = pivotCharacter;
            healingItem = Character.GetComponent<Abilities>().healingItem;
            if (healingItem != null) {
                GetComponent<Image>().sprite = healingItem.icon;
            } else {
                GetComponent<Image>().sprite = defultSprite;
            }
        } else {
            GetComponent<Image>().sprite = defultSprite;
        }

    }

    private void Update() {
        if(Character != null) {
            isReady = Character.GetComponent<Abilities>().healingReady;
            if (!isReady) {
                GetComponent<Image>().color = Color.gray;
            } else {
                GetComponent<Image>().color = Color.white;
                if (Input.GetKeyDown(BoundKey)) {
                    Activate();
                }
            }
            
        }
    }

    public void Activate() {
        if(isReady) {
            Character.GetComponent<Abilities>().healingReady = false; //Ska flyttas till mer passande plats
            pivotCharacter.GetComponent<Stats>().HealCharacter(healingItem.healPower);
        }
    }
}
