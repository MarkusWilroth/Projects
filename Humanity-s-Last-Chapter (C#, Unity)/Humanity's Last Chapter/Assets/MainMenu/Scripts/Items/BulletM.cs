using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletM : MonoBehaviour { //Markus
    public float speed; //Hur snabbt skottet rör sig, flyttas till scriptableObject?
    public float lifeTime;
    public float aoe; //Hur stor träffyta?
    private float range; //Hur långt skottet kan flyga
    private float travelDistance; //Hur långt skottet har flygit
    public LayerMask whatIsSolid;

    private Vector3 targetPos; //Positionen målet är på
    private Vector3 aimPos; //Där skottet kommer att åka till
    private Vector3 direction, startPos;
    
    private WeaponObject wo;

    // Start is called before the first frame update
    void Start() {
        aimPos = new Vector3(targetPos.x + Random.Range(-wo.spread, wo.spread), targetPos.y + Random.Range(-wo.spread, wo.spread), 0);
        startPos = transform.position;
        //transform.rotation = Quaternion.LookRotation(targetPos);
        direction.x = aimPos.x - transform.position.x;
        direction.y = aimPos.y - transform.position.y;
        direction.z = aimPos.z;
        //Invoke("DestroyProjectile", lifeTime);
    }

    public void CreateProjectile(WeaponObject wo, GameObject target) {
        this.wo = wo;
        range = wo.range /*+ Random.Range(-wo.spread, wo.spread)*/;
        targetPos.x = target.transform.position.x;
        targetPos.y = target.transform.position.y;
        targetPos.z = 0;
    }

    // Update is called once per frame
    void Update() {
        CheckHit();
        CheckDistance();
        
        //velocity = targetPos - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
    }

    private void CheckHit() { //Kollar om skottet har träffat något
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, aoe, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy")) { //Kommer inte fungera bra om vi ska ha flera "factions"
                //Debug.Log(hitInfo.collider);
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(wo.damage);
            }
            Destroy(gameObject);
        }
    }

    private void CheckDistance() { //Kollar om skottet har flygit tillräckligt långt
        travelDistance = Vector3.Distance(startPos, transform.position);
        //Debug.Log("BulletTravel: " + bulletTravel);
        if (travelDistance >= range + 5) {
            Destroy(gameObject);
        }
    }
}
