using Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class PersonalMovement : MonoBehaviour
{
    public RootNode BT;
    //public bool muzzleFlash = false;
    bool debugger = true;
    public bool moving = true;
    public float charactersMovespeed = 100f;
    public Vector3 relativePos = Vector3.zero;
    public Vector3 relativePosNonRotated = Vector3.zero;
    public Vector3 posPlusRel;
    public List<Vector3> waypoints = new List<Vector3>();
    Vector3 waypoint;
    RaycastHit2D positionBy;
    public LayerMask buildingLayer;
    private int currentWaypoint = 0;
   // public Vector2 direction = Vector2.zero;
    public GameObject manager;
    public bool ByFormation = true;
    public Vector3 posNotFormation = Vector3.zero;
    bool movingToRngPos = false;
    Vector3 rngPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        BT = GetComponent<BehaviourTree>().GetPcBt();
        manager = GameObject.FindGameObjectWithTag("CharacterManager");
        waypoint = manager.transform.position + relativePos;
        if (GetComponent<CharacterScript>().role != null && GetComponent<CharacterScript>().role.roleName.Equals("Commander"))
            manager.GetComponent<CharacterMovement>().hasCommander = true;

    }

    // Update is called once per frame
    void Update()
    {
        //GetComponentInChildren<Animator>().SetBool("flashAAA", false);
        //Debug.Log(ByFormation);
        //if (!ByFormation)
        posNotFormation = transform.position;
        if (debugger && positionBy)
            Debug.DrawLine(manager.transform.position, positionBy.point);
        BT.Start();
        //Movement();
        if (GetComponent<AIPath>().reachedDestination)
            moving = false;
        else
            moving = true;


        //Debug.Log(relativePosNonRotated);
    }
    
   
    public void AddWaypoint(Vector3 pos)
    {
        waypoint = pos;
    }

    public void AddRelativeWaypoint(Vector3 toAdd)
    {
        //Debug.Log("adding: " + toAdd);
        posPlusRel = toAdd + relativePos;
        //waypoints.Add(posPlusRel);
        waypoint = posPlusRel;
    }

    public NodeStates RngPos()
    {
        //Debug.Log("rngpos");
        //if (moving)
        //    return NodeStates.fail;
        if (movingToRngPos)
        {
            GetComponent<AIDestinationSetter>().SetPosTarget(rngPos);
        } else
        {
            float newX = Random.Range(-1, 1);
            float newY = Random.Range(-1, 1);
            rngPos = new Vector3(transform.position.x + newX, transform.position.y + newY);
            //Debug.Log("rngpos : " + rngPos);
            movingToRngPos = true;
        }
        if (Vector2.Distance(rngPos, transform.position) < 0.5f)
        {
            movingToRngPos = false;
        }
        //Debug.Log(movingToRngPos);
        return NodeStates.success;
    }
    public NodeStates HasCommander()
    {
        //return NodeStates.success;
        if (manager.GetComponent<CharacterMovement>().hasCommander)
        {
            return NodeStates.success;
        }
        GetComponent<AIPath>().maxSpeed = 2;
        return NodeStates.fail;
    }

    public NodeStates IsMoving()
    {
        if (moving)
            return NodeStates.success;
        //if (manager.GetComponent<CharacterMovement>().updateFormation && )
        return NodeStates.fail;
    }

    public NodeStates OwnPos()
    {
        //Debug.Log("ownpos");
        if (!ByFormation)
        {
            GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //bug.Log("here we are");
            return NodeStates.success;
        }

        return NodeStates.fail;
    }

    public NodeStates PosByFormation()
    {
        //Debug.Log("why here");
        positionBy = Physics2D.Raycast(manager.transform.position, /*manager.transform.position + */relativePos, relativePos.magnitude, buildingLayer);
        waypoint = manager.transform.position + relativePos;
        if (positionBy != false)
        {
            Vector2 v = positionBy.point - (Vector2)transform.position;
            v.Normalize();
            v *= 0.1f;
            waypoint = positionBy.point + v;
            GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
        } else
        {
            GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
        }

        return NodeStates.success;
    }

    public Vector3 GetPositionBy(Vector3 position)
    {
        positionBy = Physics2D.Raycast(position, /*manager.transform.position + */relativePos, relativePos.magnitude, buildingLayer);
        if (positionBy != false)
        {
            Vector2 v = positionBy.point - (Vector2)transform.position;
            v.Normalize();
            v *= 0.1f;
            position = positionBy.point + v;
            return position;
        } else
        {
            return position + relativePos;
        }
    }

    public void FlushWaypoints()
    {
        waypoints.Clear();
    }

    private void Positioning()
    {
        waypoints.Add(manager.transform.position + relativePos);
    }

    //private void Movement()
    //{
    //    //if (movingToRngPos)
    //    //    return;

    //    if (manager.GetComponent<CharacterMovement>().updateFormation && ByFormation)
    //    {
    //        positionBy = Physics2D.Raycast(manager.transform.position, relativePos, relativePos.magnitude, buildingLayer);
    //        waypoint = manager.transform.position + relativePos;
    //        if (positionBy != false)
    //        {
    //            Vector2 v = positionBy.point - (Vector2)transform.position;
    //            v.Normalize();
    //            v *= 0.1f;
    //            waypoint = positionBy.point + v;
    //            GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
    //        } else
    //        {
    //            GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
    //        }
    //    }
    //}


        //moving = true;
        //if (Vector2.Distance(waypoint, transform.position) < 0.5f)
        //{
        //    moving = false;
        //}
        //positionBy = Physics2D.Raycast(posNotFormation, relativePos, relativePos.magnitude, buildingLayer);

            //if (!ByFormation)
            //{
            //    transform.position = posNotFormation;
            //    //waypoint = posNotFormation + relativePos;
            //    //if (positionBy != false)
            //    //{
            //    //    Vector2 v = positionBy.point - (Vector2)transform.position;
            //    //    v.Normalize();
            //    //    v *= 0.1f;
            //    //    waypoint = positionBy.point + v;
            //    //    GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //    //} else
            //    //{
            //    //    GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //    //}
            //    ////GetComponent<AIDestinationSetter>().SetPosTarget(posNotFormation);
            //    //return;
            //}else
            //{
            //    if (!manager.GetComponent<CharacterMovement>().updateFormation && !moving)
            //    {
            //        transform.position = posNotFormation;
            //        //GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //    }


            //        //if (manager.GetComponent<CharacterMovement>().updateFormation)
            //        //    waypoint = manager.transform.position + relativePos;
            //    positionBy = Physics2D.Raycast(manager.transform.position, relativePos, relativePos.magnitude, buildingLayer);
            //    if (positionBy != false)
            //    {
            //        Vector2 v = positionBy.point - (Vector2)transform.position;
            //        v.Normalize();
            //        v *= 0.1f;
            //        waypoint = positionBy.point + v;
            //        GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //    } else
            //    {
            //        GetComponent<AIDestinationSetter>().SetPosTarget(waypoint);
            //    }

            //}

   // }
}
