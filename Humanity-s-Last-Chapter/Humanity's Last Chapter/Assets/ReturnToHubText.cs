using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReturnToHubText : MonoBehaviour
{
    // Start is called before the first frame update
    Text test;
    void Start()
    {
        test = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void ChangeText()
    {
        test.text = "Success!! Return to hub to reap your rewards";
    }
}
