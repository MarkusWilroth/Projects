using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class TurretAttack : MonoBehaviour {

    private WeaponAttack weaponAttack;
    private RootNode Bt;
    private GameObject[] enemies;
    private GameObject closestEnemy;

    void Start() {
        Bt = GetComponent<BehaviourTree>().GetTurretBt();
        weaponAttack = GetComponentInChildren<WeaponAttack>();
    }

    public NodeStates InRangeAndSight() {
        if (weaponAttack != null) {
            if (weaponAttack.IsRange()) {
                return NodeStates.success;
            }
        } else {
            weaponAttack = GetComponentInChildren<WeaponAttack>();
        }
        return NodeStates.fail;
    }

    public NodeStates Shoot() {
        weaponAttack.CreateBullet();
        return NodeStates.fail;
    }
}
