using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Recruit : MonoBehaviour {
    GameObject characterManagerO, characterO;
    public GameObject character;
    CharacterScript characterS;
    int characterCounter;
    
    public void RecruitCharacter() {//OBS! med en positionen kommer dom aldrig hamna i närheten av skärmen
                                    //OBSOBS! om du försöker använda det här för att skapa karaktärerna i huben när dom kommer tillbaks från ett mission 
                                            //så kan du istället använda CampScript.CreateCharacter (den kanske måste modifieras lite men det är lättare                         så )
        characterO = Instantiate(character, new Vector2(0, 0), Quaternion.identity) as GameObject;
        characterO.transform.parent = GameObject.Find("CharacterManager").transform; //Lägger in objectet i CharacterManager för att lättar ha koll på de
    }
}
