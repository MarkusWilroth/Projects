using System.Collections.Generic;
using UnityEngine;

public class HospitalScript : MonoBehaviour {

    public GameObject UIHospital, UISlots;
    private GameObject UIHolder, forHospitalParent, forHealingParent, CoinBox;
    private GameObject[] hubCharacters, hospitalCharacters;
    public List<GameObject> SlotList;
    private Stats statsScript;
    private CharacterScript hubCharacterScript, hospitalCharacterScript;
    public int healingSlots;
    private bool showCharacter;

    void Start() {
        SlotList = new List<GameObject>();
        UICharacters();
        HealingSlots();
    }

    private void UICharacters() {
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        forHospitalParent = GameObject.Find("forHospital");
        forHealingParent = GameObject.Find("forHealingSlots");
        CoinBox = GameObject.FindGameObjectWithTag("CoinBox");

        CoinBox.SetActive(false);
        showCharacter = false;


        foreach (GameObject character in hubCharacters) {  //Går igenom alla karaktärer
            statsScript = character.GetComponent<Stats>();

            if (statsScript.hp <= statsScript.maxHp) { //För att bara få de skadade karaktärena
                showCharacter = true;
            } else {
                foreach (QuirkObject quirk in statsScript.quirkList) {  //Går in i alla quirks för att se om karaktären är skadad
                    if (quirk.quirkType == QuirkScript.QuirkType.woundQuirk) {
                        showCharacter = true;
                        break;
                    }
                }
            }
            if (showCharacter) {    //Om karaktären har mindre hp än max eller har en woundQuirk kommer den att ritas us i hospital
                hubCharacterScript = character.GetComponent<CharacterScript>();

                UIHolder = Instantiate(UIHospital);
                
                UIHolder.GetComponent<CharacterScript>().LoadPlayer(hubCharacterScript);
                UIHolder.GetComponent<Stats>().LoadPlayer(statsScript);
                UIHolder.GetComponent<UIHospitalBoi>().Hospital = gameObject;

                if (UIHolder.GetComponent<CharacterScript>().inHospital) {
                    UIHolder.transform.SetParent(forHealingParent.transform, false);
                } else {
                    UIHolder.transform.SetParent(forHospitalParent.transform, false);
                }
            }

            
        }

    }

    private void HealingSlots() {
        healingSlots = WorldScript.world.hospitalLevel + 1;
    }

    public void HealCharacter(GameObject character) {
        character.transform.SetParent(forHealingParent.transform, false);
    }

    public void RemoveCharacter(GameObject character) {
        character.transform.SetParent(forHospitalParent.transform, false);
    }

    public List<GameObject> GetSlot() { //Vet inte varför detta behövs...
        return SlotList;
    }

    public void Exit() {
        hospitalCharacters = GameObject.FindGameObjectsWithTag("UIHospitalCharacter"); //Tar fram alla som är i hospital
        foreach (GameObject hospitalCharacter in hospitalCharacters) { //Kollar alla i hospital
            hospitalCharacterScript = hospitalCharacter.GetComponent<CharacterScript>();

            foreach (GameObject hubCharacter in hubCharacters) { //Kollar alla karaktärer i hubben
                hubCharacterScript = hubCharacter.GetComponent<CharacterScript>();
                
                if (hospitalCharacterScript.id == hubCharacterScript.id) { //Samma boi
                    hubCharacterScript.LoadPlayer(hospitalCharacterScript);
                    break;
                }

            }
        }
        CoinBox.SetActive(true);
        Destroy(gameObject);
    }
}
