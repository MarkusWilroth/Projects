using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour {
    public float offset;
    public GameObject projectile;

    private float testClosest, minRange, timeStamp, closest, rotZ;
    private GameObject projectileO;
    public GameObject closestEnemy;
    private GameObject[] enemies;

    private PlayerAttack playerAttack;
    private WeaponObject wo;

    private void Start() {
        playerAttack = GetComponent<PlayerAttack>();
        //transform.position = new Vector3(0, 0, 1);
    }

    public void GetData(WeaponObject wo) {
        this.wo = wo;
        GetComponent<SpriteRenderer>().sprite = wo.sprite;
        gameObject.name = wo.weaponName;
        //transform.position = Vector2.zero;
    }

    private void Update() {
        timeStamp += Time.deltaTime;
        Rotation();
    }

    public void Rotation() {
        if (closestEnemy != null) {
            if (Vector2.Distance(transform.position, closestEnemy.transform.position) < wo.range) {
                Vector3 difference = closestEnemy.transform.position - transform.position;
                rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                gameObject.transform.parent.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            //gameObject.transform.parent
        }

        //Kan ha en ifstats eller bool som säger om den endast ska sikta mot den om den är i range
    }

    //For tha nöds
    public bool InCombat(float aggroRange) {
        if (timeStamp >= wo.fireRate) {
            GetClosestEnemy();
            if (closest <= aggroRange) {
                timeStamp = 0;
                return true;
            }
        }
        return false;
    }

    //For tha nöds
    public bool IsRange() {
        GetClosestEnemy();
        if (closest <= wo.range /*&& closest >= minRange*/) { //Min range är avståndet då den byter till melee attack
            return true;
        }
        return false;
    }

    public void GetClosestEnemy() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        closest = Mathf.Infinity;

        foreach (GameObject enemy in enemies) {
            testClosest = (Vector3.Distance(transform.position, enemy.transform.position));

            if (testClosest < closest) {
                closest = testClosest;
                closestEnemy = enemy;
            }
        }
    }

    public void CreateBullet() {
        //if (closestEnemy == null)
        //    return;
        SoundManagerScript.PlaySound(wo.soundEffect);
        for (int i = 0; i < wo.bullets; i++) {
            projectileO = Instantiate(projectile, transform.position, Quaternion.identity);
            projectileO.GetComponent<BulletM>().CreateProjectile(wo, closestEnemy);
        }
    }
}
