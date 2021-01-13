using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterScript : MonoBehaviour {
    public string strName = "";
    public string title, id;
    public int inventorySize, partyMember;
    private string[] firstName = { "Fred", "Greg", "Mike", "George", "Steve", "Philip", "Tom", "Benjamin", "Samuel", "Conrad", "Tristan", "Jan", "Igor", "Leif", "Kevin", "Lorens", "Alberto", "Michael", "Johan", "Adam", "Daniel", "Matteo" };
    private string[] lastName = { "McGreg", "Harris", "Jackson", "Smith", "Lang", "Thomson", "Williams", "Jones", "Davis", "Brown", "Miller", "Wilson", "Black", "White", "Taylor", "Carter", "Cox", "Hughes", "Perry", "Jenkins", "Bell", "Hernandez", "Hill", "Lopez", "Bryant" };
    public string rangedId, combatId, healingId, clothId, headId;
    public string[] inventory;
    public bool inHospital, isEnlisted, isYou, isEssential; //Hospital är ifall man är i hospital, enlisted är ifall man ska på uppdrag, you är ifall karaktären är spelaren, essential är ifall karaktären är odödlig
    public Faction faction;
    public RoleObject role;
    private GameObject[] otherCharacters;

    public enum Faction {
        unassigned,
        playerFaction,      //Styrs av karaktären och kommer in i hubben
        neutralFaction,     //Attackerar ingen, kan ha quests, bara aktiva på missions
        enemyFaction,       //Attackerar character av player faction och alliedFaction
        alliedFaction,      //Attackerar enemyFaction
        UI,                 //Blir untagged (för UI(Mission)Character och ActiveCharacter
        activeCharacter
    }
    

    void Start() {

        switch (faction) {
            case Faction.unassigned:
                break;
            case Faction.playerFaction:
                gameObject.tag = "Character";
                break;
            case Faction.neutralFaction:
                gameObject.tag = "NeutralFaction";
                break;
            case Faction.enemyFaction:
                gameObject.tag = "Enemy";
                break;
            case Faction.alliedFaction:
                gameObject.tag = "AlliedFaction";
                break;
            case Faction.UI:
                gameObject.tag = "UI";
                break;
            case Faction.activeCharacter:
                gameObject.tag = "ActiveCharacter";
                break;
            default:
                gameObject.tag = "Untagged";
                Debug.Log("Character has no allignment");
                break;
        } //Sätter tag beroende på vilken sorts karaktär det är
        
        if (strName == "") {

            inventorySize = 5;
            strName = NameGenerator();

            if(id == "") {
                id = GetId();
            }

            combatId = "";
            healingId = "";
            rangedId = "wp0";
            inventory = new string[inventorySize];
        }
        
    }

    public void NewCharacter(string strName, string clothId, string headId) {
        this.strName = strName;
        this.clothId = clothId;
        this.headId = headId;
    }

    private string NameGenerator() { //Genererar ett namn
        strName = firstName[(int)Random.Range(0, firstName.Length)] + " " + lastName[(int)Random.Range(0, lastName.Length)];
        return strName;
    }

    public string GetId() {
        otherCharacters = GameObject.FindGameObjectsWithTag("Character");
        int testId = 0;
        bool isPossible = false;
        while (!isPossible) {
            isPossible = true;
            foreach (GameObject character in otherCharacters) {
                if (character.GetComponent<CharacterScript>().id == "ch" + testId) {
                    isPossible = false;
                    testId++;
                    break;
                }
            }
        }
        id = "ch" + testId;
        //int characterCounter = GameObject.FindGameObjectWithTag("CharacterManager").transform.childCount - 1;
        //id = "ch" + characterCounter;
        return id;
    }

    public void LoadPlayer(CharacterScript data) {
        role = data.role;
        strName = data.strName;
        id = data.id;
        rangedId = data.rangedId;
        healingId = data.healingId;
        combatId = data.combatId;
        clothId = data.clothId;
        headId = data.headId;
        inventory = data.inventory;
        inHospital = data.inHospital;
        isEnlisted = data.isEnlisted;
    }

    public void OnDeath() {
        PrepareInventory();
        GetComponent<InventoryScript>().GetInventory(inventory, strName);
    }

    private void PrepareInventory() {
        if (inventory != null) {
            inventorySize = 5;
            inventory = new string[inventorySize];
        }
        int i = 0;
        if (rangedId != null) {
            inventory[i] = rangedId;
        } else {
            inventory[i] = "";
        }
        i++;

        if (combatId != null) {
            inventory[i] = combatId;
        } else {
            inventory[i] = "";
        }
        i++;

        if (healingId != null) {
            inventory[i] = healingId;
        } else {
            inventory[i] = "";
        }
        i++;

        if (clothId != null) {
            inventory[i] = clothId;
        } else {
            inventory[i] = "";
        }
        i++;

        if (headId != null) {
            inventory[i] = headId;
        } else {
            inventory[i] = "";
        }
    }
}
