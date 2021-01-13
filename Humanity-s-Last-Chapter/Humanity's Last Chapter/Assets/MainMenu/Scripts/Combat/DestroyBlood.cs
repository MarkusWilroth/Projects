using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlood : MonoBehaviour {
    private float bloodTimer = 1.0f;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        bloodTimer -= Time.deltaTime;
        if(bloodTimer <= 0) {
            Destroy(gameObject);
        }
    }
}
