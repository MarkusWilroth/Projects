using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeCharacterScript : MonoBehaviour {

    public InputField characterName;
    public GameObject character;
    private CharacterScript characterScript;
    private List<ClothItemObject> avalibleCloth, avalibleHead;
    private PortraitScript portraitScript;
    private int clothItem, headItem;

    private void Start() {
        characterScript = character.GetComponent<CharacterScript>();
        portraitScript = character.GetComponent<PortraitScript>();
        characterName.text = characterScript.strName;

        avalibleCloth = new List<ClothItemObject>();
        avalibleHead = new List<ClothItemObject>();

        GetCloth();

        clothItem = Random.Range(0, avalibleCloth.Count);
        headItem = Random.Range(0, avalibleHead.Count);

        characterScript.clothId = avalibleCloth[clothItem].name;
        characterScript.headId = avalibleHead[headItem].name;
        portraitScript.ChangeCloth(avalibleCloth[clothItem]);
        portraitScript.ChangeCloth(avalibleHead[headItem]);
    }

    private void Update() {
        characterScript.strName = characterName.text;
    }

    private void GetCloth() {
        foreach (ClothItemObject item in Assets.assets.clothTemp) {
            if (item.clothCategory == ClothScript.ClothCategory.StartGear) {
                switch (item.clothType) {
                    case ClothScript.ClothType.HeadGear:
                        avalibleHead.Add(item);
                        break;
                    case ClothScript.ClothType.Cloth:
                        avalibleCloth.Add(item);
                        break;
                }
            }
        }
    }

    public void BtnNextCloth() {
        clothItem++;
        if(clothItem >= avalibleCloth.Count) {
            clothItem = 0;
        }
        portraitScript.ChangeCloth(avalibleCloth[clothItem]);
        characterScript.clothId = avalibleCloth[clothItem].name;
    }

    public void BtnPreCloth() {
        clothItem--;
        if (clothItem < 0) {
            clothItem = (avalibleCloth.Count) - 1;
        }
        portraitScript.ChangeCloth(avalibleCloth[clothItem]);
        characterScript.clothId = avalibleCloth[clothItem].name;
    }

    public void BtnNextHead() {
        headItem++;
        if(headItem >= avalibleHead.Count) {
            headItem = 0;
        }
        portraitScript.ChangeCloth(avalibleHead[headItem]);
        characterScript.headId = avalibleHead[headItem].name;
    }

    public void BtnPreHead() {
        headItem--;
        if (headItem < 0) {
            headItem = avalibleHead.Count - 1;
        }
        portraitScript.ChangeCloth(avalibleHead[headItem]);
        characterScript.headId = avalibleHead[headItem].name;
    }
}
