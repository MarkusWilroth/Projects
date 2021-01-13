using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleDescScript : MonoBehaviour {
    public Text txtRoleName, txtRoleDesc, txtHp, txtStr, txtDef, txtDex, txtInt, txtLdr, txtSnt, txtCha;
    private string desc;

    private void Start() {
        Clear();
    }

    public void Clear() {
        desc = "";
        txtRoleName.text = "";
        txtRoleDesc.text = "";
        txtHp.text = "";
        txtStr.text = "";
        txtDef.text = "";
        txtDex.text = "";
        txtInt.text = "";
        txtLdr.text = "";
        txtSnt.text = "";
        txtCha.text = "";
    }

    public void RoleInfo(RoleObject role) {
        txtRoleName.text = role.roleName;
        txtRoleDesc.text = role.desc;
        txtHp.text = "Hp: +" + role.maxHp;
        txtDef.text = "Def: +" + role.def;
        txtDex.text = "Dex: +" + role.dex;
    }

    public void CharacterInfo(GameObject character) {
        CharacterScript characterScript = character.GetComponent<CharacterScript>();
        Stats stats = character.GetComponent<Stats>();

        txtRoleName.text = characterScript.strName;
        txtRoleDesc.text = "";
        txtHp.text = "Hp: " + stats.hp;
        txtStr.text = "Str: " + stats.str;
        txtDef.text = "Def: " + stats.def;
        txtDex.text = "Dex: " + stats.dex;
        txtInt.text = "Int: " + stats.Int;
        txtLdr.text = "Ldr: " + stats.ldr;
        txtSnt.text = "Snt: " + stats.snt;
        txtCha.text = "Cha: " + stats.cha;
    }
}
