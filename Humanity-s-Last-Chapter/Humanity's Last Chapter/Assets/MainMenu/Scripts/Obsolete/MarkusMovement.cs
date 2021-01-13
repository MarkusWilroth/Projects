using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkusMovement : MonoBehaviour {
    Vector2 targetPos;
    GameObject Commander;

    private void Start() {
        
    }

    public void GetStartPos() {
        Commander = GameObject.FindGameObjectWithTag("Character");
        transform.position = Commander.transform.position;
        targetPos = transform.position;
    }

    private void Update() {
        if(Commander == null) {
            GetStartPos();
        }

        if(Input.GetMouseButtonDown(0)) {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * 1);
    }
}
