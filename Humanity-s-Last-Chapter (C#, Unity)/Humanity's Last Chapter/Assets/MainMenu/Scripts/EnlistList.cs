using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlistList : MonoBehaviour {

    private GameObject[] characters;
    private GameObject Holder;
    public GameObject UIRole;
    private CharacterScript characterScript;
    bool stop;

    public void Start()
    {
        stop = false;
    }
    public void IsEnlisted(int roleID) {
        //characters = GameObject.FindGameObjectsWithTag("Character");
        //if (characters.Length == 0)
        //{
        //    return;
        //}
        //foreach (GameObject character in characters) {
        //    characterScript = character.GetComponent<CharacterScript>();

        //    //If missionReady and !inHospital
        //    if(!characterScript.isEnlisted && !characterScript.inHospital && stop == false) {
                
        //        Holder = Instantiate(UIRole);
        //        Holder.GetComponent<UICharacterRole>().GetData(characterScript.strName, characterScript.id, roleID);
        //        Holder.transform.SetParent(transform, false);
        //    }
        //}
        //stop = true;
    }

    public void DestroyChildren() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        stop = false;
    }
}
