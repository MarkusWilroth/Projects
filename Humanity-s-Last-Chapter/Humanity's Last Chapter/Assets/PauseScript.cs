using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool pause = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (pause)
            {
                pause = false;
                Time.timeScale = 1;
            }
            else
            {
                pause = true;
                Time.timeScale = 0;
            }
        }

     
    }
}
