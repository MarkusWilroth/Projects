using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clickAndDrag : MonoBehaviour
{
    Vector3 mousePos, lastMousePos, lastValidMousePos;
    bool held = false, firstRun = true;
    GameObject[] UIElementList;
    GameObject parentUI;
    GameObject UIElement;
    public string CharacterID = "DEFAULT";
    public GameObject characterScrollHolder;
    // Start is called before the first frame update
    void Start()
    {
        parentUI = GameObject.Find("ImageForCharacters");
    }
    private void Awake()
    {
        UIElementList = GameObject.FindGameObjectsWithTag("FormationUiItem");
    }
    // Update is called once per frame
    public void SetUIElement()
    {
        if (!CharacterID.Equals("DEFAULT"))
        {
            CharacterScript[] pcUI = characterScrollHolder.GetComponentsInChildren<CharacterScript>();
            for (int i = 0; i < characterScrollHolder.transform.childCount; i++)
            {
                if (pcUI[i].id.Equals(CharacterID))
                {
                    UIElement = pcUI[i].gameObject;
                }
            }
        }
    }
    void Update()
    {
        SetUIElement();
        if (!parentUI.GetComponent<BoxCollider2D>().OverlapPoint(mousePos) && held)
        {
            held = false;
            transform.position = lastValidMousePos;
            return;
        }
        lastMousePos = mousePos;
        mousePos = Input.mousePosition;
        //när man hoverar över en karaktärs cirkel så blir den gul, när man klickar, håller inne och flyttra musen så följer den efter
        if (gameObject.GetComponent<CircleCollider2D>().OverlapPoint(mousePos) || gameObject.GetComponent<CircleCollider2D>().OverlapPoint(lastMousePos))
        {
            if (!CharacterID.Equals("DEFAULT"))
            {
                UIElement.GetComponent<Image>().color = Color.green;
            }
            gameObject.GetComponent<Image>().color = Color.gray;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                bool anyheld = false;
                foreach(GameObject go in UIElementList)
                {
                    if (go.GetComponent<clickAndDrag>().held)
                    {
                        anyheld = true;
                    }
                }
                if (!anyheld)
                    held = true;
            }
            if (Input.GetKey(KeyCode.Mouse0) && held)
            {
                transform.position = mousePos;
                if (parentUI.GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
                    lastValidMousePos = mousePos;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                held = false;
            }

        } else
        {
            if (!CharacterID.Equals("DEFAULT"))
            {
                UIElement.GetComponent<Image>().color = Color.white;
            }
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
