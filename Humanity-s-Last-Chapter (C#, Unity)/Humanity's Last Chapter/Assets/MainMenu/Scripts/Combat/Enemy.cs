using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class Enemy : MonoBehaviour
{
    public int health;
    private Vector3 target;

    private int lootSize;
    private string lootType;
    private int lootTypeNumber;
    private string[] lootDrop;

    public GameObject[] pcs;
    public float aggroRange;
    public float atkRange;
    public int dmg;
    public LayerMask pcLayer;

    public float attackTimer = 0;
    public float timeBetweenAttack;

    public Animator animator;

    public ParticleSystem bloodEffect;
    public bool hasSeenEnemy = false;

    public GameObject EnemyCorpse;

    // Start is called before the first frame update
    void Start()
    {
        //target = transform.position;
        pcs = GameObject.FindGameObjectsWithTag("Character");
        lootSize = Random.Range(0, 3);
        lootDrop = new string[lootSize];
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject corpse = Instantiate(EnemyCorpse);
            corpse.transform.position = transform.position;
            corpse.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, 180);
            if (lootDrop.Length > 0)
            {
                GetInventory();
            }
            Destroy(gameObject);
        }
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        RemovePcFromList();
    }

    private void LootRandomizer()
    {
        int value = Random.Range(0, 3);

        if (value == 0)
        {
            lootTypeNumber = Random.Range(0, 2);
            lootType = "hi" + lootTypeNumber;
        }
        else if (value == 1)
        {
            lootTypeNumber = Random.Range(0, 4);
            lootType = "ci" + lootTypeNumber;
        }
        else if (value == 2)
        {
            if(GetComponent<InventoryScript>().lootLevel == 0)
            {
                lootTypeNumber = Random.Range(0, 4);
            }
            if (GetComponent<InventoryScript>().lootLevel == 1)
            {
                lootTypeNumber = Random.Range(0, 6);
            }
            if (GetComponent<InventoryScript>().lootLevel == 2)
            {
                lootTypeNumber = Random.Range(0, 8);
            }            
            lootType = "wp" + lootTypeNumber;
        }
    }

    private void GetInventory()
    {
        for (int i = 0; i < lootDrop.Length; i++)
        {
            LootRandomizer();
            //Debug.Log("Size: " + lootSize + ", loot id: " + lootType + "");
            lootDrop[i] = lootType;
        }
        GetComponent<InventoryScript>().GetInventory(lootDrop, gameObject.name);
    }

    public void RemovePcFromList()
    {
        pcs = GameObject.FindGameObjectsWithTag("Character");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

    public NodeStates InAggroRange()
    {
        foreach (GameObject pc in pcs)
        {
            //Debug.Log(""+ Vector3.Distance(transform.position, pc.transform.position));
            if (Vector3.Distance(transform.position, pc.transform.position) < aggroRange)
            {
                //closestPC = Vector3.Distance(transform.position, pc.transform.position);
                //Debug.Log("pcn är inom aggrorangen");
                //iCombatTree = true;
                hasSeenEnemy = true;
                return NodeStates.success;
            }
        }
        //iCombatTree = false;
        return NodeStates.fail;
    }

    public NodeStates MoveTowardsClosestPc()
    {
        GameObject closestPc = pcs[0];
        foreach (GameObject pc in pcs)
        {
            if (Vector3.Distance(transform.position, pc.transform.position) < Vector3.Distance(transform.position, closestPc.transform.position))
            {
                if (pc.gameObject.activeInHierarchy)
                {
                    closestPc = pc;
                }
            }
        }

        //calculate direction between origin and target


        if (Vector2.Distance(transform.position, closestPc.transform.position) > 1)
        {
            animator.SetBool("Moving", true);
            Vector3 d = transform.position - closestPc.transform.position;
            d.Normalize();
            GetComponent<AIDestinationSetter>().SetPosTarget(closestPc.transform.position + d);
            return NodeStates.success;
        }
        animator.SetBool("Moving", false);
        //Debug.Log("Distance: " + Vector2.Distance(transform.position, closestPc.transform.position));
        return NodeStates.fail;
    }

    public NodeStates InMeleeRange()
    {
        animator.SetBool("Moving", false);
        if (attackTimer > 0)
        {
            //animator.SetBool("Attacking", false);
            return NodeStates.fail;
        }

        attackTimer = timeBetweenAttack;
        Collider2D[] pcsToDamage = Physics2D.OverlapCircleAll(transform.position, atkRange/*, pcLayer*/);
        bool hitPc = false;
        for (int i = 0; i < pcsToDamage.Length; i++)
        {
            if (pcsToDamage[i].CompareTag("Character"))
            {
                Vector3 difference = pcsToDamage[i].transform.position - this.transform.position;
                //calculate rotation
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 270);
                //Debug.Log("attackerar");
                animator.SetTrigger("Attack");
                hitPc = true;
                pcsToDamage[i].GetComponent<Stats>().TakeDamage(dmg);
            }
        }
        if (hitPc)
            return NodeStates.success;


        return NodeStates.fail;
    }
    void MovementForTest()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    target.z = transform.position.z;
        //}
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
