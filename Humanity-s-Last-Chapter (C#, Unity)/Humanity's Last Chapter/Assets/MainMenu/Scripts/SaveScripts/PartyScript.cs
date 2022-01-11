using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PartyScript : MonoBehaviour {
    private GameObject characterO;
    public GameObject character, UIHealth, MissionManager;
    private int partyMember;                //Används för att bestämma vilken position karaktären spawnar på
    private UIHealthBoi uiHealth;
    private List<CharacterScript> characterScriptList, toRemoveC;
    //private List<QuestObject> questList;
    private ScriptableQuest activeQuest;
    private List<Stats> statsList, toRemoveS;
    //private WorldScript worldScript;
    private Transform transParent;

    public GameObject weapon;
    private GameObject weaponO;

    public GameObject AbilityToInstantiate;

    void Start() {
        InstantiateLists();

        if (Assets.assets == null) {
            Assets.assets = new Assets();
            Assets.assets.GetAssets();
        }

        if (QuirkScript.quirkScript == null) {
            QuirkScript.quirkScript = new QuirkScript();
            QuirkScript.quirkScript.PrepareQuirks();
        }

        if (WorldScript.world == null) {
            WorldScript.world = new WorldScript();
            WorldScript.world.Reset();
            TestCharacters();
        } else {
            LoadWorld();
        }
    }

    public void SpawnCharacters() {
        characterScriptList = WorldScript.world.characterList;
        statsList = WorldScript.world.statsList;
        activeQuest = WorldScript.world.activeQuest;

        //Debug.Log("Characters(Party): " + characterScriptList.Count);

        partyMember = 0;
        transParent = GameObject.FindGameObjectWithTag("CharacterManager").transform;

        //foreach (QuestObject quest in questList) {
            MissionManager.GetComponent<MissionManagerScript>().StartQuest(activeQuest);
        //}

        GameObject[] abilitySlots = GameObject.FindGameObjectsWithTag("AbilitySlot");
        toRemoveC = new List<CharacterScript>();
        toRemoveS = new List<Stats>();
        
        foreach (CharacterScript characterScript in characterScriptList) {
            if (characterScript.isEnlisted) {
                characterO = Instantiate(character);
                characterO.GetComponent<CharacterScript>().LoadPlayer(characterScript);
                characterO.GetComponent<CharacterScript>().partyMember = partyMember + 1;
                characterO.GetComponent<Stats>().LoadPlayer(statsList[partyMember]);

                //Annan kod
                characterO.GetComponent<PersonalMovement>().relativePos = new Vector3(partyMember * 0.4f, partyMember * 0.4f);
                characterO.GetComponent<PersonalMovement>().relativePosNonRotated = new Vector3(partyMember * 0.4f, partyMember * 0.4f);
                characterO.GetComponent<PersonalMovement>().AddRelativeWaypoint(transform.position);
                gameObject.GetComponent<CharacterMovement>().AddPc(characterO);
                SpawnWeapon(characterScript.rangedId, characterO.transform);

                #region removeCode?
                //characterO.transform.SetParent(transParent, false);

                //if (characterO == null)
                //Debug.Log("wat");
                //if (characterO.GetComponent<CharacterScript>() != null) {
                //    if (characterO.GetComponent<CharacterScript>().inventory != null) {
                //        if (characterO.GetComponent<CharacterScript>().inventory.Length > 0) {
                //            GameObject go = Instantiate(AbilityToInstantiate);
                //            go.transform.SetParent(abilitySlots[partyMember].transform, false);
                //            go.GetComponent<AbilityScript>().AttachedSlot = abilitySlots[partyMember];

                //            //Assets.assets.combatTemp
                //            string id = characterO.GetComponent<CharacterScript>().inventory[0];
                //            if (id.Contains("ci")) {
                //                foreach (CombatItemObject item in Assets.assets.combatTemp) {
                //                    if (id == item.name) {
                //                        //weapon har all info man vill ha
                //                    }
                //                }
                //            }
                //            if (id.Contains("hi")) {
                //                foreach (HealingItemObject item in Assets.assets.healingTemp) {
                //                    if (id == item.name) {
                //                        //weapon har all info man vill ha
                //                    }
                //                }
                //            }

                //            abilitySlots[partyMember].GetComponent<AbilitySlotScript>().AttachedAbility = go;
                //        }


                //CombatItemObject c = new CombatItemObject();
                //if (characterO.GetComponent<CharacterScript>().itemID[0].TryGetComponent<CombatItemObject>(out c))
                //{
                //    go.GetComponent<AbilityScript>().abilityType = characterO.GetComponent<CharacterScript>().itemID[0].GetComponent<CombatItemObject>().abilityType;
                //} else
                //{
                //    go.GetComponent<AbilityScript>().abilityType = characterO.GetComponent<CharacterScript>().itemID[0].GetComponent<HealingItemObject>().abilityType;
                //}

                //}
                //}
                #endregion

                toRemoveC.Add(characterScript);
                toRemoveS.Add(statsList[partyMember]);
                
                partyMember++;
            }
        }

        for (int i = 0; i < toRemoveC.Count; i++) {
            WorldScript.world.characterList.Remove(toRemoveC[i]);
            WorldScript.world.statsList.Remove(toRemoveS[i]);
        }
        transform.position = new Vector3(3, 3, 0);
    }

    private void LoadWorld() {
        SpawnCharacters();
    }

    //Ska bort sen när vi är klara med att testa saker
    private void TestCharacters() { //Spawnar tre gubbar ifall man inte kommer från huben

        #region toRemove
        //Ability test
        //Debug.Log("skapa en turret ability");
        //GameObject[] abilitySlots = GameObject.FindGameObjectsWithTag("AbilitySlot");
        //GameObject go = Instantiate(AbilityToInstantiate);
        //go.transform.SetParent(abilitySlots[0].transform, false);
        //go.GetComponent<AbilityScript>().AttachedSlot = abilitySlots[0];
        //go.GetComponent<AbilityScript>().abilityType = AbilityScript.AbilityType.turret;
        //abilitySlots[0].GetComponent<AbilitySlotScript>().AttachedAbility = go;

        //GameObject go2 = Instantiate(AbilityToInstantiate);
        //go2.transform.SetParent(abilitySlots[1].transform, false);
        //go2.GetComponent<AbilityScript>().AttachedSlot = abilitySlots[1];
        //go2.GetComponent<AbilityScript>().abilityType = AbilityScript.AbilityType.grenade;
        //abilitySlots[1].GetComponent<AbilitySlotScript>().AttachedAbility = go2;
        #endregion

        for (int i = 0; i <= 0; i++)
        {
            characterO = Instantiate(character);
            characterO.GetComponent<CharacterScript>().partyMember = i + 1;
            characterO.GetComponent<PersonalMovement>().relativePos = new Vector3(i, i);
            characterO.GetComponent<PersonalMovement>().AddRelativeWaypoint(transform.position);
            gameObject.GetComponent<CharacterMovement>().AddPc(characterO);
            characterO.GetComponent<PersonalMovement>().relativePosNonRotated = new Vector3(i, i);
            SpawnWeapon("wp0", characterO.transform);
            //SpawnWeapon("wp" + Random.Range(0, Assets.assets.weaponTemp.Length), characterO.transform);
            characterO.transform.SetParent(transParent, false);
        }

        transform.position = new Vector3(3,3,0);
    }

    public void SpawnWeapon(string wpId, Transform parent) {
        weaponO = Instantiate(weapon, gameObject.transform.position, Quaternion.identity);
        weaponO.transform.SetParent(parent, false);
        weaponO.transform.localScale = new Vector3(1, 1, 1);

        foreach (WeaponObject wp in Assets.assets.weaponTemp) {
            if (wp.name == wpId) {
                weaponO.GetComponent<WeaponAttack>().GetData(wp);
                break;
            }
        }
    }


    private void InstantiateLists() {
        characterScriptList = new List<CharacterScript>();
        statsList = new List<Stats>();
    }
}
