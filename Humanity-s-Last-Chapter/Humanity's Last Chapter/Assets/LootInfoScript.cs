using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootInfoScript : MonoBehaviour
{
    public GameObject lootItem;
    private GameObject holder;
    private Sprite itemSprite;
    public string itemName;
    private string[] inventory;
    public Text txtHeader;

    public void GetData(string strName, string[] inventory)
    {
        txtHeader.text = strName + " - Loot";
        this.inventory = inventory;

        foreach (string item in inventory)
        {
            FindItem(item);

            holder = Instantiate(lootItem);
            holder.GetComponent<LootItemScript>().GetItem(itemSprite, itemName);
        }
    }

    private void FindItem(string itemID)
    {
        switch (itemID[0])
        {
            case 'h':
                foreach (HealingItemObject hi in Assets.assets.healingTemp)
                {
                    if (hi.name == itemID)
                    {
                        itemSprite = hi.icon;
                        itemName = hi.itemName;
                    }
                }
                break;
            case 'c':
                foreach (CombatItemObject ci in Assets.assets.combatTemp)
                {
                    if (ci.name == itemID)
                    {
                        itemSprite = ci.sprite;
                        itemName = ci.itemName;
                    }
                }
                break;
            case 'w':
                foreach (WeaponObject wp in Assets.assets.weaponTemp)
                {
                    if (wp.name == itemID)
                    {
                        itemSprite = wp.sprite;
                        itemName = wp.weaponName;
                    }
                }
                break;
        }
    }
}
