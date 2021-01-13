using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopSlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool isActive, inside;
    private string strName, strDesc, itemID;
    private int cost, arrPos;
    private GameObject parent;

    void Start() {
        //txtName = GameObject.FindGameObjectWithTag("TextItemName").GetComponent<Text>();
        //txtDescrip = GameObject.FindGameObjectWithTag("TextItemDescrip").GetComponent<Text>();
    }
    void Update() {
        if (inside && isActive) {
            if (Input.GetMouseButtonDown(0)) {
                if (tag == "StoreSlot") {
                    if (WorldScript.world.gold >= cost) {
                        WorldScript.world.gold -= cost;
                        WorldScript.world.AddItem(itemID, 1);
                        WorldScript.world.shopArr[arrPos] = "";
                        foreach (Transform child in transform) {
                            Destroy(child.gameObject);
                        }
                        isActive = false;
                    }
                }
                if (tag == "ItemSlot") {
                    WorldScript.world.gold += cost;
                    WorldScript.world.AddToStore(itemID, 1);
                    WorldScript.world.storageArr[arrPos] = "";
                    foreach (Transform child in transform) {
                        Destroy(child.gameObject);
                    }
                    isActive = false;
                }
                parent.GetComponent<ShopScript>().UpdateList();
            }
        }
    }

    public void Equip() {
        if (isActive) {
            isActive = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (isActive) {
            parent.GetComponent<ShopScript>().GetInfo(strName, strDesc, cost);
        }
        inside = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (isActive) {
            parent.GetComponent<ShopScript>().ResetInfo();
        }
        inside = false;
    }

    public void GetItem(string strName, string strDesc, string itemID, int cost, int arrPos, GameObject parent) {
        this.strName = strName;
        this.strDesc = strDesc;
        this.itemID = itemID;
        this.cost = cost;
        this.arrPos = arrPos;
        this.parent = parent;
        isActive = true;

        if (tag == "StoreSlot") {
            this.cost += cost / 10;
        } else {
            this.cost -= cost / 10;
        }
    }
}
