using UnityEngine;

public class SpawnWeapon : MonoBehaviour {
    private WeaponObject[] weapons;
    public GameObject Weapon;
    private GameObject WeaponO;
    private int weaponCounter;
    private weaponStats weaponScript;
    
    void Start() {
        //weaponCounter = Directory.GetFiles("Assets/WeaponFolder/").Length/2;
        //weapons = new WeaponObject[weaponCounter];

        //for (int i = 0; i < weaponCounter; i++) {
        //    weapons[i] = (WeaponObject)AssetDatabase.LoadAssetAtPath("Assets/WeaponFolder/wp" + i + ".asset", typeof(WeaponObject));
        //}
        //Debug.Log("weaponsLength: " + weapons.Length);
    }

    public void Spawn(string wpId, GameObject parent) {
        //if(weaponCounter == 0) {
        //    Start();
        //}

        //for (int i = 0; i < weapons.Length; i++) {
        //    if (wpId == weapons[i].name) {
        //        //spawn Weapon
        //        WeaponO = Instantiate(Weapon);
        //        WeaponO.transform.SetParent(parent.transform, false);

        //        //Detta lär ju gå att göra snyggare... ska bara lista ut hur
        //        weaponScript = WeaponO.GetComponent<weaponStats>();
        //        weaponScript.GetData(weapons[i].weaponName, weapons[i].description, weapons[i].range, weapons[i].fireRate, weapons[i].damage, weapons[i].cost, weapons[i].sprite);
        //    }
        //}
    }
}
