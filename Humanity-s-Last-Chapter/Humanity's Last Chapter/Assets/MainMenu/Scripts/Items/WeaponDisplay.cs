using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour {
    public string weaponName = "Weapon name Here";
    public string description = "Info about the weapon";
    public int cost, damage;
    public float fireRate, range;
    public Sprite sprite;

    public void GetData(WeaponObject wpo) {
        weaponName = wpo.weaponName;
        description = wpo.description;
        range = wpo.range;
        fireRate = wpo.fireRate;
        damage = wpo.damage;
        cost = wpo.cost;
        sprite = wpo.sprite;
        gameObject.GetComponent<Image>().sprite = sprite;
    }
}
