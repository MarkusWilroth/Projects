using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterBox : MonoBehaviour {

    private CharacterScript characterScript;
    private Text txtDescName, txtDescDesc;
    public Text txtName;
    public Sprite cloth;

    public GameObject[] itemSlots;
    public GameObject weaponSlot, clothSlot, headSlot, healingSlot, combatSlot;

    private GameObject ItemO;

    public GameObject Item, Portrait;
    private ItemSlotScript iss;
    private GameObject Character, DescParent;

    private string rangedId, clothId, headId, healingId, combatId;
    private string[] inventoryArr;
    private ItemInfo itemInfo;

    public void GetData(GameObject Character, GameObject DescParent, Text txtDescName, Text txtDescDesc) {
        this.Character = Character;
        this.DescParent = DescParent;
        //this.txtDescName = txtDescName;
        //this.txtDescDesc = txtDescDesc;
        characterScript = Character.GetComponent<CharacterScript>();
        //txtName.text = characterScript.strName;

        Portrait.GetComponent<CharacterScript>().LoadPlayer(characterScript);
        Portrait.GetComponent<Stats>().LoadPlayer(Character.GetComponent<Stats>());

        PrepareSlots(); //Sätter det karaktären har på sig till slots i characterBox
    }

    private void PrepareSlots() {

        if (characterScript.rangedId != "") { //Spawnar vapen i weaponSlot
            ItemO = Instantiate(Item);
            ItemO.transform.SetParent(weaponSlot.transform, false);

            rangedId = characterScript.rangedId;
            foreach (WeaponObject weapon in Assets.assets.weaponTemp) {
                if (weapon.name == rangedId) {
                    itemInfo = ItemO.GetComponent<ItemInfo>();
                    itemInfo.GetData(weapon.icon, weapon.weaponName, weapon.description, weapon.name, weapon.cost, txtDescName, txtDescDesc);
                    weaponSlot.GetComponent<ItemSlotScript>().GetItem(weapon.weaponName, weapon.description, DescParent);
                }
            }
        }

        if (characterScript.clothId != "") {
            ItemO = Instantiate(Item);
            ItemO.transform.SetParent(clothSlot.transform, false);
            ItemO.transform.localScale = new Vector3(50, 50, 1);

            clothId = characterScript.clothId;
            foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
                if (cloth.name == clothId) {
                    itemInfo = ItemO.GetComponent<ItemInfo>();
                    itemInfo.GetData(cloth.icon, cloth.itemName, cloth.description, cloth.name, cloth.cost, txtDescName, txtDescDesc);
                    clothSlot.GetComponent<ItemSlotScript>().GetItem(cloth.itemName, cloth.description, DescParent);
                }
            }
        }

        if (characterScript.headId != "") {
            ItemO = Instantiate(Item);
            ItemO.transform.SetParent(headSlot.transform, false);

            headId = characterScript.headId;
            foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
                if (cloth.name == headId) {
                    itemInfo = ItemO.GetComponent<ItemInfo>();
                    itemInfo.GetData(cloth.icon, cloth.itemName, cloth.description, cloth.name, cloth.cost, txtDescName, txtDescDesc);
                    headSlot.GetComponent<ItemSlotScript>().GetItem(cloth.itemName, cloth.description, DescParent);
                }
            }
        }

        if (characterScript.healingId != "") {
            ItemO = Instantiate(Item);
            ItemO.transform.SetParent(healingSlot.transform, false);
            ItemO.transform.localScale = new Vector3(50, 50, 1);

            healingId = characterScript.healingId;
            foreach (HealingItemObject healingItem in Assets.assets.healingTemp) {
                if (healingItem.name == healingId) {
                    itemInfo = ItemO.GetComponent<ItemInfo>();
                    itemInfo.GetData(healingItem.icon, healingItem.itemName, healingItem.description, healingItem.name, healingItem.cost, txtDescName, txtDescDesc);
                    healingSlot.GetComponent<ItemSlotScript>().GetItem(healingItem.itemName, healingItem.description, DescParent);
                }
            }
        }

        if (characterScript.combatId != "") {
            ItemO = Instantiate(Item);
            ItemO.transform.SetParent(combatSlot.transform, false);
            ItemO.transform.localScale = new Vector3(50, 50, 1);

            combatId = characterScript.combatId;
            foreach (CombatItemObject combatItem in Assets.assets.combatTemp) {
                if (combatItem.name == combatId) {
                    itemInfo = ItemO.GetComponent<ItemInfo>();
                    itemInfo.GetData(combatItem.icon, combatItem.itemName, combatItem.description, combatItem.name, combatItem.cost, txtDescName, txtDescDesc);
                    combatSlot.GetComponent<ItemSlotScript>().GetItem(combatItem.itemName, combatItem.description, DescParent);
                }
            }
        }
    }

    public void FinalInfo() {
        characterScript = Portrait.GetComponent<CharacterScript>();
        if (weaponSlot.GetComponentInChildren<ItemInfo>() != null) {
            rangedId = weaponSlot.GetComponentInChildren<ItemInfo>().id;
        } else {
            rangedId = "";
        }
        if (headSlot.GetComponentInChildren<ItemInfo>() != null) {
            headId = headSlot.GetComponentInChildren<ItemInfo>().id;
        } else {
            headId = "";
        }
        if (clothSlot.GetComponentInChildren<ItemInfo>() != null) {
            clothId = clothSlot.GetComponentInChildren<ItemInfo>().id;
        } else {
            clothId = "";
        }
        if (healingSlot.GetComponentInChildren<ItemInfo>() != null) {
            healingId = healingSlot.GetComponentInChildren<ItemInfo>().id;
        } else {
            healingId = "";
        }
        if (combatSlot.GetComponentInChildren<ItemInfo>() != null) {
            combatId = combatSlot.GetComponentInChildren<ItemInfo>().id;
        } else {
            combatId = "";
        }
        
        characterScript.rangedId = rangedId;
        
        if (headId != characterScript.headId) {
            if (headId == null || headId == "") {
                Portrait.GetComponent<PortraitScript>().RemoveHeadGear();
            } else {
                foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
                    if (cloth.name == headId) {
                        Portrait.GetComponent<PortraitScript>().ChangeCloth(cloth);
                    }
                }
            }
            
        }
        if (clothId != characterScript.clothId) {
            if (clothId == null || clothId == "") {
                Portrait.GetComponent<PortraitScript>().RemoveCloth();
            } else {
                foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
                    if (cloth.name == clothId) {
                        Portrait.GetComponent<PortraitScript>().ChangeCloth(cloth);
                    }
                }
            }
            
        }
        characterScript.healingId = healingId;
        characterScript.combatId = combatId;
        //Updaterar characterScript;
    }

    private void CreateItem(string id, GameObject Slot) {
        iss = Slot.GetComponent<ItemSlotScript>();
        switch (id[0]) {
            case 'h': //Healingitem
                foreach (HealingItemObject healing in Assets.assets.healingTemp) {
                    if (healing.name == id) {
                        ItemO = Instantiate(Item);
                        ItemO.transform.SetParent(Slot.transform, false);
                        itemInfo = ItemO.GetComponent<ItemInfo>();
                        itemInfo.GetData(healing.icon, healing.itemName, healing.description, healing.name, healing.cost, txtDescName, txtDescDesc);
                        iss.GetComponent<ItemSlotScript>().GetItem(healing.itemName, healing.description, DescParent);
                        break;
                    }
                }
                break;

            case 'c':
                foreach (CombatItemObject combat in Assets.assets.combatTemp) {
                    if (combat.name == id) {
                        ItemO = Instantiate(Item);
                        ItemO.transform.SetParent(Slot.transform, false);
                        itemInfo = ItemO.GetComponent<ItemInfo>();
                        itemInfo.GetData(combat.icon, combat.itemName, combat.description, combat.name, combat.cost, txtDescName, txtDescDesc);
                        iss.GetComponent<ItemSlotScript>().GetItem(combat.itemName, combat.description, DescParent);
                        break;
                    }
                }
                break;

            case 'w':
                foreach (WeaponObject weapon in Assets.assets.weaponTemp) {
                    if (weapon.name == id) {
                        ItemO = Instantiate(Item);
                        ItemO.transform.SetParent(Slot.transform, false);
                        itemInfo = ItemO.GetComponent<ItemInfo>();
                        itemInfo.GetData(weapon.icon, weapon.weaponName, weapon.description, weapon.name, weapon.cost, txtDescName, txtDescDesc);
                        iss.GetComponent<ItemSlotScript>().GetItem(weapon.weaponName, weapon.description, DescParent);
                        break;
                    }
                }
                break;
        }
    }
}
