using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour { //Markus - Håller koll på vilka abilites en karaktär har och när de kan användas

    private GameObject ActiveCharacter;
    private CharacterScript characterScript;
    public GameObject pivotCharacter;

    //Så att man endast går in i foreach loopen en gång per karaktär och inte varje gång man byter karaktär
    public WeaponObject weapon;
    public HealingItemObject healingItem;
    public CombatItemObject combatItem;

    private string weaponId, healingId, combatId;
    public bool weaponReady, healingReady, combatReady;
    private int weaponTimer, healingTimer, combatTimer, partyMember;
    private float weaponStamp, healingStamp, combatStamp;
    private KeyCode key;

    private void Start() {
        ActiveCharacter = GameObject.FindGameObjectWithTag("ActiveCharacter");
        characterScript = pivotCharacter.GetComponent<CharacterScript>();
        partyMember = characterScript.partyMember;

        GetEquipment();

        if (weapon != null) {
            weaponTimer = weapon.coolDownTimer;
        }
        if (healingItem != null) {
            healingTimer = healingItem.coolDownTimer;
        }
        if (combatItem != null) {
            combatTimer = combatItem.coolDownTimer;
        }
    }

    public void BtnSelect() {
        ActiveCharacter.GetComponent<ActiveCharacter>().SwitchCharacter(gameObject, pivotCharacter);
    }

    private void Update() {
        if(Input.GetKeyDown(partyMember.ToString())) {
            ActiveCharacter.GetComponent<ActiveCharacter>().SwitchCharacter(gameObject, pivotCharacter);
        }
        Timer();
    }

    private void Timer() { //Timer som håller koll på om abilityn är redo
        if (weapon != null) {
            if (!weaponReady) {
                weaponStamp += Time.deltaTime;
                if (weaponStamp >= weaponTimer) {
                    weaponStamp = 0;
                    weaponReady = true;
                }
            }
        }

        if (healingItem != null) {
            if (!healingReady) {
                healingStamp += Time.deltaTime;
                if (healingStamp >= healingTimer) {
                    healingStamp = 0;
                    healingReady = true;
                }
            }
        }

        if (combatItem != null) {
            if (!combatReady) {
                combatStamp += Time.deltaTime;
                if (combatStamp >= combatTimer) {
                    combatStamp = 0;
                    combatReady = true;
                }
            }
        }
    }

    private void GetEquipment() {
        weaponId = characterScript.rangedId;
        if (weaponId != "" || weaponId != null) {
            foreach (WeaponObject weapon in Assets.assets.weaponTemp) {
                if (weapon.name == weaponId) {
                    this.weapon = weapon;
                }
            }
        } else {
            
            weapon = null;
        }

        healingId = characterScript.healingId;
        if (healingId != "" || healingId != null) {
            foreach (HealingItemObject healingItem in Assets.assets.healingTemp) {
                if (healingItem.name == healingId) {
                    this.healingItem = healingItem;
                }
            }
        } else {
            
            healingItem = null;
        }

        combatId = characterScript.combatId;
        if (combatId != "" || combatId != null) {
            foreach (CombatItemObject combatItem in Assets.assets.combatTemp) {
                if (combatItem.name == combatId) {
                    this.combatItem = combatItem;
                }
            }
        } else {
            combatItem = null;
        }
    }
}
