using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppointedRoleScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Text txtCharacterName, txtRoleName;
    public GameObject Portrait, btnAppoint, btnRemove;

    private bool isAppointed;

    private RoleObject role;
    private CharacterScript characterScript;
    private Stats stats;
    private RoleScript roleScript;
    private GameObject Manager, RoleDesc, Character;
    private GameObject[] characters;

    private void Start() {
        RoleDesc = GameObject.FindGameObjectWithTag("QuestDesc"); //Skapa en ny tag för RoleQuest? Borde inte behövas men kan bli snyggare
    }

    public void GetRole(RoleObject role, GameObject Manager) { //Får rollen samt Manager för att komma åt dess metoder
        this.role = role;
        this.Manager = Manager;

        txtRoleName.text = role.roleName;

        //Kollar alla karaktärer ifall de har denna roll och ger portrait värden om det finns
        //characters = GameObject.FindGameObjectsWithTag("Character");
        //foreach (GameObject character in characters) {
        //    characterScript = character.GetComponent<CharacterScript>();
        //    if (characterScript.role == role) {
        //        stats = character.GetComponent<Stats>();
        //        roleScript = character.GetComponent<RoleScript>();
        //        isAppointed = true; //Håller koll på om knappen Appoint eller Remove ska visas
        //        Portrait.GetComponent<CharacterScript>().LoadPlayer(characterScript);
        //        Portrait.GetComponent<Stats>().LoadPlayer(stats);

        //        txtCharacterName.text = characterScript.strName;
        //    }
        //}
        SetBtn(); //Ändrar så att rätt knapp visas (Appoint/Remove)
    }

    private void SetBtn() {
        if (isAppointed) {
            Portrait.transform.GetChild(0).gameObject.SetActive(true);
            btnAppoint.SetActive(false);
            btnRemove.SetActive(true);
        } else {
            Portrait.transform.GetChild(0).gameObject.SetActive(false);
            btnAppoint.SetActive(true);
            btnRemove.SetActive(false);
        }
    }

    public void BtnAppoint() {
        Manager.GetComponent<CommandCenterScript>().SetRole(role);
    }

    public void BtnSwitch() {
        Manager.GetComponent<CommandCenterScript>().SwitchRole(role, Character);
    }

    public void GetCharacter(GameObject Character) {
        this.Character = Character;

        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        //roleScript.ChangeRole(role); //Kanske alternativ för rad 71-72

        //roleScript.ChangeStats(characterScript.role, -1); //Tar bort buffar från gamla rollen
        //roleScript.ChangeStats(role, 1); //Ger buffar från den nya rollen

        //characterScript.role = role;
        isAppointed = true;

        Portrait.GetComponent<CharacterScript>().LoadPlayer(characterScript);
        Portrait.GetComponent<Stats>().LoadPlayer(stats);
        txtCharacterName.text = characterScript.strName;
        SetBtn();
    }
    public void OnPointerEnter(PointerEventData pointerEventData) {
        RoleDesc.GetComponent<QuestDescScript>().RoleInfo(role);
    }

    public void OnPointerExit(PointerEventData pointerEventData) {
        RoleDesc.GetComponent<QuestDescScript>().ClearRole();
    }

    public void BtnChangeRole() {
        Manager.GetComponent<CommandCenterScript>().ChangeRole(Character);
    }
}
