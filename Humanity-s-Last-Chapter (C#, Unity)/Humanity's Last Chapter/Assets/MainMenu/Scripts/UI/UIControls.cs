using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    Vector3 hpBarOffset = new Vector3(-0.255f,0.3f,0);
    public GameObject HealthBar;
    GameObject currentHPBar;

    Vector3 namePlateOffset = new Vector3(-0.255f, 0.45f,0);
    public GameObject namePlate;
    GameObject currentNamePlate;

    GameObject ParentForUiRepresentation;
    public GameObject UiRepresentation;
    public GameObject CurrentUiRepresentation;
    // Start is called before the first frame update
    void Start()
    {
        currentHPBar = Instantiate(HealthBar);
        currentHPBar.GetComponent<HPinCombat>().attachedToPlayer = gameObject;

        currentNamePlate = Instantiate(namePlate);
        currentNamePlate.GetComponent<TextMesh>().text = GetComponent<CharacterScript>().strName;
        currentNamePlate.GetComponent<MeshRenderer>().sortingOrder = 1;

        ParentForUiRepresentation = GameObject.Find("forCharacters");
        //GameObject.Find("CharacterScroll").SetActive(true);
        CurrentUiRepresentation = Instantiate(UiRepresentation);
        CurrentUiRepresentation.transform.SetParent(ParentForUiRepresentation.transform, false);
        CurrentUiRepresentation.GetComponent<CharacterScript>().LoadPlayer(GetComponent<CharacterScript>());
        CurrentUiRepresentation.GetComponent<CharacterScript>().partyMember = GetComponent<CharacterScript>().partyMember;
       CurrentUiRepresentation.GetComponent<Stats>().LoadPlayer(GetComponent<Stats>());
        CurrentUiRepresentation.GetComponent<Abilities>().pivotCharacter = gameObject;

        //CurrentUiRepresentation.GetComponent<UIBoiScript>().GetPos(ParentForUiRepresentation.GetComponentsInChildren<UIBoiScript>().Length);
        CurrentUiRepresentation.GetComponent<Button>().onClick.AddListener(CurrentUiRepresentation.GetComponent<Stats>().BringUpStats);
        //CurrentUiRepresentation.GetComponent<UIBoiScript>().isOwned = true;
        CurrentUiRepresentation.transform.localScale = new Vector3(1,1,1);
        //GetComponent<UpdateUiBoiInMission>().UIBoi = CurrentUiRepresentation;
    }

    // Update is called once per frame
    void Update()
    {
        currentHPBar.transform.position = transform.position + hpBarOffset;
        currentNamePlate.transform.position = transform.position + namePlateOffset;

        if (CurrentUiRepresentation == null)
            Debug.Log("wat");

        Stats statsScript = GetComponent<Stats>();
        if (statsScript != null) {
            CurrentUiRepresentation.GetComponent<Stats>().LoadPlayer(statsScript);
                //CurrentUiRepresentation.GetComponent<UIBoiScript>().SetStats(statsScript);
        }
        CharacterScript characterScript = GetComponent<CharacterScript>();
        if(characterScript != null) {
            CurrentUiRepresentation.GetComponent<CharacterScript>().LoadPlayer(characterScript);
        }


    }
    private void OnDestroy()
    {
        Destroy(currentHPBar);
        Destroy(currentNamePlate);
    }

    private void OnDisable()
    {
        if (!gameObject.activeSelf)
        {
            //GetComponent<Stats>().hp = 5;
            if (CurrentUiRepresentation != null)
                //CurrentUiRepresentation.GetComponent<UIBoiScript>().SetStats(GetComponent<Stats>());
                if (currentHPBar != null)
                    currentHPBar.SetActive(false);
            if (currentNamePlate != null)
                currentNamePlate.SetActive(false);
        }
    }
}
