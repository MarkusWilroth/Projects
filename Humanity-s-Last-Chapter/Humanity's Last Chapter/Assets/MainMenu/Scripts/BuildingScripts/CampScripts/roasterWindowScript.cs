using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roasterWindowScript : MonoBehaviour {
    private GameObject CharacterManager, characterO;
    public GameObject characterUI;
    public Text txtHealth, txtName;
    private int childCounter;
    private Vector2 characterPos;

    void Start() {
        txtHealth.text = GetComponent<CharacterScript>().strName;
        txtName.text = "Health: " + GetComponent<Stats>().hp.ToString();
    }
    
    public void btnHire() {
        CharacterManager = GameObject.FindGameObjectWithTag("CharacterManager");
        childCounter = CharacterManager.transform.childCount;
        characterPos = new Vector2(0, 210 - (105 * childCounter));
        characterO = Instantiate(characterUI, characterPos, Quaternion.identity);
        characterO.transform.SetParent(CharacterManager.transform, false);

        //characterO.GetComponent<Button>().onClick.AddListener(characterO.GetComponent<AddToPlayerRoster>().AddToPlayer);
        ////characterO.GetComponentInChildren<Text>().text = characterO.GetComponent<CharacterScript>().name;
        ////GameObject actualParent = GameObject.Find("forCamp");
        //characterO.GetComponent<AddToPlayerRoster>().UIParent = CharacterManager;
        //GameObject[] controllers = GameObject.FindGameObjectsWithTag("Controller");
        //for (int j = 0; j < controllers.Length; j++) {
        //    controllers[j].GetComponent<HubCharController>().AddToRoster(characterO);
        //}
        //characterO.GetComponent<AddToPlayerRoster>().owned = true;
        //characterO.GetComponent<AddToPlayerRoster>().controller = controllers[0];
        Destroy(gameObject);

        //Borde få nytt ID här också!
    }
}
