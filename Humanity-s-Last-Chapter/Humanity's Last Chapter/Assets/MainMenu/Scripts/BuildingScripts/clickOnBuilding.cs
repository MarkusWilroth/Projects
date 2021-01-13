using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickOnBuilding : MonoBehaviour
{
    public string buildingName;
    public Canvas specificBuildingCanvas;

    private bool hbActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && hbActive)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePosition))
            {
                specificBuildingCanvas.gameObject.SetActive(true);

                GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

                for (int i = 0; i < buildings.Length; i++)
                {
                    buildings[i].GetComponent<clickOnBuilding>().SetHitBoxActive(false);
                }
                if (buildingName.Equals("Camp"))
                {
                    GetComponent<CampScript>().CreateRoster();
                }
            }
        }
    }
    public void SetHitBoxActive(bool set)
    {
        hbActive = set;
    }
}
