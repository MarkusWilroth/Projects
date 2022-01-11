using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ClothScript;

public class PortraitScript : MonoBehaviour {

    public GameObject HeadGear, Cloth, Background;
    private CharacterScript characterScript;
    private Stats stats;
    private ClothScript headScript, clothScript;
    public Sprite defultPortrait;

    private void Start() {
        characterScript = GetComponent<CharacterScript>();
        stats = GetComponent<Stats>();
        headScript = HeadGear.GetComponent<ClothScript>();
        clothScript = Cloth.GetComponent<ClothScript>();

        headScript.GetHolder(characterScript, stats, characterScript.headId);
        clothScript.GetHolder(characterScript, stats, characterScript.clothId);
    }
    public void NoRole() {
        Background.GetComponent<Image>().sprite = defultPortrait;
    }

    public void UpdatePortrait() {
        characterScript = GetComponent<CharacterScript>();
        stats = GetComponent<Stats>();
        headScript = HeadGear.GetComponent<ClothScript>();
        clothScript = Cloth.GetComponent<ClothScript>();

        headScript.GetHolder(characterScript, stats, characterScript.headId);
        clothScript.GetHolder(characterScript, stats, characterScript.clothId);
    }

    public void ChangeCloth(ClothItemObject newCloth) {
        if (characterScript == null) {
            stats = GetComponent<Stats>();
            characterScript = GetComponent<CharacterScript>();
            headScript = HeadGear.GetComponent<ClothScript>();
            clothScript = Cloth.GetComponent<ClothScript>();
        }

        switch (newCloth.clothType) {
            case ClothType.HeadGear:
                characterScript.headId = newCloth.name;
                headScript.ChangeCloth(newCloth);
                break;
            case ClothType.Cloth:
                characterScript.clothId = newCloth.name;
                clothScript.ChangeCloth(newCloth);
                break;
        }
    }
    public void RemoveCloth() {
        characterScript.clothId = "";
        clothScript.ChangeCloth(null);
    }
    public void RemoveHeadGear() {
        characterScript.headId = "";
        headScript.ChangeCloth(null);
    }
}
