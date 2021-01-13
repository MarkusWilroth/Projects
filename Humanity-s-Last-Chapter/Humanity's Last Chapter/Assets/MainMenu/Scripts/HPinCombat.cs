using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPinCombat : MonoBehaviour
{
    Vector3 localScale;
    float startHP;
    public GameObject attachedToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        startHP = attachedToPlayer.GetComponent<Stats>().hp;

        //Debug.Log(localScale.x);
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = attachedToPlayer.GetComponent<Stats>().hp/startHP;
        transform.localScale = 0.5f * localScale;
        //Debug.Log(localScale.x);
        //test
        if (Input.GetKeyDown(KeyCode.T))
        {
            attachedToPlayer.GetComponent<Stats>().hp--;
        }
    }
}
