using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeShop : MonoBehaviour {

    public GameObject updateUpgrade;
    public Text txtLevel;
    public int upgradeCost;
    public int shopLevel;

    void Start() {
        shopLevel = WorldScript.world.shopLevel;
        txtLevel.text = "Shop Level: " + shopLevel;
    }

    public void BtnUpgrade() {
        if (upgradeCost * shopLevel <= WorldScript.world.gold && !WorldScript.world.shopUpgrade) { //Har man tillräckligt med pengar och ingen uppgradering är igång
            WorldScript.world.gold -= upgradeCost * shopLevel;
            WorldScript.world.stockSize += 3;
            WorldScript.world.shopLevel += 1;
            WorldScript.world.shopUpgrade = true;

            shopLevel = WorldScript.world.shopLevel;
            txtLevel.text = "Shop Level: " + shopLevel;
            updateUpgrade.GetComponent<ShowShopInfo>().UpdateText();
        }
        
    }
}
