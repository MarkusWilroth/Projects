using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    public enum MovementState {
        idle,
        attack
    }

    private GameObject[] allCharacters, allNpc;
    private GameObject characterO, npcO;
    private Vector2 targetPos, diff;
    public MovementState movementState;
    
    public int speed;
    private bool isMoving;
    public float movingTimer;
    private float timeStamp, dist, curDistance, characterDist, npcDist;

    void Start() {
        targetPos = new Vector2(transform.position.x, transform.position.y);
        isMoving = false;
        
    }

    // Update is called once per frame
    void Update() {
        timeStamp += Time.deltaTime;
        switch (movementState) {
            case MovementState.idle:
                if(!isMoving) {
                    targetPos.x += Random.Range(-0.5f, 0.5f);
                    targetPos.y += Random.Range(-0.5f, 0.5f);
                    isMoving = true;
                }
                if (movingTimer <= timeStamp) {
                    isMoving = false;
                    timeStamp = 0;
                }
                break;

            case MovementState.attack:
                targetPos = GetClosest();
                break;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public Vector2 GetClosest() {
        //allCharacters = GameObject.FindGameObjectsWithTag("Character");
        //characterO = GetClosestSort(allCharacters);
        //characterDist = Vector2.Distance(transform.position, characterO.transform.position);
        characterDist = Mathf.Infinity;

        allNpc = GameObject.FindGameObjectsWithTag("Faction"); //Taggen borde vara vad factionen heter
        npcO = GetClosestSort(allNpc);
        npcDist = Vector2.Distance(transform.position, npcO.transform.position);

        if (characterDist < npcDist) {
            return characterO.transform.position;
        }
        else {
            return npcO.transform.position;
        }
    }

    public GameObject GetClosestSort(GameObject[] targets) {
        GameObject targetSort = null;
        targetSort = null;
        dist = Mathf.Infinity;

        foreach (GameObject go in targets) {
            diff = go.transform.position - transform.position;
            curDistance = diff.sqrMagnitude;

            if(curDistance < dist) {
                targetSort = go;
                dist = curDistance;
            }
        }
        return targetSort;
    }
}
