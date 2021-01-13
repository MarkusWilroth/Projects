using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleBoxScript : MonoBehaviour {

    public Text txtName, txtRole, txtHp;
    public Scrollbar healthBar;
    public GameObject AppointBtn, Portrait;
    private GameObject Manager, Character;
    private GameObject[] hubCharacters, boxCharacters;
    private RoleObject role;
    private CharacterScript hubCharacterScript, boxCharacterScript;
    private Stats stats;
    private bool isAppointed, roleTaken;

    public void GetRole(RoleObject role, GameObject Manager) {
        this.role = role;
        this.Manager = Manager;

        roleTaken = false;
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject character in hubCharacters) {
            hubCharacterScript = character.GetComponent<CharacterScript>();
            if (hubCharacterScript.role == role) {
                stats = character.GetComponent<Stats>();

                Portrait.GetComponent<CharacterScript>().LoadPlayer(hubCharacterScript);
                Portrait.GetComponent<Stats>().LoadPlayer(stats);

                SetStats();
                hubCharacterScript.isEnlisted = false;
                roleTaken = true;
                break;
            }
        }
        if (!roleTaken) {
            //Destroy(Portrait);
            EmptyRole();
        }
    }

    public void GetCharacter(GameObject Character, GameObject Manager) {
        this.Character = Character;
        this.Manager = Manager;

        hubCharacterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();

        Portrait.GetComponent<CharacterScript>().LoadPlayer(hubCharacterScript);
        Portrait.GetComponent<Stats>().LoadPlayer(stats);

        boxCharacterScript = Portrait.GetComponent<CharacterScript>();

        role = boxCharacterScript.role;
        if (role != null) {
            txtRole.text = role.roleName;
        } else {
            txtRole.text = "-No Role-";
        }
        PrepareAppoint();

        txtName.text = boxCharacterScript.title + boxCharacterScript.strName;

        SetStats();
        //boxCharacterScript.isEnlisted = false;
    }

    private void EmptyRole() {
        txtName.text = "Role: " + role.roleName;
        txtHp.text = "";

        float barSize = 0;
        healthBar.size = barSize;
    }

    private void SetStats() {
        txtName.text = hubCharacterScript.title + hubCharacterScript.strName;
        txtHp.text = "Hp: " + stats.hp + "/" + stats.maxHp;

        float barSize = stats.maxHp / stats.hp;
        healthBar.size = barSize;
    }

    public void BtnAppoint() {
        if (stats != null && !boxCharacterScript.inHospital) {
            if (isAppointed) {
                AppointBtn.GetComponent<Image>().color = Color.red;
                AppointBtn.GetComponentInChildren<Text>().text = "A\np\np\no\ni\nn\nt";
                boxCharacterScript.isEnlisted = false;
                isAppointed = false;
                Manager.GetComponent<CommandCenterScript>().appointedCharacters--;
                //Göra karaktären notEnlisted
            } else {
                AppointBtn.GetComponent<Image>().color = Color.green;
                AppointBtn.GetComponentInChildren<Text>().text = "U\nn\na\np\np\no\ni\nn\nt";
                boxCharacterScript.isEnlisted = true;
                isAppointed = true;
                Manager.GetComponent<CommandCenterScript>().appointedCharacters++;
                //Göra karaktären enlisted
            }
            Manager.GetComponent<CommandCenterScript>().CheckMissionReady();
        } else {
            AppointBtn.GetComponent<Image>().color = Color.gray;
            AppointBtn.GetComponentInChildren<Text>().text = "I\nn\n \nH\no\ns\np\ni\nt\na\nl";
            boxCharacterScript.isEnlisted = false;
            isAppointed = false;
        }
    }

    private void PrepareAppoint() {
        if (hubCharacterScript.inHospital) {
            AppointBtn.GetComponent<Image>().color = Color.gray;
            AppointBtn.GetComponentInChildren<Text>().text = "I\nn\n \nH\no\ns\np\ni\nt\na\nl";
        }
        else if (hubCharacterScript.isEnlisted) {
            isAppointed = true;
            AppointBtn.GetComponent<Image>().color = Color.green;
            AppointBtn.GetComponentInChildren<Text>().text = "U\nn\na\np\np\no\ni\nn\nt";
            Manager.GetComponent<CommandCenterScript>().appointedCharacters++;
        } else {
            isAppointed = false;
            AppointBtn.GetComponent<Image>().color = Color.red;
            AppointBtn.GetComponentInChildren<Text>().text = "A\np\np\no\ni\nn\nt";
        }
        Manager.GetComponent<CommandCenterScript>().CheckMissionReady();
    }

    public void BtnChangeRole() {
        Manager.GetComponent<CommandCenterScript>().ChangeRole(Character);
    }
}
 /* ToDo:
  * Klickar man på start ska det endast gå om karatärer som är appointed <= partySize
  * Endast de som är appointed kan gå på mission
  */
