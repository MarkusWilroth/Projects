using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public GameObject lootBox;
    private GameObject holder;
    private string strName;
    public string[] inventory;
    public int lootLevel;

    public void GetInventory(string[] inventory, string strName)
    {
        this.inventory = inventory;
        this.strName = strName;
        holder = Instantiate(lootBox);
        holder.transform.position = transform.position;
        holder.GetComponent<LootScript>().GetItems(inventory, strName);
        holder.name = strName + "'s Loot";
    }

    void OnDestroy() //Den kommer hit när man stänger av...
    {
        
    }
}
