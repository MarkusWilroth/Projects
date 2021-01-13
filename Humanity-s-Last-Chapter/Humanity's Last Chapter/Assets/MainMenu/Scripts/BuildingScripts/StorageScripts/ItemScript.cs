using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    bool held = false;
    bool active = false;
    Vector3 slotPosition;
    public string ItemID;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButtonDown(0) && !held) //If picking up this
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
                {
                    if (!held)
                    {
                        held = true;
                        MouseData.HoldItem(gameObject);
                    }
                }
            }
            else if (Input.GetMouseButton(0) && held) //If holding onto this
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gameObject.GetComponent<Transform>().position = mousePosition;
            }
            else if (held) //If this was let go
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                List<GameObject> items = new List<GameObject>(GameObject.FindGameObjectsWithTag("Item"));
                foreach (GameObject item in items)
                {
                    if (item != gameObject && item.GetComponent<Collider2D>().OverlapPoint(mousePosition))
                    {
                        SwitchPositions(gameObject, item);
                        break;
                    }
                }

                held = false;
                gameObject.GetComponent<Transform>().position = slotPosition;
                MouseData.RemoveItem();
            }
        }
        if (!held)
        {
            if (transform.position != slotPosition)
                gameObject.GetComponent<Transform>().position = slotPosition; //varför inte bara transform.position = slotPosition?
        }
    }

    public Vector3 SlotPositon()
    {
        return slotPosition;
    }

    public void SetSlot(Vector3 SlotPosition)
    {
        slotPosition = SlotPosition;
        gameObject.GetComponent<Transform>().position = slotPosition;
    }

    public void SetColor(Color newColor)
    {
        gameObject.GetComponent<Image>().color = newColor;
    }

    public bool IsActive()
    {
        return active;
    }

    public void SetInactive()
    {
        active = false;
    }

    public void CreateItem(string itemID)
    {
        SetColor(new Color(0.4f, 0.4f, 0.4f, 1f));
        ItemID = itemID;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = itemID;
        active = true;
    }

    public void SwitchPositions(GameObject droppedItem, GameObject onItem)
    {
        //Make sure parents aren't already the same.
        if (droppedItem.transform.parent != onItem.transform.parent)
        {
            //Save parents
            Transform droppedParent = droppedItem.transform.parent;
            Transform onParent = onItem.transform.parent;
            //Transfer parents
            droppedItem.transform.SetParent(onParent, true);
            onItem.transform.SetParent(droppedParent, true);
        }
        //Save positions
        Vector2 droppedItemPosition = droppedItem.GetComponent<ItemScript>().SlotPositon();
        Vector3 onItemPosition = onItem.GetComponent<ItemScript>().SlotPositon();
        //Transfer positions
        droppedItem.GetComponent<ItemScript>().SetSlot(onItemPosition);
        onItem.GetComponent<ItemScript>().SetSlot(droppedItemPosition);
    }
}