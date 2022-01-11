using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreationWindow : MonoBehaviour {

    public InputField inputWorld;
    public GameObject Character;
    public GameObject UICharacter;
    private GameObject holder, parent;

    private CharacterScript characterScript;
    private Stats stats;
    private GameObject CoinBox;

    private void Start() {
        parent = GameObject.FindGameObjectWithTag("CharacterManager");
        CoinBox = GameObject.FindGameObjectWithTag("CoinBox");
        CoinBox.active = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
        {
            BtnContinue();
        }
    }

    public void BtnContinue() { //Sätter in alla ändringar så att de fungerar i Hubben
        WorldScript.world.saveName = inputWorld.text;       //Sätter SaveName till det man har skrivit in
        characterScript = Character.GetComponent<CharacterScript>();
        stats = Character.GetComponent<Stats>();
        stats.hp = stats.maxHp; //Vissa kläder ändrar maxHp så sätter hp till maxHp (Bordes dock göras i ClothScript)

        holder = Instantiate(UICharacter);
        holder.GetComponent<CharacterScript>().NewCharacter(characterScript.strName, characterScript.clothId, characterScript.headId);
        holder.GetComponent<Stats>().NewCharacter(stats);
        holder.transform.SetParent(parent.transform, false);

        CoinBox.active = true;
        Destroy(gameObject);    //Tar bort fönstret
    }
}
