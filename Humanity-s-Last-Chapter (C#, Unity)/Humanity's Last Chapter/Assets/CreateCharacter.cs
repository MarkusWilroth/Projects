using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCharacter : MonoBehaviour {

    private CharacterScript characterScript;
    private Stats stats;
    private string[] firstNames = { "James", "John", "Robert", "Michael", "William", "David", "Richard", "Charles", "Joseph", "Thomas", "Christopher", "Daniel", "Paul", "Mark", "Donald", "George", "Kenneth", "Steven", "Edward", "Brian"};
    private string[] lastNames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson"};
    
    private string fullName, birthmonth, descBirth;
    private string[] month = { "Januari", "Feburari", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    string[] earlyLife;
    private int[] daysInMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    private int birthday, birthyear, age, rand;
    public int cost;
    private List<string> lifeBook;

    private WeaponObject startWeapon;
    private List<WeaponObject> startWeaponList;

    private ClothItemObject cloth, headGear;
    private List<ClothItemObject> startCloth, startHeadGear;

    private void Start() {
        stats = GetComponent<Stats>();
        characterScript = GetComponent<CharacterScript>();

        startWeaponList = new List<WeaponObject>();
        startCloth = new List<ClothItemObject>();
        startHeadGear = new List<ClothItemObject>();

        CreateStats();
        CreateName();
        CreateBirth();
        CreateStory();
        GetEquipment();

        characterScript.NewCharacter(fullName, cloth.name, headGear.name);
    }

    //Ger karaktären grundläggande stats
    private void CreateStats() {
        stats.maxHp = 150;
        stats.cha = 10;
        stats.def = 10;
        stats.dex = 10;
        stats.Int = 10;
        stats.ldr = 10;
        stats.snt = 100;
        stats.str = 10;
    }   

    //Ger karaktären ett random namn
    private void CreateName() {
        fullName = firstNames[Random.Range(0, firstNames.Length)] + " " + lastNames[Random.Range(0, lastNames.Length)];
    }   

    //Ger karaktären ett födelsedatum
    private void CreateBirth() {    
        rand = Random.Range(0, month.Length);

        birthmonth = month[rand];
        birthday = Random.Range(1, daysInMonth[rand]);
        birthyear = WorldScript.world.year - 50 + Random.Range(0, 30); //Tar året detta utspelas och sen minus 50 för att få så att det yngsta möjliga en karaktär kan vara är 20 år
        age = WorldScript.world.year - birthyear;
    }

    //Ger karaktären en random background story
    private void CreateStory() {
        descBirth = fullName + " was born " + birthday + " " + birthmonth + " " + birthyear + ". ";
        earlyLife = new string[] { fullName + "'s parents died early in his life and he was brought up as an orphan. ", "There was something special about " + fullName + " as he never seemed to belong anywhere. " + fullName + " had it difficult in his early life as he had no firends. " };
        rand = Random.Range(0, earlyLife.Length);
        lifeBook.Add(earlyLife[rand]);

        switch (rand) {
            case 0: //parents died early
                stats.Int += 5;
                stats.snt += 10;
                stats.cha -= 5;
                break;
            case 1: //Mobbad
                stats.maxHp += 50;
                stats.snt -= 20;
                stats.cha -= 10;
                stats.ldr -= 10;
                stats.str += 5;
                stats.def += 5;
                break;
        }
    }

    private void GetEquipment() {
        foreach (WeaponObject weapon in Assets.assets.weaponTemp) {
            if (weapon.wpLevel == 0) {
                startWeaponList.Add(weapon);
            }
        }

        startWeapon = startWeaponList[Random.Range(0, startWeaponList.Count)];

        foreach (ClothItemObject cloth in Assets.assets.clothTemp) {
            if (cloth.clothCategory == ClothScript.ClothCategory.StartGear) {

                switch (cloth.clothType) {
                    case ClothScript.ClothType.Cloth:
                        startCloth.Add(cloth);
                        break;
                    case ClothScript.ClothType.HeadGear:
                        startHeadGear.Add(cloth);
                        break;
                }
            }
        }
        cloth = startCloth[Random.Range(0, startCloth.Count)];
        headGear = startHeadGear[Random.Range(0, startHeadGear.Count)];
    }

    
}
