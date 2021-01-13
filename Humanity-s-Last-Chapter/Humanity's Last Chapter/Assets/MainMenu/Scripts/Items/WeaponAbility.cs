using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbility : MonoBehaviour {

    public Sprite defultSprite;
    public KeyCode BoundKey;
    private bool isReady;
    private GameObject Character;
    private WeaponObject weapon;

    private void Start() {
        isReady = false;
    }

    public void GetItem(GameObject Character, GameObject pivotCharacter) {
        if(Character != null) {
            this.Character = Character;
            weapon = Character.GetComponent<Abilities>().weapon;
            GetComponent<Image>().sprite = weapon.icon;
        }else {
            GetComponent<Image>().sprite = defultSprite;
        }
        
    }

    private void Update() {
        if (Character != null) {
            if (!isReady) {
                isReady = Character.GetComponent<Abilities>().weaponReady;
                GetComponent<Image>().color = Color.gray;
            } else {
                GetComponent<Image>().color = Color.white;
                if (Input.GetKeyDown(BoundKey)) {
                    Activate();
                }
            }

        }
    }

    private void Activate() { //Utför abilitin

    }
}
