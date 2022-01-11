using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDescScript : MonoBehaviour {

    public Text txtName, txtDesc, txtReward, txtHp, txtStr, txtDef, txtDex, txtInt, txtLdr, txtSnt, txtCha;
    private List<string> rewardList;
    private string reward;
    private int rewardCount, i;
    private ScriptableQuest activeQuest;
    CharacterScript characterScript;
    Stats stats ;
    public GameObject StatsBox, RewardBox;

    private void Start() {
        txtName.text = "";
        txtDesc.text = "";
        rewardList = new List<string>();
        reward = "";
        txtReward.text = reward;
    }

    public void QuestInfo(ScriptableQuest quest) {
        RewardBox.SetActive(true);
        StatsBox.SetActive(false);
        txtName.text = quest.missionName;
        txtDesc.text = quest.description;
        rewardList.Clear();

        reward = "Reward: ";

        if (quest.goldReward > 0) {
            rewardList.Add(quest.goldReward + "gold");
        }

        if (quest.rsReward > 0) {
            rewardList.Add(quest.rsReward + "rs");
        }

        rewardCount = rewardList.Count;
        i = 0;
        foreach (string reward in rewardList) {
            this.reward += reward;

            i++;
            if (i < rewardCount - 1) { //Om i är större än detta vet vi att nästa reward är sista
                this.reward += ", ";
            } else if (i == rewardCount - 1) {
                this.reward += " & ";
            }
            
        }
        txtReward.text = reward;
    }

    public void QuestReset() {
        activeQuest = WorldScript.world.activeQuest;
        if(activeQuest != null) {
            QuestInfo(activeQuest);
        } else {
            ClearQuest();
        }
    }

    private void ClearQuest() {
        txtName.text = "";
        txtDesc.text = "";
        reward = "";
        txtReward.text = reward;
    }
    

    public void ClearRole() {
        txtName.text = "";
        txtDesc.text = "";
        txtHp.text = "";
        //txtStr.text = "";
        txtDef.text = "";
        txtDex.text = "";
        //txtInt.text = "";
        //txtLdr.text = "";
        //txtSnt.text = "";
        //txtCha.text = "";
    }

    public void RoleInfo(RoleObject role) {
        RewardBox.SetActive(false);
        StatsBox.SetActive(true);

        txtName.text = role.roleName;
        txtDesc.text = role.desc;
        txtHp.text = "Hp: +" + role.maxHp;
        //txtStr.text = "Str: +" + role.str;
        txtDef.text = "Def: +" + role.def;
        txtDex.text = "Dex: +" + role.dex;
        //txtInt.text = "Int: +" + role.Int;
        //txtLdr.text = "Ldr: +" + role.ldr;
        //txtSnt.text = "Snt: +" + role.snt;
        //txtCha.text = "Cha: +" + role.cha;
    }

    public void CharacterInfo(GameObject Character) {
        this.characterScript = Character.GetComponent<CharacterScript>();
        this.stats = Character.GetComponent<Stats>();

        RewardBox.SetActive(false);
        StatsBox.SetActive(true);

        txtName.text = characterScript.strName;
        txtDesc.text = "";
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
