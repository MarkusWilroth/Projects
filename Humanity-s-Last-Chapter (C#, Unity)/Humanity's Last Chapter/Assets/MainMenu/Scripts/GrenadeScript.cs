using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {
    public Vector3 TargetPos;
    Vector3 direction;
    public int damage = 40;
    public float aoe = 6f; //Area of Effect (Hur stor smäll)
    private int speed = 4;
    bool explodeOnNext = false;
    GameObject[] enemies;

    public GameObject Explosion;
    // Start is called before the first frame update
    void Start() {
        direction = TargetPos - transform.position;
        direction.Normalize();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void GetData(Vector3 targetPos, int damage, float aoe, Sprite sprite) {
        this.TargetPos = targetPos;
        this.damage = damage;
        this.aoe = aoe;
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    // Update is called once per frame
    void Update() {
        if (explodeOnNext) {
            Explode();
            Destroy(gameObject);
        }
        if (Vector2.Distance(transform.position, TargetPos) < 0.5f)
        {
            GetComponent<CircleCollider2D>().radius = aoe;
            explodeOnNext = true;
        }
        transform.position += direction * speed * Time.deltaTime;
    }
    void Explode() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null) {
            foreach (GameObject enemy in enemies) {
                if (GetComponent<CircleCollider2D>().OverlapPoint(enemy.transform.position)) {
                    enemy.GetComponent<SpriteRenderer>().color = Color.green;
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                }
            }
        }

        GameObject go = Instantiate(Explosion);
        go.transform.position = TargetPos;
        go.transform.localScale = new Vector3(0.25f,0.25f,1);
        //go.transform.localScale = new Vector3(aoe, aoe, 1);
    }
}
