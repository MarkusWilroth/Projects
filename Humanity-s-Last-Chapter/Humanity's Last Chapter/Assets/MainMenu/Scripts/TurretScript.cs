using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class TurretScript : MonoBehaviour {
    public float range;
    public int dmg;
    public float attackTimer;
    public float fireRate;
    GameObject projectile;
    public GameObject Projectile;
    public LayerMask whatIsEnemy;
    RaycastHit2D ray;
    GameObject[] enemies;
    GameObject closestEnemy;
    float offsetRot = 270;
    float StartTimer = 15;
    float timer = 0;
    Transform timerUi;
    Vector3 timerLocalScale = Vector3.zero;
    RootNode Bt;
    // Start is called before the first frame update
    void Start() {
        Bt = GetComponent<BehaviourTree>().GetTurretBt();
        timerUi = transform.GetChild(1);
        timerLocalScale = timerUi.localScale;
    }

    public void GetData(WeaponObject weapon) {
        dmg = weapon.damage;
        range = weapon.range;
        fireRate = weapon.fireRate;
    }

    // Update is called once per frame
    void Update() {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        if (Bt != null)
            Bt.Start();
        timerLocalScale.x = (StartTimer - timer) / StartTimer;
        timerUi.localScale = 0.5f * timerLocalScale;
        timer += Time.deltaTime;
        if (timer >= StartTimer)
            Destroy(gameObject, 0);
    }
    public NodeStates InRangeAndSight() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = Mathf.Infinity;
        if (enemies.Length < 1)
            return NodeStates.fail;
        foreach (GameObject enemy in enemies) {
            float testClosest = (Vector3.Distance(transform.position, enemy.transform.position));

            if (testClosest < closest) {
                closest = testClosest;
                closestEnemy = enemy;
            }
        }
        ray = Physics2D.Raycast(transform.position, closestEnemy.transform.position - transform.position);
        if (!(Vector3.Distance(closestEnemy.transform.position,transform.position) < range))
            return NodeStates.fail;
        if (!ray)
            return NodeStates.fail;
        else if (ray.collider.tag.Equals("Enemy")) {
            Rotate();
            Debug.DrawLine(transform.position, ray.point);
            return NodeStates.success;
        }

        return NodeStates.fail;
    }

    public NodeStates Shoot() {
        if (attackTimer > 0)
            return NodeStates.fail;
        projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        projectile.transform.localScale = new Vector3(0.3f, 0.5f, 1);
        projectile.GetComponent<Projectile>().CreateProjectile(0f);
        projectile.GetComponent<Projectile>().SetTargetPos(closestEnemy.transform.position);
        projectile.GetComponent<Projectile>().whatIsSolid = whatIsEnemy;
        attackTimer = fireRate;
        return NodeStates.success;
    }

    public void Rotate() {
        float rotZ;
        Vector3 difference = closestEnemy.transform.position - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        gameObject.transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, rotZ + offsetRot);
    }

}
