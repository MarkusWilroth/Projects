using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterToAppoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text roleName;
    private GameObject Character, CharacterInfo, Manager;
    private CharacterScript characterScript;
    private Stats stats;
    private QuestDescScript questDescScript;

    public void GetCharacter(GameObject Character, GameObject Manager) {
        this.Character = Character;
        this.Manager = Manager;
        CharacterInfo = GameObject.FindGameObjectWithTag("QuestDesc");
        questDescScript = CharacterInfo.GetComponent<QuestDescScript>();
        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        roleName.text = characterScript.strName;
    }

    public void BtnReturn() {
        Manager.GetComponent<CommandCenterScript>().Regrates();
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        questDescScript.CharacterInfo(Character);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        questDescScript.ClearRole();
        questDescScript.QuestReset();
    }
}
