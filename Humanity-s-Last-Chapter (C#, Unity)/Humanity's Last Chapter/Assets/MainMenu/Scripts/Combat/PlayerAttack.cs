using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Node;

public class PlayerAttack : MonoBehaviour {

    public int damage;
    public float aggroDistance;
    public float attackRange;
    public LayerMask BuildingsAnEnemies;
    public LayerMask whatIsEnemy;

    private Vector3 enemyPos;
    private RaycastHit2D hitInfo;

    /* Notes:
        Det finns två koder i detta script, toro kod (Vi gör alla vapen i kod) och Markus kod (Vi gör alla vapen som scriptable objects)
        Båda kan inte användas samtidigt! För att byta kod krävs:

        * Här:                      Avmarkera region koden du inte vill använda och markera den i den andra
        * I Party script:           Markera/avmarkera Markus vapen skapande (rad 48 - 61)
        * I BoiGun prefaben:        Markera/avmarkera Test Weapon (Markus Vapen)
        * I Humanityboi prefaben:   Markera/avmarkera Weapon Script (Toros vapen skapande)
         
        Notes från markus: Anledningen till detta är för att visa hur mycket mer effektivt det är att ha Scriptable Objects!
     */

    #region Markus exclusive variabler
    private WeaponAttack tWeapon;
    #endregion

    #region Toro exclusive variabler
    public int loadout;
    public float speed = 1.5f;
    public float timeBetweenAttack;
    public RectTransform attackPos;
    public GameObject projectile;

    private bool walking;
    private float enemyDistance;
    private float attackTimer;
    private string loadoutType = " ";
    private Vector3 destination;
    private Transform enemy;
    private GameObject projectile0;
    #endregion

    //Gemensama funktioner behöver inte ändras för att markus eller toro kod ska fungera
    #region Gemensama funktioner
    public NodeStates LineOfSight() {
        hitInfo = Physics2D.Raycast(transform.position, enemyPos - transform.position, aggroDistance, BuildingsAnEnemies);
        if (!hitInfo.collider)
        {
            return NodeStates.fail;
        } else if (hitInfo.collider.gameObject.CompareTag("Enemy"))
        /*(hitInfo.collider.CompareTag("Enemy"))*/
        {
            //Debug.Log(enemyPos);
            Debug.DrawLine(transform.position, hitInfo.point);
            return NodeStates.success;
        } else
            return NodeStates.fail;
        //return NodeStates.fail;
    }

    public NodeStates MeleeAttack() {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange);
        for (int i = 0; i < enemiesToDamage.Length; i++) {
            if (enemiesToDamage[i].CompareTag("Enemy"))
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
        return NodeStates.success;
    }

    public NodeStates MoveTowarsEnemy() {
        GameObject target = tWeapon.closestEnemy;
        //GetComponent<AIDestinationSetter>().SetPosTarget(target.transform.position);
        return NodeStates.success;
    }

    //public NodeStates MeleeAttack() {
    //    //Debug.Log("melee");
    //    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);
    //}

    public NodeStates WithinAggroRange() {
        return NodeStates.success;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion

    #region Markus Kod
    void Start() {
        tWeapon = GetComponentInChildren<WeaponAttack>();
    }

    public NodeStates IsRanged() {
        if (tWeapon != null) {
            if (tWeapon.IsRange()) {
                return NodeStates.success;
            }
        } else {
            tWeapon = GetComponentInChildren<WeaponAttack>();
            
        }
        return NodeStates.fail;
    }

    public NodeStates InCombatRange() {
        if (tWeapon != null) {
            if (tWeapon.InCombat(aggroDistance)) {
                enemyPos = tWeapon.closestEnemy.transform.position;
                return NodeStates.success;
            }
        }
        return NodeStates.fail;
    }

    public NodeStates RangeAttack() {
        tWeapon = GetComponentInChildren<WeaponAttack>();
        tWeapon.CreateBullet();
        GetComponentInChildren<Animator>().SetTrigger("flash");
        return NodeStates.success;
    }


    #endregion

    #region Toro Kod
    //void Start() {
    //    //destination = transform.position;

    //    foreach (GameObject item in gameObject.GetComponent<stats>().items) {
    //        if (item.GetComponent<ItemScript>().IsActive()) {
    //            switch (item.GetComponent<ItemScript>().ItemID) {
    //                case "Melee":
    //                    loadout = 0;
    //                    break;

    //                case "Pistol":
    //                    loadout = 1;
    //                    break;

    //                case "Shotgun":
    //                    loadout = 2;
    //                    break;
    //            }
    //            break;
    //        }
    //    }
    //    CheckLoadout();
    //}

    //public NodeStates IsRanged() {
    //    if (loadoutType == "Pistol")
    //        return NodeStates.success;
    //    else
    //        return NodeStates.fail;
    //}

    //public NodeStates InCombatRange() {
    //    GameObject go = GetNearestTarget();
    //    if (go.Equals(gameObject)) //Varför har ni så att GetNearestTarget returnar gameObject och inte null när det inte finns några fiender? och sen => if (go != null)
    //    {
    //        return NodeStates.fail;
    //    }

    //    enemy = go.transform;
    //    enemyPos = enemy.position;
    //    enemyDistance = Vector3.Distance(enemyPos, transform.position);
    //    if (attackTimer <= 0) {
    //        if (enemyDistance <= aggroDistance) {
    //            return NodeStates.success;
    //        }
    //    }
    //    return NodeStates.fail;
    //}

    //public NodeStates RangeAttack() {
    //    TestCombat();
    //    return NodeStates.success;
    //}

    //private void TestCombat() {
    //    //enemy = GetNearestTarget().transform;
    //    //enemyPos = enemy.position;
    //    //enemyDistance = Vector3.Distance(enemyPos, transform.position);
    //    //Debug.Log("Distance: " + enemyDistance);
    //    //if (attackTimer <= 0)
    //    //{
    //    //    if (enemyDistance <= aggroDistance)
    //    //    {
    //    //        if (loadoutType == "Melee")
    //    //        {
    //    //            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsEnemy);

    //    //            for (int i = 0; i < enemiesToDamage.Length; i++)
    //    //            {
    //    //                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
    //    //            }
    //    //        }

    //    //    }
    //    //}
    //    if (loadoutType == "Pistol") {
    //        //projectile0.GetComponent<Projectile>().spread = 0f;
    //        projectile0 = Instantiate(projectile, transform.position, Quaternion.identity);
    //        projectile0.GetComponent<Projectile>().CreateProjectile(0f);
    //        //projectile0.GetComponent<Projectile>().character = gameObject;
    //    }
    //    if (loadoutType == "Shotgun") {

    //        for (int i = 0; i < 4; i++) {
    //            projectile0 = Instantiate(projectile, transform.position, Quaternion.identity);
    //            projectile0.GetComponent<Projectile>().CreateProjectile(1f);
    //        }
    //    }
    //    attackTimer = timeBetweenAttack;
    //}

    //void CheckLoadout() {
    //    if (loadout == 0) {
    //        loadoutType = "Melee";
    //        damage = 5;
    //        timeBetweenAttack = 0.5f;
    //    } else if (loadout == 1) {
    //        loadoutType = "Pistol";
    //        damage = 10;
    //        timeBetweenAttack = 1;
    //    } else if (loadout == 2) {
    //        loadoutType = "Shotgun";
    //        damage = 4;
    //        timeBetweenAttack = 3;
    //    }
    //}

    //private void TestMoveToDestination() {
    //    if (Input.GetMouseButtonDown(1)) {
    //        destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        destination.z = transform.position.z;
    //    }
    //    transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    //}

    //void Update() {
    //    //For testing only
    //    //TestMoveToDestination();
    //    //TestCombat();

    //    if (attackTimer > 0)
    //        attackTimer -= Time.deltaTime;
    //}

    //GameObject GetNearestTarget() {
    //    if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
    //        return gameObject;
    //    }

    //    return GameObject.FindGameObjectsWithTag("Enemy").Aggregate((o1, o2) => Vector3.Distance(o1.transform.position, this.transform.position)
    //    > Vector3.Distance(o2.transform.position, this.transform.position) ? o2 : o1);
    //}
    #endregion
}