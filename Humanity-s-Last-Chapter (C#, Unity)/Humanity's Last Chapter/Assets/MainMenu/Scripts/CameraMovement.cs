using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float speed = 2f;
    public float edgeScrollSpeed = 0.5f;
    public float maxZoom = 3;
    private float zoom = 5;
    public bool edgeScrollEnabled = true;
    public float zoomSpeed = 2f;
    private Vector3 previousPosition;
    float xBoundary = 8.0f;
    float yBoundary = 4.04f;
    float maxYBoundary = 73.88f;
    float maxXBoundary = 70.0f;

    private Camera myCam;
    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponent<Camera>();

    }
    // Update is called once per frame
    void Update()
    {
        ScrollZoom();

        KeyScroll();
        if (Input.GetKey(KeyCode.LeftAlt))
            PullScroll();
        else
            EdgeScroll();

        if (transform.position.x < xBoundary)
        {
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.y < yBoundary)
        {
            transform.position = new Vector3(transform.position.x, yBoundary, transform.position.z);
        }
        if (transform.position.x > maxXBoundary)
        {
            transform.position = new Vector3(maxXBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.y > maxYBoundary)
        {
            transform.position = new Vector3(transform.position.x, maxYBoundary, transform.position.z);
        }

    }
    private void ScrollZoom()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            zoom -= zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            zoom += zoomSpeed * Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            zoom -= zoomSpeed * Time.deltaTime * 10;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            zoom += zoomSpeed * Time.deltaTime * 10;
        }
        if (zoom < maxZoom)
            zoom = maxZoom;
        else if (zoom > 15)
            zoom = 15;
        myCam.orthographicSize = zoom;
    }

    private void KeyScroll()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * (zoom / 25) * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * (zoom / 25) * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, speed * (zoom / 25) * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -speed * (zoom / 25) * Time.deltaTime, 0));
        }
    }    

    private void PullScroll()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetMouseButtonDown(1))
            {
                previousPosition = Input.mousePosition;
            }
            Vector3 distance = Input.mousePosition - previousPosition;
            transform.position -= distance * 0.1f * (zoom / 25);
            previousPosition = Input.mousePosition;
        }
    }

    private void EdgeScroll() //if your mouse goes towards the edge of the screen the screen will go that way
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!edgeScrollEnabled)
                edgeScrollEnabled = true;
            else
                edgeScrollEnabled = false;
        }
        if (!edgeScrollEnabled)
        {
            return;
        }
        int edgeSize = 50;
        if (Input.mousePosition.x > Screen.width - edgeSize)
        {
            transform.Translate(new Vector3(edgeScrollSpeed * (zoom / 25) * Time.deltaTime, 0, 0));
        }
        if (Input.mousePosition.x < edgeSize)
        {
            transform.Translate(new Vector3(-edgeScrollSpeed * (zoom / 25) * Time.deltaTime, 0, 0));
        }
        if (Input.mousePosition.y > Screen.height - edgeSize)
        {
            transform.Translate(new Vector3(0, edgeScrollSpeed * (zoom / 25) * Time.deltaTime, 0));
        }
        if (Input.mousePosition.y < edgeSize)
        {
            transform.Translate(new Vector3(0, -edgeScrollSpeed * (zoom / 25) * Time.deltaTime, 0));
        }
    }
}
