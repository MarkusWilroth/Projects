using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleManagerScript : MonoBehaviour {

    public GameObject ForRoles, ForCharacters, CharacterBox, RoleBox;
    private GameObject holder, activeRoleBox;
    private GameObject[] Characters;

    private void Start() {
        GetRole(); //Skapar alla roller och ger dem gameObject så att de lätta kan komma åt metoden GetCharacters
    }

    public void GetCharacters(GameObject activeRoleBox) {
        this.activeRoleBox = activeRoleBox;
        

        Characters = GameObject.FindGameObjectsWithTag("Character"); //Skulle kunna unvika denna ifall det blir för dyrt genom att skicka characters från AppointedRoleScript
        foreach (GameObject character in Characters) {
            if (character.GetComponent<CharacterScript>().role == null) { //Listar endast karaktärer som inte har en roll
                holder = Instantiate(CharacterBox);
                holder.transform.SetParent(ForCharacters.transform, false);
                holder.GetComponent<AppointCharacterScript>().GetCharacter(character, gameObject);
            }
        }
    }

    public void GetRole() { //Fixa de i rang ordning?
        foreach (RoleObject role in WorldScript.world.activeRoles) {
            holder = Instantiate(RoleBox);
            holder.transform.SetParent(ForRoles.transform, false);
            holder.GetComponent<AppointedRoleScript>().GetRole(role, gameObject);
        }
    }

    public void SetRole(GameObject character) {
        if (activeRoleBox != null) { //Borde inte kunna vara det
            activeRoleBox.GetComponent<AppointedRoleScript>().GetCharacter(character);
        }
        foreach (Transform child in ForCharacters.transform) { //Ska hända när en karaktär blir appointed... inte här
            Destroy(child.gameObject);
        }
    }
}
