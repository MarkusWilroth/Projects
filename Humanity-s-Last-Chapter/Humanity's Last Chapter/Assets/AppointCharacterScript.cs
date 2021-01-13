using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AppointCharacterScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text characterName;
    public GameObject Portrait;
    private GameObject Character, Manager, RoleDesc;
    private CharacterScript characterScript;
    private Stats stats;

    public void GetCharacter(GameObject Character, GameObject Manager) {
        this.Character = Character;
        this.Manager = Manager;
        RoleDesc = GameObject.FindGameObjectWithTag("QuestDesc");

        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        characterName.text = characterScript.strName;

        Portrait.GetComponent<CharacterScript>().LoadPlayer(characterScript);
        Portrait.GetComponent<Stats>().LoadPlayer(stats);
    }

    public void BtnAppoint() {
        Manager.GetComponent<RoleManagerScript>().SetRole(Character);
    }

    public void OnPointerEnter(PointerEventData pointerEventData) {
        RoleDesc.GetComponent<RoleDescScript>().CharacterInfo(Character);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        RoleDesc.GetComponent<RoleDescScript>().Clear();
    }
}
