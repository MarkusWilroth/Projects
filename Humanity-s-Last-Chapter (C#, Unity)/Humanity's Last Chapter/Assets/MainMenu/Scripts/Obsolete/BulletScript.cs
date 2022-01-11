using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {
    public int damage;
    public float bulletSpeed;
    public Vector2 bulletEndPos;

    void Start() {
        bulletSpeed = 0.1f;
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, bulletEndPos, bulletSpeed);

        if((Vector2)transform.position == bulletEndPos) {
            Destroy(gameObject);
        }
    }

    public void BulletLand(Vector2 targetPos, int accuracy) {
        //Ska använda accurecy för att se om skottet kommer träffa eller missa
        if(Random.Range(0,100) <= accuracy) {
            //Hit
        }
        else {
            //Miss
        }

        bulletEndPos = targetPos;
    }
}
