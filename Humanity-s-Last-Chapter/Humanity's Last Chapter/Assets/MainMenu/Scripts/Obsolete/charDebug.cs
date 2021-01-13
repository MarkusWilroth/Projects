using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponentInChildren<Text>().text + " currentPos : " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
