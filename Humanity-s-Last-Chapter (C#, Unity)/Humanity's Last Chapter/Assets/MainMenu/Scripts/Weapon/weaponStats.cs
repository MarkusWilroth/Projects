using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponStats : MonoBehaviour { //Ännu en klass med liten bokstav...
    public string weaponName = "Weapon name Here";
    public string description = "Info about the weapon";
    public int cost, damage;
    public float fireRate, range, timeStamp, distEnemy;
    public Sprite sprite;
    private Vector3 direction;
    private Vector2 target;

    private GameObject[] enemies;
    private GameObject enemy, bulletO;
    public GameObject Bullet;
    private BulletScript bulletScript;
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        timeStamp += Time.deltaTime;

        if (fireRate <= timeStamp) {
            enemy = GetClosestEnemy();
            distEnemy = Vector2.Distance(transform.position, enemy.transform.position);

            if (distEnemy <= range) {
                target = enemy.transform.position;

                direction = enemy.transform.position - transform.position;
                bulletO = Instantiate(Bullet);
                bulletO.transform.SetParent(transform, false);
                bulletScript = bulletO.GetComponent<BulletScript>();
                bulletScript.damage = damage;
                bulletScript.BulletLand(target, 100);

                timeStamp = 0;
            }
        }
    }
    public void GetData(WeaponObject wpo) {
        weaponName = wpo.weaponName;
        description = wpo.description;
        range = wpo.range;
        fireRate = wpo.fireRate;
        damage = wpo.damage;
        cost = wpo.cost;
        sprite = wpo.sprite;
        GetComponent<SpriteRenderer>().sprite = wpo.sprite;
    }
                

    public GameObject GetClosestEnemy() { //Måste se så att fienden är in line of sight också!
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject go in enemies) {
            Vector2 diff = go.transform.position - transform.position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
