using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreation : MonoBehaviour {

    public GameObject Character, Parent;
    private CharacterScript characterScript;
    private List<ClothItemObject> avalibleCloth, avalibleHead;
    private string strName, clothId, headId;
    private int randFirst, randSecond;
    private string[] firstName = { "Fred", "Greg", "Jason", "James", "Robert", "Michael", "David", "William", "Richard", "Joseph", "Thomas", "Charles", "Jake", "Daniel", "Matthew", "Mark", "Andrew", "Joshua" };
    private string[] lastName = { "Smith", "Johnson", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Jackson", "White", "Harris", "Martin", "Thompson", "Robinson", "Lewis", "Walker", "King" };

    private void Start() {
        characterScript = new CharacterScript();
        avalibleCloth = new List<ClothItemObject>();
        avalibleHead = new List<ClothItemObject>();

        GenerateName();
        GetCloth();
    }

    private void GenerateName() {
        randFirst = Random.Range(0, firstName.Length);                  //Tar fram vilket av förnamnen den ska ha
        randSecond = Random.Range(0, lastName.Length);                  //Tar fram vilket av efternamnen den ska ha
        strName = firstName[randFirst] + " " + lastName[randSecond];    //Sätter namnet
    }

    private void GetCloth() {
        foreach (ClothItemObject item in Assets.assets.clothTemp) {
            if (item.clothCategory == ClothScript.ClothCategory.StartGear) {
                switch (item.clothType) {
                    case ClothScript.ClothType.HeadGear:
                        avalibleHead.Add(item);
                        break;
                    case ClothScript.ClothType.Cloth:
                        avalibleCloth.Add(item);
                        break;
                }
            }
        }
    }
}
