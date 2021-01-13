using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HealingItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public bool held, inside;
    public string id;
    public string itemDescrip, itemName;
    private int healPower, slotNr;
    private Vector2 mousePos;
    private GameObject[] ItemSlots;
    private ItemSlotScript[] itemSlotList;
    private Rect slotRect, testRect;
    private Transform moveParent;


    public void GetData(HealingItemObject hio, GameObject[] ItemSlots, int slotNr) {
        GetComponent<Image>().sprite = hio.texture;
        itemName = hio.itemName;
        id = hio.name;
        healPower = hio.healPower;
        itemDescrip = hio.description;
        this.ItemSlots = ItemSlots;
        held = false;
        slotRect = GetComponent<Image>().sprite.rect;
        this.slotNr = slotNr;
        moveParent = GameObject.FindGameObjectWithTag("LoadManager").transform;
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (held) {
            gameObject.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0)) {
                Place();
            }
        }
        else {
            if (Input.GetMouseButtonDown(0) && inside) {
                held = true;
                transform.SetParent(moveParent, false);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        inside = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        inside = false;
    }

    public void Place() {
        held = false;
        for (int i = 0; i < ItemSlots.Length; i++) {
            
            if (ItemSlots[i].GetComponent<ItemSlotScript>().inside) {
                Debug.Log("Slot["+i+"]: " + WorldScript.world.storageArr[i]);
                if(WorldScript.world.storageArr[i] == null || WorldScript.world.storageArr[i] == "") {
                    WorldScript.world.MoveItem(slotNr, i, id);
                    Debug.Log("Out");
                    slotNr = i;

                } else {
                    Debug.Log("Slot[" + i + "] is taken");
                    break;
                }
                
            }
        }
        gameObject.transform.SetParent(ItemSlots[slotNr].transform, false);
        transform.position = transform.parent.position;
    }
}
