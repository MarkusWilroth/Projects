using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class RangedEnemy : MonoBehaviour
{
    Vector3 setPosTo = new Vector3(0, 0, 0), lastSeenPlayerPos = Vector2.zero;
    GameObject[] toGoTo;
    public Vector3[] waypoints;
    int currentWP;
    float offset = 3f;
    RootNode BT;
    GameObject targetedPc;
    public float range = 3;
    public GameObject Projectile;
    GameObject projectile;
    public Animator animator;
    public LayerMask walls;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Building");
        waypoints = new Vector3[3];
        //waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        Vector3 toAdd = new Vector3(Random.Range(transform.position.x - offset, transform.position.x + offset),
            Random.Range(transform.position.y - offset, transform.position.y + offset), 0);
        waypoints[0] = toAdd;
        toAdd = new Vector3(Random.Range(transform.position.x - offset, transform.position.x + offset),
            Random.Range(transform.position.y - offset, transform.position.y + offset), 0);
        waypoints[1] = toAdd;
        toAdd = new Vector3(Random.Range(transform.position.x - offset, transform.position.x + offset),
            Random.Range(transform.position.y - offset, transform.position.y + offset), 0);
        waypoints[2] = toAdd;
        animator = GetComponent<Animator>();
        foreach (GameObject go in walls)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (go.GetComponent<BoxCollider2D>().OverlapPoint(waypoints[i]))
                {
                    waypoints[i] = transform.position;
                }
            }
        }

        GetComponent<AIDestinationSetter>().SetPosTarget(waypoints[currentWP]);
        currentWP = 0;
        //Debug.Log("" + waypoints.Length);
        BT = GetComponent<BehaviourTree>().GetRangedEnemyBt();
        //.Log("skapas som den ska");
    }
    public NodeStates CloseToWaypoint()
    {
        //Debug.Log(waypoints[currentWP]);
        if (waypoints.Length == 0)
        {
            return NodeStates.fail;
        }
        if (Vector2.Distance(waypoints[currentWP], transform.position) < 0.5f)
        {
            return NodeStates.success;
        }
        if (GetComponent<Enemy>().hasSeenEnemy)
        {
            GetComponent<Enemy>().hasSeenEnemy = false;
            return NodeStates.success;
        }
        return NodeStates.fail;
    }

    public NodeStates NextWaypoint()
    {
        currentWP++;
        //Debug.Log(waypoints[currentWP]);
        if (currentWP < waypoints.Length)
        {
            GetComponent<AIDestinationSetter>().SetPosTarget(waypoints[currentWP]);
        } else
        {
            currentWP = 0;
            GetComponent<AIDestinationSetter>().SetPosTarget(waypoints[currentWP]);
        }
        //Debug.Log("" + currentWP);
        return NodeStates.success;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Moving", true);
        if (BT == null)
        {
            //Debug.Log("inget BT");
            return;
        }
        //Debug.Log("går in i update");
        BT.Start();
        //Debug.Log("i ranged enemy: " );
    }

    public NodeStates PcInRange()
    {
        foreach (GameObject go in GetComponent<Enemy>().pcs)
        {
            Vector2 v = go.transform.position - transform.position;
            RaycastHit2D ray = Physics2D.Raycast(transform.position, v, range, walls);
            if (targetedPc == null)
            {
                if (!ray && Vector2.Distance(go.transform.position, transform.position) < range)
                {
                    targetedPc = go;
                }
            } else if (!ray && Vector2.Distance(go.transform.position,transform.position) < Vector2.Distance(targetedPc.transform.position,transform.position))
            {
                targetedPc = go;
            }
        }

        if (targetedPc != null)
        {
            Vector3 towards = targetedPc.transform.position - transform.position;
            float angle = Mathf.Atan2(towards.y, towards.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = q;
            //Debug.Log("ser en karaktär");
            GetComponent<Enemy>().hasSeenEnemy = true;
            return NodeStates.success;
        }

        //Debug.Log("kan inte se en karaktär");
        return NodeStates.fail;
    }

    public NodeStates RangedAttack()
    {
        if (GetComponent<Enemy>().attackTimer > 0)
            return NodeStates.fail;
        animator.SetTrigger("Attack");
        projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().CreateProjectile(0f);
        projectile.GetComponent<Projectile>().damage = GetComponent<Enemy>().dmg;
        projectile.GetComponent<Projectile>().SetTargetPos(targetedPc.transform.position);

        projectile.GetComponent<Projectile>().whatIsSolid = GetComponent<Enemy>().pcLayer;
        GetComponent<Enemy>().attackTimer = GetComponent<Enemy>().timeBetweenAttack;
        lastSeenPlayerPos = targetedPc.transform.position;
        targetedPc = null;

        return NodeStates.success;
    }

    public NodeStates HasSeenPc()
    {
        if (lastSeenPlayerPos.x != 0 && lastSeenPlayerPos.y != 0)
            return NodeStates.fail;

        return NodeStates.success;
    }

    public NodeStates TowardsLastSeenPc()
    {
        GetComponent<AIDestinationSetter>().SetPosTarget(lastSeenPlayerPos);
        return NodeStates.fail;
    }
}
