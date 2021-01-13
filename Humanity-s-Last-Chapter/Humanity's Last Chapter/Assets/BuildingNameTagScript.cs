using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingNameTagScript : MonoBehaviour
{
    GameObject Paper;
    GameObject Text;
    GameObject[] Buildings;
    GameObject WindowManager;
    // Start is called before the first frame update
    void Start()
    {
        Paper = transform.GetChild(0).gameObject;
        Text = Paper.transform.GetChild(0).gameObject;
        Buildings = GameObject.FindGameObjectsWithTag("Building");
        WindowManager = GameObject.FindGameObjectWithTag("WindowManager");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < WindowManager.transform.childCount; i++)
        {
            if(WindowManager.transform.GetChild(i).gameObject.activeSelf)
            {
                if (Paper.activeSelf)
                {
                    Paper.gameObject.SetActive(false);
                }
                return;
            }
        }
        foreach (GameObject building in Buildings)
        {
            if (building.GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                Paper.gameObject.SetActive(true);
                Text.GetComponent<Text>().text = building.name;
                break;
            }
            else if (Paper.activeSelf)
            {
                Paper.gameObject.SetActive(false);
            }
        }
        if(Paper.activeSelf)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }
}
