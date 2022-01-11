using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {

    public GameObject Character;
    private CharacterScript characterScript;
    private Stats stats;
    public Text txtName, txtInt, txtDex, txtCha, txtLdr, txtSnt, txtStr, txtDef, txtHp, txtLevel;
    public Scrollbar healthBar;
    private int barSize;

    private void Start() {
        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        txtName.text = characterScript.strName;
        //txtInt.text = "Int: " + stats.Int;
        txtDex.text = "Dex: " + stats.dex;
        //txtCha.text = "Cha: " + stats.cha;
        //txtLdr.text = "Ldr: " + stats.ldr;
        //txtSnt.text = "Snt: " + stats.snt;
        //txtStr.text = "Str: " + stats.str;
        txtDef.text = "Def: " + stats.def;
        txtHp.text = "HP: " + stats.hp + "/" + stats.maxHp;
        //txtLevel.text = stats.level.ToString();

        barSize = stats.maxHp / stats.hp;
        healthBar.size = barSize;
    }

    public void UpdateStats() {
        if (stats != null) {
            txtName.text = characterScript.strName;
            txtDex.text = "Dex: " + stats.dex;
            txtDef.text = "Def: " + stats.def;
            txtHp.text = "HP: " + stats.hp + "/" + stats.maxHp;
            //txtLevel.text = stats.level.ToString();

            barSize = stats.maxHp / stats.hp;
            healthBar.size = barSize;
        }
    }
}
