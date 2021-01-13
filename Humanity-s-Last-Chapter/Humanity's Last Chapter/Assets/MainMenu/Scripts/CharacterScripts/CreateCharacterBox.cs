using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacterBox : MonoBehaviour {

    public Text txtDescName, txtDescDesc;
    public GameObject CharacterBox, InventoryParent;
    public GameObject BoxDad, ItemSlot;
    private GameObject Holder;
    private GameObject[] CharacterArr, itemSlots, hubCharacters, storageCharacters;
    private CharacterScript hubCharacterScript, storageCharacterScript;
    private Stats hubStats, storageStats;

    private void Start() {

        //Skapar Inventory
        for (int i = 0; i < WorldScript.world.storageSize; i++) {
            Holder = Instantiate(ItemSlot);
            Holder.transform.SetParent(InventoryParent.transform, false);
            Holder.GetComponent<ItemSlotScript>().slotNr = i;
            Holder.transform.tag = "ItemSlot";
        }
        
        //Fyller inventory med items
        itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        GetComponent<CreateItems>().FillSlots(WorldScript.world.storageArr, itemSlots, gameObject);
        
        //Skapar Character boxes
        CharacterArr = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject Character in CharacterArr)
        {
            Holder = Instantiate(CharacterBox);
            Holder.transform.SetParent(BoxDad.transform, false);
            Holder.GetComponent<CharacterBox>().GetData(Character, gameObject, txtDescName, txtDescDesc);
        }
    }
    public void Return() {
        storageCharacters = GameObject.FindGameObjectsWithTag("UIHospitalCharacter"); //Tar fram alla som är i hospital
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");

        foreach (GameObject storageCharacter in storageCharacters) { //Kollar alla i hospital
            
            storageCharacter.GetComponent<CharacterBox>().FinalInfo();
            storageCharacterScript = storageCharacter.GetComponentInChildren<CharacterScript>();
            storageStats = storageCharacter.GetComponentInChildren<Stats>();

            foreach (GameObject hubCharacter in hubCharacters) { //Kollar alla karaktärer i hubben
                hubCharacterScript = hubCharacter.GetComponent<CharacterScript>();

                if (storageCharacterScript.id == hubCharacterScript.id) { //Samma boi
                    hubStats = hubCharacter.GetComponent<Stats>();
                    hubStats.LoadPlayer(storageStats);
                    hubCharacterScript.LoadPlayer(storageCharacterScript);
                    break;
                }

            }
        }
        Destroy(gameObject);
    }
}


