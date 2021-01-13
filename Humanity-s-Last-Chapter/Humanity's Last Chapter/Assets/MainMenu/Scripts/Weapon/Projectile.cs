using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float spread;
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;

    private Transform enemy;
    private Vector3 targetPos;
    private Vector3 direction;

    public GameObject character;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("skott på väg");
        enemy = GetNearestTarget().transform;
        targetPos = new Vector3(targetPos.x + Random.Range(-spread, spread), targetPos.y + Random.Range(-spread, spread), targetPos.z);
        //transform.rotation = Quaternion.LookRotation(targetPos);
        direction = targetPos - transform.position;
        Invoke("DestroyProjectile", lifeTime);
    }

    public void SetTargetPos(Vector3 v)
    {
        targetPos = v;
    }

    public void CreateProjectile(float spread)
    {
        this.spread = spread;
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemy != null)
        //{
        //    if (enemy.GetComponent<BoxCollider2D>().OverlapPoint(transform.position))
        //    {
        //        //Debug.Log("skaträffas");
        //        enemy.GetComponent<Enemy>().TakeDamage(damage);
        //        DestroyProjectile();
        //    }
        //}
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction/*targetPos*/, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo != false/*hitInfo.collider.CompareTag("Enemy")*/)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                } else
                {
                    hitInfo.collider.GetComponent<Stats>().TakeDamage(damage);
                    Debug.Log("ska ta dmg");
                }
            }
            DestroyProjectile();
        }

        //velocity = targetPos - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    void DestroyProjectile()
    {
        //Instantiate(destroyEffect, transform.position, Quaternion.identity); //to make a visual when bullet collides maybe?
        Destroy(gameObject);
    }

    public bool LayerMaskContains(LayerMask lm, int layer)
    {
        return lm == (lm | (1 << layer));
    }

    GameObject GetNearestTarget()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Aggregate((o1, o2) => Vector3.Distance(o1.transform.position, this.transform.position) > Vector3.Distance(o2.transform.position, this.transform.position) ? o2 : o1);
    }
}
