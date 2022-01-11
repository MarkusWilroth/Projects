using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool isActive, inside;
    public string itemName, itemDescrip;
    public int slotNr;
    public Text txtName, txtDescrip;
    public GameObject Parent;
    public Color defaultColor;

    void Start()
    {
        txtName = GameObject.FindGameObjectWithTag("TextItemName").GetComponent<Text>();
        txtDescrip = GameObject.FindGameObjectWithTag("TextItemDescrip").GetComponent<Text>();
    }

    public void Equip()
    {
        if (isActive)
        {
            isActive = false;
        }
    }

    public void Update()
    {
        //if (GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        //{
        //    if (!isActive)
        //    {
        //        isActive = true;
        //        inside = true;
        //    }
        //}
        //else if (isActive)
        //{
        //    isActive = false;
        //    inside = false;
        //}
    }

    public void SetDefaultColor()
    {
        if(defaultColor != null)
        {
            this.GetComponent<Image>().color = defaultColor;
        }
        else
        {
            this.GetComponent<Image>().color = Color.white;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isActive)
        {
            //Parent.GetComponent<UpdateInfo>().UpdateText(itemName, itemDescrip);
        }
        inside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isActive)
        {
            //Parent.GetComponent<UpdateInfo>().UpdateText("-", "");
        }
        inside = false;
    }

    public void GetItem(string itemName, string itemDescrip, GameObject Parent)
    {
        this.itemName = itemName;
        this.itemDescrip = itemDescrip;
        this.Parent = Parent;
        isActive = true;
    }

    private void OnDestroy() {
        if (transform.tag == "ItemSlot") { //Kollar om den är av typen ItemSlot, Alla ItemSlots i storage har taggen ItemSlots, characters ItemSlots har ett mer specefik tag
            if (transform.childCount > 0) { //Kollar så att det har ett barn/item
                WorldScript.world.storageArr[slotNr] = GetComponentInChildren<ItemInfo>().id; //Sätter så att Storage sparas
            } else {
                WorldScript.world.storageArr[slotNr] = ""; //Om den inte har ett barn är slotten null
            }
        }
        
        
    }
}
