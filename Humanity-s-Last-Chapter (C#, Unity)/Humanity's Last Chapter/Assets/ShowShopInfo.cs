using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowShopInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public GameObject Shop;
    public Text txtName, txtDesc, txtCost;

    private UpgradeShop shopScript;

    private void Start() {
        shopScript = Shop.GetComponent<UpgradeShop>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        txtName.text = "Upgrade the Shop!";
        if (WorldScript.world.shopUpgrade){
            txtDesc.text = "The shop is being upgraded, it will be finished tomorrow!";
            txtCost.text = "-";
        } else {
            txtDesc.text = "Upgrade the shop to level " + (shopScript.shopLevel + 1) + "! A better shop will provide better items you can buy!";
            txtCost.text = "Cost: " + shopScript.upgradeCost * shopScript.shopLevel;
        }
    }

    public void UpdateText() {
        if (WorldScript.world.shopUpgrade) {
            txtDesc.text = "The shop is being upgraded, it will be finished tomorrow!";
            txtCost.text = "-";
        }
        else {
            txtDesc.text = "Upgrade the shop to level " + (shopScript.shopLevel + 1) + "! A better shop will provide better items you can buy!";
            txtCost.text = "Cost: " + shopScript.upgradeCost * shopScript.shopLevel;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        txtName.text = "";
        txtDesc.text = "";
        txtCost.text = "";
    }

}
