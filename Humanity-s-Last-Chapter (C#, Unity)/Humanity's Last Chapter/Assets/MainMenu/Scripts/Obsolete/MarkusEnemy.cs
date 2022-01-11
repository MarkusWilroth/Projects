using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy1 : MonoBehaviour {

    public string id;
    public int hp;

    private void OnMouseOver() {
        if(Input.GetMouseButton(0)) {
            TakeDamage();
        }
    }
    public void TakeDamage() {
        //Debug.Log(gameObject.name + " is clicked!");
        hp -= 5;
        
        if(hp <= 0) {
            Destroy(gameObject);
        }
    }
}
