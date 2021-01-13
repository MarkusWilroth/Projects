using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour {

    private HealingItemObject[] hio;    //Samling av all healingItems
    private CombatItemObject[] cio;     //Samling av alla combatItems
    private WeaponObject[] wpo;         //Samling av alla weapons

    public Text txtDescName, txtDescDesc; //Info rutornas text i StorageWindow

    private GameObject[] shopSlots, itemSlots; //ShopSlots och itemSlots är respektiva slots där items kan finnas
    private GameObject LoadManager, ItemO, ShopParent, InventoryParent, slotO;
    public Text txtGold, txtName, txtDesc, txtCost;
    private string strGold;
    private string[] strShopArr, strInventoryArr;
    private List<Object> itemIDO;

    private ShopSlotScript sss;
    private ItemInfo itemInfo;
    public GameObject ItemSlot, shopItem;
    public int slotsCounter;

    private void Start() {

        AddToID();

        ShopParent = GameObject.FindGameObjectWithTag("ForStoreStorage");
        for (int i = 0; i < slotsCounter; i++) {
            slotO = Instantiate(ItemSlot, ShopParent.transform);
            slotO.tag = "StoreSlot";
        }

        InventoryParent = GameObject.FindGameObjectWithTag("ForItemStorage");
        for (int i = 0; i < WorldScript.world.storageSize; i++) {
            slotO = Instantiate(ItemSlot, InventoryParent.transform);
            slotO.tag = "ItemSlot";
        }

        shopSlots = GameObject.FindGameObjectsWithTag("StoreSlot");
        itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
        LoadManager = GameObject.FindGameObjectWithTag("LoadManager");
        //LoadManager.GetComponent<CreateItems>().FillSlots(WorldScript.world.shopArr, shopSlots);

        txtName.text = "";
        txtDesc.text = "";
        txtCost.text = "";
        txtGold.text = "Gold: " + WorldScript.world.gold;

        strShopArr = WorldScript.world.shopArr;
        strInventoryArr = WorldScript.world.storageArr;
        FillShop(strShopArr, shopSlots, WorldScript.world.shopSize);
        FillShop(strInventoryArr, itemSlots, WorldScript.world.storageSize);
    }

    private void Update() {
        strGold = "Gold: " + WorldScript.world.gold; //Uppdaterar så att txtGold kan rita ut hur mycket guld som finns
        txtGold.text = strGold;

    }
    public void UpdateList() {  //Uppdaterar de olika arrayes som finns
        strShopArr = WorldScript.world.shopArr;
        strInventoryArr = WorldScript.world.storageArr;
        FillShop(strShopArr, shopSlots, WorldScript.world.shopSize);
        FillShop(strInventoryArr, itemSlots, WorldScript.world.storageSize);
    }

    public void GetInfo(string strName, string strDesc, int cost) {
        txtName.text = strName;
        txtDesc.text = strDesc;
        txtCost.text = "Cost: " + cost;
    }

    public void ResetInfo() {
        txtName.text = "";
        txtDesc.text = "";
        txtCost.text = "Cost: ";
    }

    private void AddToID() {
        itemIDO = new List<Object>();
        hio = Assets.assets.healingTemp;
        cio = Assets.assets.combatTemp;
        wpo = Assets.assets.weaponTemp;

        for (int i = 0; i < hio.Length; i++) {
            itemIDO.Add(hio[i]);
        }
        for (int i = 0; i < cio.Length; i++) {
            itemIDO.Add(cio[i]);
        }
        for (int i = 0; i < wpo.Length; i++) {
            itemIDO.Add(wpo[i]);
        }
    }

    private void FillShop(string[] inventory, GameObject[] shopSlots, int size) {
        for (int i = 0; i < size; i++) {
            if(inventory[i] != "" && inventory[i] != null) {
                sss = shopSlots[i].GetComponent<ShopSlotScript>();
                switch (inventory[i][0]) {
                    case 'h':
                        foreach (HealingItemObject healingItem in hio) {
                            if (healingItem.name == inventory[i]) {
                                ItemO = Instantiate(shopItem, shopSlots[i].transform);
                                itemInfo = ItemO.GetComponent<ItemInfo>();
                                itemInfo.GetData(healingItem.icon, healingItem.itemName, healingItem.description, healingItem.name, healingItem.cost, txtDescName, txtDescDesc);
                                sss.GetItem(itemInfo.strName, itemInfo.strDesc, inventory[i], itemInfo.cost, i, gameObject);
                                break;
                            }
                        }
                        break;
                    case 'c':
                        foreach (CombatItemObject combatItem in cio) {
                            if (combatItem.name == inventory[i]) {
                                ItemO = Instantiate(shopItem, shopSlots[i].transform);
                                itemInfo = ItemO.GetComponent<ItemInfo>();
                                itemInfo.GetData(combatItem.icon, combatItem.itemName, combatItem.description, combatItem.name, combatItem.cost, txtDescName, txtDescDesc);
                                sss.GetItem(itemInfo.strName, itemInfo.strDesc, inventory[i], itemInfo.cost, i, gameObject);
                                break;
                            }
                        }
                        break;
                    case 'w':
                        foreach (WeaponObject weapon in wpo) {
                            if (weapon.name == inventory[i]) {
                                ItemO = Instantiate(shopItem, shopSlots[i].transform);
                                itemInfo = ItemO.GetComponent<ItemInfo>();
                                itemInfo.GetData(weapon.icon, weapon.weaponName, weapon.description, weapon.name, weapon.cost, txtDescName, txtDescDesc);
                                sss.GetItem(itemInfo.strName, itemInfo.strDesc, inventory[i], itemInfo.cost, i, gameObject);
                                break;
                            }
                        }
                        break;
                }
                
            }
        }
    }

    public void BtnExit() {
        Destroy(gameObject);
    }
}
