using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatAbility : MonoBehaviour
{

    public KeyCode BoundKey;
    public CombatType combatType;
    public Sprite defultSprite;
    public GameObject itemTemp, projectile, turret;
    private GameObject Character, pivotCharacter, drawItemTemp;
    private CombatItemObject combatItem;
    private Vector3 mousePos;
    private bool isReady, isPlacing;

    GameObject toDraw;

    public enum CombatType
    {
        placeable,
        projectile
    }

    private void Start()
    {
        drawItemTemp = Instantiate(itemTemp);
        drawItemTemp.SetActive(false);
        toDraw = new GameObject();
        toDraw.AddComponent<SpriteRenderer>();
        toDraw.GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 0.4f);
        toDraw.GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
    }

    public void GetItem(GameObject Character, GameObject pivotCharacter)
    {
        if (Character != null)
        {
            this.Character = Character;
            this.pivotCharacter = pivotCharacter;
            combatItem = Character.GetComponent<Abilities>().combatItem;

            if (combatItem != null) {
                toDraw.GetComponent<SpriteRenderer>().sprite = combatItem.rangeThingy;
                toDraw.transform.localScale = new Vector3(combatItem.placeRange * 2, combatItem.placeRange * 2, 1);
                GetComponent<Image>().sprite = combatItem.sprite;
            } else {
                GetComponent<Image>().sprite = defultSprite;
            }
            
        } else
        {
            GetComponent<Image>().sprite = defultSprite;
        }
        isPlacing = false;
        drawItemTemp.SetActive(false);
    }

    private void Update()
    {
        if (pivotCharacter != null)
            toDraw.transform.position = pivotCharacter.transform.position;

        if (Character != null)
        {
            isReady = Character.GetComponent<Abilities>().combatReady;
            if (!isReady)
            {
                GetComponent<Image>().color = Color.gray;
            } else
            {
                GetComponent<Image>().color = Color.white;
                if (Input.GetKeyDown(BoundKey))
                {
                    Activate();
                }
            }
            if (isPlacing)
            {
                toDraw.SetActive(true);
                PlaceTarget();
            } else
            {
                toDraw.SetActive(false);
            }
        }
    }

    private void Activate()
    {
        if (isReady)
        {
            isPlacing = true;
            drawItemTemp.SetActive(true);
            drawItemTemp.GetComponent<SpriteRenderer>().sprite = combatItem.sprite;
            //Character.GetComponent<Abilities>().combatReady = false;
            //combatItem.AttachedAbility.GetComponent<AbilityScript>().Activate();
        }

    }

    private void PlaceTarget()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        drawItemTemp.transform.position = mousePos;

        if (Vector3.Distance(drawItemTemp.transform.position, pivotCharacter.transform.position) <= combatItem.placeRange)
        {
            drawItemTemp.GetComponent<SpriteRenderer>().color = Color.blue;
            if (Input.GetMouseButtonDown(0))
            {
                Character.GetComponent<Abilities>().combatReady = false;
                isPlacing = false;
                drawItemTemp.SetActive(false);

                //Place Item
                switch (combatItem.combatType)
                {
                    case CombatType.placeable:
                        PlaceObject();
                        break;

                    case CombatType.projectile:
                        ThrowObject();
                        break;
                }
            }
        } else
        {
            drawItemTemp.GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (Input.GetMouseButtonDown(1))
        {
            isPlacing = false;
            drawItemTemp.SetActive(false);
        }
        //Räknar ut avståndet mellan pivotCharacter.transform.position och drwaItemTemp, om det är mindre än combatItem.range kör!
    }

    private void PlaceObject()
    {
        GameObject go = Instantiate(turret);
        go.transform.position = mousePos;
        go.GetComponent<TurretScript>().GetData(combatItem.attachedWeapon);
        //go.GetComponentInChildren<WeaponAttack>().GetData(combatItem.attachedWeapon);
    }

    private void ThrowObject()
    {
        GameObject go = Instantiate(projectile);
        //go.GetComponent<GrenadeScript>().TargetPos = mousePos;
        go.GetComponent<GrenadeScript>().GetData(mousePos, combatItem.damage, combatItem.aoe, combatItem.sprite);
        go.transform.position = pivotCharacter.transform.position;
    }
}

