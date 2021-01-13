using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandCenterScript : MonoBehaviour {

    public GameObject ForMission, ForRoles, ForCharacterChange, MissionBox, CharacterBox, RoleBox, AppointedRole, BtnStartMission;   //De med for fungerar som parents, de med Box är prefabs (Borde ha samma namn)
    public Text txtStartMission; //För att ändra texten till startMission knappen så man kan se om den är redo att starta mission
    private GameObject holder, CharacterToAlter, CoinBox;    //holder används för att simpelt komma åt värden från instansierat object | CharacterToAlter är karaktären som ska byta roll
    private GameObject[] hubCharacters, commandCharacters;    //Alla karaktärer som har tag "Character"
    private List<GameObject> characterList; //Håller koll så att den inte ger samma karaktär till olika roller
    public RoleObject citizenRole;  //Standarad roll som ges till karaktärer (Borde inte behövas, alla karaktärer skapas med en roll)
    public int appointedCharacters; //Hur många karaktärer som ska gå på mission
    private CharacterScript hubCharacterScript, commandCharacterScript;
    private Color colorReadyGreen;

    private void Start() {
        characterList = new List<GameObject>();
        appointedCharacters = 0;
        CreateQuestList();  //Skapar listan av alla valbara quests
        CreateCharacterList();  //Skapar listan av alla karaktärer som kan gå på mission
        CheckMissionReady();

        CoinBox = GameObject.FindGameObjectWithTag("CoinBox");
        CoinBox.SetActive(false);
    }

    private void CreateQuestList() {
        foreach (ScriptableQuest quest in WorldScript.world.avalibleQuests) {
            holder = Instantiate(MissionBox);
            holder.GetComponent<MissionBoxScript>().GetQuest(quest, gameObject);
            holder.transform.SetParent(ForMission.transform, false);
        }
    }

    private void ClearRoleList() {  //Ser till så att alla barn till ForRole försvinner (Byter man till att välja role ska inte karaktärer synas etc)
        foreach (Transform child in ForRoles.transform) {
            Destroy(child.gameObject);
        }
    }

    public void CreateCharacterList() {
        appointedCharacters = 0;
        ClearRoleList();

        foreach (Transform child in ForCharacterChange.transform) {
            Destroy(child.gameObject);
        }

        if (hubCharacters == null) {
            hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        }

        foreach (GameObject character in hubCharacters) {
            holder = Instantiate(RoleBox);
            if (character.GetComponent<CharacterScript>().role == null) {
                character.GetComponent<CharacterScript>().role = citizenRole;
            }

            holder.GetComponent<RoleBoxScript>().GetCharacter(character, gameObject);
            holder.transform.SetParent(ForRoles.transform, false);
        }
    }

    public void CreateRoleList() {
        ClearRoleList();
        characterList.Clear();

        if (hubCharacters == null) {
            hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        }

        foreach (GameObject character in hubCharacters) {
            characterList.Add(character);
        }

        foreach (RoleObject role in WorldScript.world.activeRoles) {
            holder = Instantiate(AppointedRole);
            holder.transform.SetParent(ForRoles.transform, false);
            holder.GetComponent<AppointedRoleScript>().GetRole(role, gameObject);

            foreach (GameObject character in characterList) {                           //Förbereder för att kolla om det finns någon karaktär med den rollen
                if (character.GetComponent<CharacterScript>().role == role) {           //Om det finns en karaktär med den rollen kommer den att ge rollen karaktären och ta bort karaktären 
                    holder.GetComponent<AppointedRoleScript>().GetCharacter(character); //från listan så att samma karaktär inte kan få mer än en roll
                    if (character == CharacterToAlter) {
                        Destroy(holder);
                    }
                    characterList.Remove(character);
                    break;
                }
            }
        }
    }

    public void ChangeRole(GameObject Character) {  //När man klickar på knappen BtnChangeRole i RoleBox kommer karaktären att sparas som CharacterToAlter för att sen kunna ändras samt
        commandCharacters = GameObject.FindGameObjectsWithTag("UIHospitalCharacter"); //Tar fram alla som är i hospital
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject hospitalCharacter in commandCharacters) { //Kollar alla i hospital
            commandCharacterScript = hospitalCharacter.GetComponent<CharacterScript>();


            foreach (GameObject hubCharacter in hubCharacters) { //Kollar alla karaktärer i hubben
                hubCharacterScript = hubCharacter.GetComponent<CharacterScript>();

                if (commandCharacterScript.id == hubCharacterScript.id) { //Samma boi

                    hubCharacterScript.LoadPlayer(commandCharacterScript);
                    break;
                }

            }
        }

        CharacterToAlter = Character;               //ändras det så att man ser roller och inte karaktärer
        holder = Instantiate(CharacterBox);
        holder.GetComponent<CharacterToAppoint>().GetCharacter(Character, gameObject);
        holder.transform.SetParent(ForCharacterChange.transform, false);

        CreateRoleList();
    }

    public void SetRole(RoleObject role) {
        //if (oldCharacter != null) {     //Om det fanns en karaktär på rollen man väljer ändras den till citizenRole
        //    oldCharacter.GetComponent<CharacterScript>().role = citizenRole;
        //}
        CharacterToAlter.GetComponent<RoleScript>().ChangeRole(role);   //Ändrar rollen på den man klickade på till den nya rollen

        CreateCharacterList();  //Tar tillbaka så att man ser karaktärena igen
    }

    public void SwitchRole(RoleObject role, GameObject oldCharacter) {  //Om man kilckar på knappen AlterCharacter (Dyker upp om rollen redan används)
        oldCharacter.GetComponent<RoleScript>().ChangeRole(CharacterToAlter.GetComponent<CharacterScript>().role);  //Sätter den som hade rollen redan så att den får den nya rollen
        CharacterToAlter.GetComponent<RoleScript>().ChangeRole(role);   //Ger den man skulle ändra roll på så att den får den nya rollen
        CreateCharacterList();
    }

    public void Regrates() {    //Om man klickar på knappen Return istället för att ändra roll (Ändrar inte roll)
        CharacterToAlter = null;
        CreateCharacterList();
    }

    public void BtnExit() {
        commandCharacters = GameObject.FindGameObjectsWithTag("UIHospitalCharacter"); //Tar fram alla som är i hospital
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject hospitalCharacter in commandCharacters) { //Kollar alla i hospital
            commandCharacterScript = hospitalCharacter.GetComponent<CharacterScript>();
            

            foreach (GameObject hubCharacter in hubCharacters) { //Kollar alla karaktärer i hubben
                hubCharacterScript = hubCharacter.GetComponent<CharacterScript>();

                if (commandCharacterScript.id == hubCharacterScript.id) { //Samma boi
                    
                    hubCharacterScript.LoadPlayer(commandCharacterScript);
                    break;
                }

            }
        }
        CoinBox.SetActive(true);
        Destroy(gameObject);
    }

    public void BtnGoToMission() {
        commandCharacters = GameObject.FindGameObjectsWithTag("UIHospitalCharacter"); //Tar fram alla som är i hospital
        hubCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject hospitalCharacter in commandCharacters) { //Kollar alla i hospital
            commandCharacterScript = hospitalCharacter.GetComponent<CharacterScript>();


            foreach (GameObject hubCharacter in hubCharacters) { //Kollar alla karaktärer i hubben
                hubCharacterScript = hubCharacter.GetComponent<CharacterScript>();

                if (commandCharacterScript.id == hubCharacterScript.id) { //Samma boi

                    hubCharacterScript.LoadPlayer(commandCharacterScript);
                    break;
                }

            }
        }
        if (WorldScript.world.activeQuest != null && appointedCharacters > 0 && appointedCharacters <= WorldScript.world.partySize) {
            GetComponent<SceneSwitcher>().GoToMission();
        }
    }

    public void CheckMissionReady() {   //Används för att kolla om mission är ready och för att visa varför den inte är det
        if (WorldScript.world.activeQuest == null) {
            BtnStartMission.GetComponent<Image>().color = Color.red;
            txtStartMission.text = "Need an active quest!";
        } else if (appointedCharacters <= 0) {
            BtnStartMission.GetComponent<Image>().color = Color.red;
            txtStartMission.text = "Appoint people!";
        } else if (appointedCharacters > WorldScript.world.partySize) {
            BtnStartMission.GetComponent<Image>().color = Color.red;
            txtStartMission.text = "Too many characters! " + appointedCharacters.ToString() + "/" + WorldScript.world.partySize.ToString();
        } else {
            //if ( colorReadyGreen == null) {
            //    colorReadyGreen = new Color32(17/255, 176/255, 0/255, 255/255);
            //}
            colorReadyGreen = new Color(17/255f, 176/255f, 0, 255);
            BtnStartMission.GetComponent<Image>().color = colorReadyGreen;
            txtStartMission.text = "Appointed Characters: " + appointedCharacters.ToString() + "/" + WorldScript.world.partySize.ToString();
        }
    }
}
