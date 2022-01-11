using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIBoiScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TextMeshProUGUI txtName, txtLevel, txtHp, txtExp, txtSkills;
    public Slider healthSlider, expSlider;
    public GameObject InfoBox;
    private CharacterScript characterScript;
    private Stats statsScript;
    private int cha, str, def, intelligence, dex, com, nrg, snt, hp, maxHp, nextLevel, lvl;
    private int exp;
    public bool isOwned, isInItems;

    void Start() {
        characterScript = GetComponent<CharacterScript>();
        statsScript = GetComponent<Stats>();
        //Debug.Log("HP before: " + hp);
        str = statsScript.str;
        def = statsScript.def;
        hp = statsScript.hp;
        //Debug.Log("He After: " + hp);
        lvl = statsScript.level;
        maxHp = statsScript.maxHp;
        exp = statsScript.exp;
        nextLevel = statsScript.nextLevel;

        txtName.text = characterScript.strName;
        txtSkills.text = "STR: " + str + "\nDEF: " + def;
        txtHp.text = hp + "/" + maxHp;
        txtExp.text = exp + "/" + nextLevel;

        healthSlider.value = hp / maxHp;
        if (nextLevel != 0)
            expSlider.value = exp / nextLevel;

        if(!isOwned) {
            InfoBox.SetActive(true);
        }
    }

    public void SetStats(Stats statsScript)
    {
        str = statsScript.str;
        def = statsScript.def;
        hp = statsScript.hp;
        lvl = statsScript.level;
        maxHp = statsScript.maxHp;
        exp = statsScript.exp;
        nextLevel = statsScript.nextLevel;

        txtSkills.text = "STR: " + str + "\nDEF: " + def;
        txtHp.text = hp + "/" + maxHp;
        txtExp.text = exp + "/" + nextLevel;

        healthSlider.value = hp / maxHp;
        expSlider.value = exp / nextLevel;
    }

    public void GetPos(int childCounter) {
        transform.position = new Vector3(0, 210 - (105 * childCounter),0);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if(isOwned && !isInItems) {
            InfoBox.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData) {
        if(isOwned && !isInItems) {
            InfoBox.SetActive(false);
        }
    }

    public void OpenItems() {
        isInItems = true;

        //Make it false for all other UIItems
    }

    public void CloseItems() {
        isInItems = false;
    }
}
