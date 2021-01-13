using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToPlayerRoster : MonoBehaviour
{
    public GameObject UIParent;
    public GameObject controller;
    public bool owned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void AddToPlayer()
    {
        if (owned)
        {
            return;
        }
        //DontDestroyOnLoad(gameObject);
        this.transform.SetParent(UIParent.transform);
        //GetComponent<RectTransform>().position = new Vector3(GetComponent<RectTransform>().position.x - 200,
        //    GetComponent<RectTransform>().position.y, 0);
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("Controller");
        for (int i  = 0; i < controllers.Length; i++)
        {
            controllers[i].GetComponent<HubCharController>().AddToRoster(gameObject);
        }
        //StaticCharacters.AddPC(gameObject);
        controller = controllers[0];
        owned = true;
    }
}
