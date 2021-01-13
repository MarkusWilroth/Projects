using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageLoader : MonoBehaviour
{
    public GameObject StoragePrefab;
    public GameObject ItemPrefab;
    List<GameObject> items = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0;x < 81;x++)
            items.Add(Instantiate(ItemPrefab, gameObject.transform.GetChild(0).transform));
        int i = 0;
        int j = 0;
        foreach(GameObject item in items)
        {
            item.GetComponent<ItemScript>().SetSlot(GetComponent<RectTransform>().position + new Vector3(-2.4f + i * 0.6f, 2.5f - j * 0.6f, 0));
            i++;
            if(i > 8)
            {
                i = 0;
                j++;
            }
        }
        for (int x = 0; x < 3; x++)
        {
            //items[x].GetComponent<ItemScript>().SetColor(new Color(Random.value, Random.value, Random.value, 255));
            switch(x)
            {
                case 0:
                    items[x].GetComponent<ItemScript>().CreateItem("Melee");
                    break;
                case 1:
                    items[x].GetComponent<ItemScript>().CreateItem("Pistol");
                    break;
                case 2:
                    items[x].GetComponent<ItemScript>().CreateItem("Shotgun");
                    break;
            }
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    if (GetComponentInChildren<Collider2D>().OverlapPoint(mousePosition))
        //    {
        //        if(MouseData.GetItem() != null)
        //        {
        //            foreach (GameObject item in items)
        //            {
        //                if (item != MouseData.GetItem() && item.GetComponent<Collider2D>().OverlapPoint(mousePosition))
        //                {
        //                    SwitchPositions(MouseData.GetItem(), item);
        //                    Debug.Log("Items switched");
        //                    break;
        //                }
        //            }
        //            Debug.Log("Dropped item in storage");
        //        }
        //    }
        //}
    }

    public void AddItem(GameObject go)
    {
        
    }

    public void SwitchPositions(GameObject droppedItem, GameObject onItem)
    {
        int foundItems = 0;
        foreach (GameObject item in items)
        {
            if(item == onItem || item == droppedItem)
            {
                foundItems++;
            }
        }
        if(foundItems == 2)
        {
            GameObject temp = droppedItem;
            Vector3 onItemPosition = onItem.GetComponent<ItemScript>().SlotPositon();
            int droppedItemIndex = items.IndexOf(droppedItem);
            int onItemIndex = items.IndexOf(onItem);
            onItem.GetComponent<ItemScript>().SetSlot(temp.GetComponent<ItemScript>().SlotPositon());
            temp.GetComponent<ItemScript>().SetSlot(onItemPosition);
            items[droppedItemIndex] = onItem;
            items[onItemIndex] = temp;
        }
    }
}
