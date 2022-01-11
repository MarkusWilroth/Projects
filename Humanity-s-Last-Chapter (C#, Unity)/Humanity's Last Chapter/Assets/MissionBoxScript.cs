using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MissionBoxScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text txtName;
    private ScriptableQuest quest;
    private GameObject ActiveQuest, QuestDesc, forRoles, holder, Manager;
    public GameObject Role;
    private bool findingRole;
    private List<RoleObject> avalibleMilitaryRoles;
    RoleObject bestRole;

    private void Start() {
        ActiveQuest = GameObject.FindGameObjectWithTag("ForActiveQuest");
        QuestDesc = GameObject.FindGameObjectWithTag("QuestDesc");
        forRoles = GameObject.FindGameObjectWithTag("ForRole");
        avalibleMilitaryRoles = new List<RoleObject>();
    }

    public void GetQuest(ScriptableQuest quest, GameObject Manager) {
        this.quest = quest;
        this.Manager = Manager;
        txtName.text = quest.missionName;
    }

    public void SetActive() {
        WorldScript.world.activeQuest = quest;
        GetRoles();
        ActiveQuest.GetComponent<ActiveMissionScript>().GetActiveQuest();
        Manager.GetComponent<CommandCenterScript>().CheckMissionReady();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        QuestDesc.GetComponent<QuestDescScript>().QuestInfo(quest);
    }

    public void OnPointerExit(PointerEventData eventData) {
        QuestDesc.GetComponent<QuestDescScript>().QuestReset();
    }

    private void GetRoles() {
        //foreach (Transform child in forRoles.transform) {
        //    Destroy(child.gameObject);
        //}

        //avalibleMilitaryRoles.Clear();
        //foreach (RoleObject role in WorldScript.world.activeRoles) {        //Tar fram alla activa militara roller för att sedan kunna skapa de så att de blir listad i rankOrdning
        //    if (role.roleCategory == RoleScript.RoleCategory.Military || role.roleCategory == RoleScript.RoleCategory.Mobile) {
        //        avalibleMilitaryRoles.Add(role);
        //    }
        //}
        
        //while (avalibleMilitaryRoles.Count > 0) {
        //    foreach (RoleObject role in avalibleMilitaryRoles) {
        //        float roleValue = Mathf.Infinity; //Vi vill hitta den minsta
        //        if (role.roleRank < roleValue) {
        //            roleValue = role.roleRank;
        //            bestRole = role;
        //        }
        //    }
        //    holder = Instantiate(Role);
        //    holder.transform.SetParent(forRoles.transform, false);
        //    holder.GetComponent<RoleBoxScript>().GetRole(bestRole, gameObject);
        //    avalibleMilitaryRoles.Remove(bestRole);
        //}
        
        //for (int i = 0; i < WorldScript.world.partySize; i++) {
        //    findingRole = true;
        //    while (findingRole) {

        //    }
        //    foreach ()
        //}
        //Create the roles

            //Attach characters to the role
    }
}