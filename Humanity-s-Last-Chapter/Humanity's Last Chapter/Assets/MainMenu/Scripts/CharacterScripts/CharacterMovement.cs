using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Node;

public class CharacterMovement : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 1000f;

    Vector3 rotDirection = Vector3.zero, rotStart = Vector3.zero;
    RaycastHit2D ray;

    public List<GameObject> pcs = new List<GameObject>();

    private List<GameObject> selectedCharacters;
    bool posForFormation = true;
    public bool updateFormation = true;

    public List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypoint = 0;

    public GameObject toDrawForRotation;

    public bool hasCommander = false;
    //For strategygame select units
    Vector3 startPos = Vector3.zero, endPos = Vector3.zero;
    //För att rita ut rutan som vi selectar
    Vector3 startDrawPos = Vector3.zero, endDrawPos = Vector3.zero;
    public bool drawBox = true;
    public Texture semiTransBox;
    Rect drawBoxRect = Rect.zero;
    // Start is called before the first frame update
    void Start()
    {
        drawBox = true;
        GetComponent<LineRenderer>().alignment = LineAlignment.View;
        GetComponent<LineRenderer>().useWorldSpace = true;
        waypoints.Add(new Vector3(1, 0, 0));
        //transform.position = new Vector3(3, 3, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        pcs = GameObject.FindGameObjectsWithTag("Character").ToList<GameObject>();
        if (selectedCharacters == null)
            selectedCharacters = pcs;
        AddSelectedCharacters();
        //if (drawBox)
            //OnGUI();
        InputMovement();
        //Movement();
        InputRotation();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (GameObject go in pcs)
            {
                go.GetComponent<PersonalMovement>().ByFormation = true;
                go.GetComponent<SpriteRenderer>().color = Color.white;
            }
            selectedCharacters = pcs;
        }
        if (posForFormation && updateFormation)
        {
            foreach (GameObject go in pcs)
            {
                go.GetComponent<PersonalMovement>().ByFormation = true;
            }
            updateFormation = false;
        }//else if (updateFormation)
        //{

        //}
    }
   

    void InputRotation()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftAlt))
        {
            rotStart = mousePosition;
            GetComponent<LineRenderer>().enabled = true;
        }
        if (Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftAlt))
        {
            rotDirection = mousePosition;
            Vector3[] v = new Vector3[2];
            rotDirection.z = 0;
            rotStart.z = 0;
            v[0] = rotStart;
            v[1] = rotDirection;
            GetComponent<LineRenderer>().SetPositions(v);
            
            //Debug.Log(v + "" + rotStart +""+ rotDirection);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftAlt))
        {
            GetComponent<LineRenderer>().enabled = false;
            RotateFormation();
        }
    }

    public void AddSelectedCharacters()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startDrawPos = Input.mousePosition;
            startPos = mousePosition;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //drawBox = true;
            endDrawPos = Input.mousePosition;
            endPos = mousePosition;
            CreateRectFromMouse();
            SelectChars(drawBoxRect);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            //drawBox = false;
            startPos = Vector3.zero;
            endPos = Vector3.zero;
            startDrawPos = Vector3.zero;
            endDrawPos = Vector3.zero;
            drawBoxRect = Rect.zero;
        }
    }
    
    void CreateRectFromMouse()
    {
        float width, height;
        if (startDrawPos.x > endDrawPos.x)
            width = startDrawPos.x - endDrawPos.x;
        else
            width = endDrawPos.x - startDrawPos.x;

        if (startDrawPos.y > endDrawPos.y)
            height = startDrawPos.y - endDrawPos.y;
        else
            height = endDrawPos.y - startDrawPos.y;
        Rect toDrawBox;

        if (endDrawPos.x > startDrawPos.x)
        {
            if (endDrawPos.y > startDrawPos.y)
                toDrawBox = new Rect(startDrawPos.x, Screen.height - endDrawPos.y, width, height);
            else
                toDrawBox = new Rect(startDrawPos.x, Screen.height - startDrawPos.y, width, height);
        } else
        {
            if (endDrawPos.y > startDrawPos.y)
                toDrawBox = new Rect(endDrawPos.x, Screen.height - endDrawPos.y, width, height);
            else
                toDrawBox = new Rect(endDrawPos.x, Screen.height - startDrawPos.y, width, height);
        }

        drawBoxRect = toDrawBox;
    }

    void OnGUI() //Det är så rutan ritas ut när man klickar vänstterklick och håller inne, jag vet inte varför jag får ett error
    {
        if (drawBox)
            GUI.DrawTexture(drawBoxRect, semiTransBox);
    }

    public void SelectChars(Rect toSelect)
    {
        Vector3 v;
        Vector3 testScreen = new Vector3(Screen.width/2, Screen.height/2, 0);
        selectedCharacters.Clear();
        foreach (GameObject go in pcs)
        {
            v = Camera.main.WorldToScreenPoint(go.transform.position);
            v.y = Screen.height - v.y;
            if (toSelect.Contains(v))
            {
                go.GetComponent<PersonalMovement>().ByFormation = true;
                selectedCharacters.Add(go);
                go.GetComponent<SpriteRenderer>().color = Color.green;
            } else
            {
                go.GetComponent<PersonalMovement>().ByFormation = false;
                selectedCharacters.Remove(go);
                go.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        if (selectedCharacters.Count > 0 && !(selectedCharacters.Count == pcs.Count))
        {
            posForFormation = false;
        }else
        {
            selectedCharacters = pcs;
            posForFormation = true;
        }
    }

    public void AddPc(GameObject toAdd)
    {
        pcs.Add(toAdd);
    }
    /// <summary>
    /// obs fungerar ej just nu
    /// </summary>
    /// <param name="rotateTowards"></param>
    public void RotateFormation()
    {
        Vector3 difference = rotDirection - rotStart;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, rotZ + 270);
        if (!posForFormation)
        {
            foreach (GameObject pc in selectedCharacters)
            {
                Vector3 toSet = pc.GetComponent<PersonalMovement>().relativePosNonRotated;
                toSet = rot * toSet;
                pc.GetComponent<PersonalMovement>().relativePos = toSet;
            }
        }else
        {
            foreach (GameObject pc in pcs)
            {
                Vector3 toSet = pc.GetComponent<PersonalMovement>().relativePosNonRotated;
                toSet = rot * toSet;
                pc.GetComponent<PersonalMovement>().relativePos = toSet;
            }
        }
    
    }

    private void InputMovement()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse1)/*&& !Input.GetKey(KeyCode.LeftShift)*/ && !Input.GetKey(KeyCode.LeftAlt) && posForFormation)
        {
            waypoints.Clear();

            waypoints.Add(mousePosition);
            transform.position = mousePosition;
            updateFormation = true;

            //RotateFormation(mousePosition);
            //}else if (!posForFormation && Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftAlt))
            //{
            //    foreach (GameObject go in selectedCharacters)
            //    {
            //        go.GetComponent<PersonalMovement>().ByFormation = false;
            //        go.GetComponent<PersonalMovement>().posNotFormation = mousePosition;

            //    }
            //}
        } else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            foreach (GameObject go in selectedCharacters)
            {
                go.GetComponent<PersonalMovement>().ByFormation = false;
                if (selectedCharacters.Count > 1)
                {
                    Debug.Log(selectedCharacters.Count);
                    go.GetComponent<PersonalMovement>().AddWaypoint(go.GetComponent<PersonalMovement>().GetPositionBy(mousePosition/* + go.GetComponent<PersonalMovement>().relativePos*/));
                }
                else
                {
                    Debug.Log("en karaktär");
                    go.GetComponent<PersonalMovement>().AddWaypoint(mousePosition);
                }
            }
        }  
    }

    //private void Movement()
    //{
    //    //if (waypoints.Count != 0)
    //    //{
    //    //    direction = waypoints[currentWaypoint] - transform.position;
    //    //    GetComponent<AIDestinationSetter>().SetPosTarget(waypoints[currentWaypoint]);
    //    //    //Debug.Log("distance  :   " + Vector3.Distance(transform.position, waypoints[currentWaypoint]));
    //    //    if (direction.magnitude < 2)
    //    //    {
    //    //        currentWaypoint++;
    //    //    }
    //    //    //direction = direction.normalized;
    //    //    //transform.position += direction * moveSpeed * Time.deltaTime;
    //    //    if (currentWaypoint >= waypoints.Count)
    //    //    {
    //    //        //Debug.Log("in");
    //    //        currentWaypoint = 0;
    //    //        waypoints.Clear();
    //    //    }
    //    //}
    //}
}