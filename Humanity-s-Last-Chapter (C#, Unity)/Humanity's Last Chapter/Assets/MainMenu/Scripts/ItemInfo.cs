using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler { //Markus - Används för att enkelt få data från objectet oberoend från vilken sorts object det är

    private Text itemName, itemDesc;
    public string strName, strDesc, id;
    public int cost;
    public Sprite icon;

    private void Start() {
        //itemName = GameObject.FindGameObjectWithTag("TextItemName").GetComponent<Text>();
        //itemDesc = GameObject.FindGameObjectWithTag("TextItemDescrip").GetComponent<Text>();
    }

    public void GetData(Sprite icon, string strName, string strDesc, string id, int cost, Text itemName, Text itemDesc) {
        this.icon = icon;
        this.strName = strName;
        this.strDesc = strDesc;
        this.id = id;
        this.cost = cost;
        this.itemName = GameObject.FindGameObjectWithTag("TextItemName").GetComponent<Text>();
        this.itemDesc = GameObject.FindGameObjectWithTag("TextItemDescrip").GetComponent<Text>();
        //Debug.Log("itemDesc: " + itemDesc);
        GetComponent<Image>().sprite = icon;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //itemName.text = strName;
        //itemDesc.text = strDesc;
        //if (itemName != null && itemDesc != null) {
        //    itemName.text = strName;
        //    itemDesc.text = strDesc;
        //} else {
        //    Debug.Log("Can't find textBox!");
        //}
    }

    public void OnPointerExit(PointerEventData eventData) {
        //itemName.text = "";
        //itemDesc.text = "";
        //if (itemName != null && itemDesc != null) {
        //    itemName.text = "";
        //    itemDesc.text = "";
        //} else {
        //    Debug.Log("Can't find textBox!");
        //}
    }

    public void OnMouseOver() {
        itemName.text = strName;
        itemDesc.text = strDesc;
    }

    public void OnMouseExit() {
        itemName.text = "";
        itemDesc.text = "";
    }
}
