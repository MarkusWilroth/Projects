using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothScript : MonoBehaviour {

    public GameObject Character;
    public Sprite UIMask;

    private ClothItemObject item;
    private string headId, clothId;
    private ClothType clothType;
    private CharacterScript characterScript;
    private Stats stats;

    public enum ClothType {
        HeadGear,
        Cloth
    }

    public enum ClothCategory {
        StartGear,
        LowLevel,
        MedLevel,
        HighLevel,
        Faction
    }

    private void Start() {
        
        

        //headId = characterScript.headId;
        //clothId = characterScript.clothId;

        //if (headId != null) {
        //    FindItem(headId);
        //}
        //if (clothId != null) {
        //    FindItem(clothId);
        //}
    }

    public void GetHolder(CharacterScript characterScript, Stats stats, string itemId) {
        this.characterScript = characterScript;
        this.stats = stats;
        item = FindItem(itemId);
        if(item != null) {
            GetComponent<Image>().sprite = item.portrait;
        } else {
            GetComponent<Image>().sprite = UIMask;
        }
    }

    private ClothItemObject FindItem(string id) {
        if(id != "" || id != null) {
            foreach (ClothItemObject clothItem in Assets.assets.clothTemp) {
                if (clothItem.name == id) {
                    return clothItem;
                    //switch (clothItem.clothType) {
                    //    case ClothType.HeadGear:
                    //        item = clothItem;
                    //        break;
                    //    case ClothType.Cloth:
                    //        cloth = clothItem;
                    //        break;
                    //}
                    //break;
                }
            }
        }
        
        return null;
    }

    public void ChangeCloth(ClothItemObject NewItem) {
        if(item != null) {
            changeStats(item, -1);
        }                        //Tar bort all boost som gamla itemet hade

        if (NewItem != null) {
            changeStats(NewItem, 1);                        //Lägger till boost som den nya itemet har
            item = NewItem;                                 //Updaterar så att den har det nya itemet som old
            GetComponent<Image>().sprite = item.portrait;   //Updaterar Bilden
        }
        
    }
    private void changeStats(ClothItemObject item, int modifier) { //Ändrar statsen beroende på plaggets värde
        if(stats == null) {
            stats = Character.GetComponent<Stats>();
        }

        stats.maxHp += (item.maxHp * modifier);
        stats.hp += (item.maxHp * modifier);
        stats.str += (item.str * modifier);
        stats.def += (item.def * modifier);
        stats.Int += (item.Int * modifier);
        stats.dex += (item.dex * modifier);
        stats.cha += (item.cha * modifier);
        stats.ldr += (item.ldr * modifier);
        stats.nrg += (item.nrg * modifier);
        stats.snt += (item.snt * modifier);
    }
}
