using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HospitalInfo : MonoBehaviour {
    public GameObject Character;
    private CharacterScript characterScript;
    private Stats stats;
    public Text txtName, txtHp, txtLevel;
    public Scrollbar healthBar;
    private int barSize;

    private void Start() {
        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        txtName.text = characterScript.strName;
        txtHp.text = "HP: " + stats.hp + "/" + stats.maxHp;
        //txtLevel.text = stats.level.ToString();

        barSize = stats.maxHp / stats.hp;
        healthBar.size = barSize;
    }
}
