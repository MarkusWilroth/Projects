using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roasterScript : MonoBehaviour {

    public GameObject character;
    private GameObject characterO;
    private int rand, ammountRand;
    private Vector3 roasterPos;

    private List<WeaponObject> startWeaponList;
    private List<ClothItemObject> startClothList;
    private List<ClothItemObject> startHeadList;

    public void CreateBarrackBoiz() {
        if (startWeaponList == null) {
            PrepareLists();
        }
        roasterPos = new Vector3(-600, 180, 1); //sjukt fult måste göras snyggare!
        if (!WorldScript.world.spawnedBoiz) { //Om spawnedBoiz är false har det inte spawnar karaktärer denna runda
            SpawnNewBarrack();
            WorldScript.world.spawnedBoiz = true;
        } else {
            LoadBarrack();
        }
        
    } private void SpawnNewBarrack() {
        ammountRand = Random.Range(4, 6); //tycker vi senare ska ha denna på ett annat sätt
        for (int i = 0; i < ammountRand; i++) {
            if (i == 3) {
                roasterPos.y = 180;
                roasterPos.x += 700;
            }
            characterO = Instantiate(character, roasterPos, Quaternion.identity);
            characterO.GetComponent<CharacterScript>().id = "hire" + i;
            roasterPos.y -= 200;
            GetEquipment();
            WorldScript.world.charBarrackPepList.Add(characterO.GetComponent<CharacterScript>());
            WorldScript.world.staBarrackPepList.Add(characterO.GetComponent<Stats>());
            //Destroy(characterO);
        }
    } private void LoadBarrack() {
        for (int i = 0; i < WorldScript.world.charBarrackPepList.Count; i++) { //För varje laddad karaktär ska den spawna
            if (i == 3) { //Kollar om den ska flytta så att den ska flytta där rutan hamnar i x-led
                roasterPos.y = 180;
                roasterPos.x += 700;
            }
            characterO = Instantiate(character, roasterPos, Quaternion.identity); //Skapar själva karaktären
            roasterPos.y -= 200; //Ser till att nästa som spawnar hamnar under

            characterO.GetComponent<CharacterScript>().LoadPlayer(WorldScript.world.charBarrackPepList[i]); //sätter så att characterScript är detsamma
            characterO.GetComponent<Stats>().LoadPlayer(WorldScript.world.staBarrackPepList[i]); //Sätter så att stats är detsamma
        }
    }
    private void PrepareLists() {
        startWeaponList = new List<WeaponObject>();
        startClothList = new List<ClothItemObject>();
        startHeadList = new List<ClothItemObject>();

        foreach (WeaponObject weapon in Assets.assets.weaponTemp) {
            if (weapon.wpLevel <= 0) {
                startWeaponList.Add(weapon);
            }
        }

        foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
            if (cloth.clothCategory == ClothScript.ClothCategory.StartGear) {
                switch (cloth.clothType) {
                    case ClothScript.ClothType.Cloth:
                        startClothList.Add(cloth);
                        break;
                    case ClothScript.ClothType.HeadGear:
                        startHeadList.Add(cloth);
                        break;
                    default:
                        Debug.Log("Item '" + cloth.name + "' don't have a clothType");
                        break;
                }
            }
        }
    }

    private void GetEquipment() {
        if (startWeaponList.Count > 0) {
            rand = Random.Range(0, startWeaponList.Count);
            characterO.GetComponent<CharacterScript>().rangedId = startWeaponList[rand].name;
        }

        if (startClothList.Count > 0) {
            rand = Random.Range(0, startClothList.Count);
            characterO.GetComponent<PortraitScript>().ChangeCloth(startClothList[rand]);
        }

        if (startHeadList.Count > 0) {
            rand = Random.Range(0, startHeadList.Count);
            characterO.GetComponent<PortraitScript>().ChangeCloth(startHeadList[rand]);
        }
    }

}
