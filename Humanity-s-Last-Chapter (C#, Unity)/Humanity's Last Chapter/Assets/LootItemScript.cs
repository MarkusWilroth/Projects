using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootItemScript : MonoBehaviour
{
    public GameObject imgItem;
    public Text txtItemName;

    public void GetItem(Sprite image, string itemName)
    {
        imgItem.GetComponent<Image>().sprite = image;
        txtItemName.text = itemName;
    }
}
