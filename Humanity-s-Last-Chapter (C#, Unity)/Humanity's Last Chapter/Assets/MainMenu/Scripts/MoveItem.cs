using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string id;
    private int slotNr, newSlotNr;

    private bool inside, held;
    private Vector2 mousePos;
    private GameObject moveParent;
    private Transform oldParent;
    private ItemInfo itemInfo;
    private GameObject[] ItemSlots, WeaponSlots, ClothSlots, InventorySlots;

    void Start()
    {
        //moveParent = GameObject.FindGameObjectWithTag("LoadManager").transform;
        oldParent = transform.parent;
        InventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        ItemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        WeaponSlots = GameObject.FindGameObjectsWithTag("WeaponSlot");
        ClothSlots = GameObject.FindGameObjectsWithTag("ClothSlot");

        moveParent = GameObject.FindGameObjectWithTag("StorageMoveParent");
        slotNr = GetComponentInParent<ItemSlotScript>().slotNr;
        itemInfo = GetComponent<ItemInfo>();
        id = GetComponent<ItemInfo>().id;
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (held)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (oldParent.GetComponent<ItemSlotScript>().inside)
                {
                    oldParent.GetComponent<ItemSlotScript>().SetDefaultColor();
                    moveParent.GetComponent<ItemGrabberScript>().Release();
                    held = false;
                }
                else
                {
                    Place();
                }
            }
        }
        else
        {
            if (!moveParent.GetComponent<ItemGrabberScript>().holding)
            {
                if (Input.GetMouseButtonDown(0) && GetComponent<Collider2D>().OverlapPoint(mousePos)) //inside) {
                {
                    oldParent.GetComponent<Image>().color = Color.gray;
                    held = true;
                    moveParent.GetComponent<ItemGrabberScript>().NewItem(this.GetComponent<Image>().sprite);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inside = false;
    }

    public void Place()
    {


        //Check Inventory Slots
        foreach (GameObject itemSlot in GameObject.FindGameObjectsWithTag("ItemSlot"))
        {
            if (Move(itemSlot, "*"))
            {
                return;
            }
        }
        //Check Weapon Slots
        if (id[0] == 'w')
        {
            foreach (GameObject weaponSlot in GameObject.FindGameObjectsWithTag("WeaponSlot"))
            {
                if (Move(weaponSlot, "WeaponSlot"))
                {
                    return;
                }
            }
        }
        //Check Clothes Slots
        else if (id[0] == 'c' && id[1] == 'l')
        { //ClothSlot
            ClothItemObject[] clothTemp = Assets.assets.clothTemp;
            foreach (ClothItemObject cloth in clothTemp)
            {
                if (id == cloth.name)
                {
                    if (cloth.clothType == ClothScript.ClothType.Cloth)
                    {
                        foreach (GameObject ClothSlot in GameObject.FindGameObjectsWithTag("ClothSlot"))
                        {
                            if (Move(ClothSlot, "ClothSlot"))
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        foreach (GameObject hatSlot in GameObject.FindGameObjectsWithTag("HatSlot"))
                        {
                            if (Move(hatSlot, "HatSlot"))
                            {
                                return;
                            }
                        }
                    }
                    break;
                }
            }

        }
        else if (id[0] == 'h')
        {
            foreach (GameObject healSlot in GameObject.FindGameObjectsWithTag("HealSlot"))
            {
                if (Move(healSlot, "HealSlot"))
                {
                    return;
                }
            }
        }
        else if (id[0] == 'c' && id[1] == 'i')
        { //CombatItem
            foreach (GameObject CombatItem in GameObject.FindGameObjectsWithTag("CombatSlot"))
            {
                if (Move(CombatItem, "CombatSlot"))
                {
                    return;
                }
            }
        }
        //Stop holding this item
        held = false;
        moveParent.GetComponent<ItemGrabberScript>().Release();
        oldParent.GetComponent<ItemSlotScript>().SetDefaultColor();
    }
    public void SetOldParent(Transform newOldParent)
    {
        oldParent = newOldParent;
    }

    private bool CheckSlot(Transform item)
    {
        if (oldParent.tag == "ClothSlot" || oldParent.tag == "HatSlot")
        {
            if (id[0] == item.GetComponent<ItemInfo>().id[0] && id[1] == item.GetComponent<ItemInfo>().id[1])
            {
                return true;
            }
        }
        else
        {
            if (id[0] == item.GetComponent<ItemInfo>().id[0])
            {
                return true;
            }
        }
        return false;
    }

    private void Switch(GameObject itemSlot, string type)
    {
        //Move item in the itemSlot to the oldParent.
        foreach (Transform t in itemSlot.transform)
        {
            if (t.tag == "Item")
            {
                if (oldParent.tag != "ItemSlot")
                {
                    if (!CheckSlot(t))
                        return;
                }
               
                //Make oldParent active, because why not
                oldParent.GetComponent<ItemSlotScript>().isActive = true;
                //Set the oldParent-slot to this item's info
                oldParent.GetComponent<ItemSlotScript>().GetItem(t.GetComponent<ItemInfo>().strName, t.GetComponent<ItemInfo>().strDesc, itemSlot.GetComponent<ItemSlotScript>().Parent);
                //Set this item's parent to the oldParent
                t.SetParent(oldParent.transform, false);
                t.GetComponent<MoveItem>().SetOldParent(oldParent);
                //Put the item on the new slot
                transform.position = transform.parent.position;
            }
        }
        held = false;
        moveParent.GetComponent<ItemGrabberScript>().Release();
        oldParent.GetComponent<ItemSlotScript>().SetDefaultColor();
        //Make new slot active
        itemSlot.GetComponent<ItemSlotScript>().isActive = true;
        //Set the slot to this item's info
        itemSlot.GetComponent<ItemSlotScript>().GetItem(itemInfo.strName, itemInfo.strDesc, oldParent.GetComponent<ItemSlotScript>().Parent);
        //Set this item's parent to the new slot
        gameObject.transform.SetParent(itemSlot.transform, false);
        //Set the new slot to this item's "old" parent, I guess...
        oldParent = transform.parent;
        //Put the item on the new slot
        transform.position = transform.parent.position;
        
    }

    private bool Move(GameObject itemSlot, string type)
    {
        bool isMoveable = false;
        if (itemSlot.GetComponent<ItemSlotScript>().inside)
        { //Ifall musen är innanför en ruta
            if (itemSlot.tag == "ItemSlot")
            {
                if (itemSlot.transform.childCount <= 0)
                { //Har inga barn i sig
                    isMoveable = true;
                }
                else
                {
                    Switch(itemSlot, type);
                    return true;
                }
            }
            else if (itemSlot.transform.childCount <= 1)
            { //Har endast text i sig
                isMoveable = true;
            }
            else
            {
                Switch(itemSlot, type);
                return true;
            }


            if (isMoveable)
            {
                
                moveParent.GetComponent<ItemGrabberScript>().Release();
                oldParent.GetComponent<ItemSlotScript>().SetDefaultColor();
                held = false;
                oldParent.GetComponent<ItemSlotScript>().isActive = false;
                //Make new slot active
                itemSlot.GetComponent<ItemSlotScript>().isActive = true;
                //Set the slot this this item's info
                itemSlot.GetComponent<ItemSlotScript>().GetItem(itemInfo.strName, itemInfo.strDesc, oldParent.GetComponent<ItemSlotScript>().Parent);
                //Set this item's parent to the new slot
                gameObject.transform.SetParent(itemSlot.transform, false);
                //Set the new slot to this item's "old" parent, I guess...
                oldParent = transform.parent;
                //Put the item on the new slot
                transform.position = transform.parent.position;
            }
            else
            {
                held = true;
            }
            return true;
        }
        return false;
    }
}
