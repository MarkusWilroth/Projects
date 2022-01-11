using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public float offset;
    private float rotZ;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = new Vector3(0,100,0);
    }

    // Update is called once per frame
    void Update()
    {
        //calculate direction between origin and target
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        //calculate rotation
        //rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //set rotation
        //Rotation();
    }

    public Quaternion Rotation()
    {
        return this.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
    }
}
