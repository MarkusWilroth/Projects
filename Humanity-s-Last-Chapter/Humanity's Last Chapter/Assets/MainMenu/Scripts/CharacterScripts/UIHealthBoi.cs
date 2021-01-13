using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIHealthBoi : MonoBehaviour {

    public TextMeshProUGUI txtName, txtHp;
    public Slider healthSlider;
    private GameObject[] characters;
    private GameObject Hospital, CharacterParent;
    private Transform SlotParent;
    private List<GameObject> slotList;
    private string id;
    private bool inSlot;

    private void Start() {
        
        CharacterParent = GameObject.Find("forHospital");
        
    }

    public void InHosital() {
        if(!inSlot) {
            MoveToSlot();
        } else {
            MoveToList();
        }
    }

    public void MoveToSlot() {
        characters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject character in characters) {
            if (character.GetComponent<CharacterScript>().id == id) {
                Hospital = GameObject.Find("HospitalWindow");
                slotList = Hospital.GetComponent<HospitalScript>().GetSlot();

                foreach (GameObject slot in slotList) {
                    SlotParent = slot.transform.GetChild(0);
                    if (!SlotParent.GetComponent<HealingSlotScript>().isUsed) {
                        transform.SetParent(SlotParent, false);
                        SlotParent.GetComponent<HealingSlotScript>().isUsed = true;
                        inSlot = true;
                        character.GetComponent<CharacterScript>().inHospital = true;
                        break;
                    }
                }

                //Ändra UIBoi till att inte kunna gå på mission

                break;
            }
        }
    }

    public void MoveToList() {
        GetComponentInParent<HealingSlotScript>().isUsed = false;
        transform.SetParent(CharacterParent.transform, false);
        foreach (GameObject character in characters) {
            if (character.GetComponent<CharacterScript>().id == id) {
                character.GetComponent<CharacterScript>().inHospital = true;
                break;
            }
        }
    }

    public void GetData(string name, string id, int hp, int maxHp) {
        this.id = id;
        txtName.text = name;
        txtHp.text = hp + "/" + maxHp;
        
    }
}
