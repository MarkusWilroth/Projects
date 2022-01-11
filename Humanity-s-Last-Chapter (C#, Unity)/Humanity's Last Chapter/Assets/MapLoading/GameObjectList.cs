using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectList : MonoBehaviour
{
    public string prefabType;
    List<GameObject> objects = new List<GameObject>();
    int x = 0;

    public void SetType(string newType)
    {
        prefabType = newType;
    }

    public void Add(params float[] values)
    {
        objects.Add(new GameObject());
        if (prefabType == "Road")
        {
            objects[x].AddComponent<RoadScript>();
            for (int i = 0; i < values.Length; i++)
            {
                objects[x].GetComponent<RoadScript>().AddValue(values[i]);
            }
        }
        else if (prefabType == "Wall")
        {
            objects[x].AddComponent<WallScript>();
            objects[x].tag = "Building";
            objects[x].layer = 9;
            for (int i = 0; i < values.Length; i++)
            {
                objects[x].GetComponent<WallScript>().AddValue(values[i]);
            }
        }
        objects[x].transform.parent = transform;
        x++;
    }
    public List<GameObject> GetObjects()
    {
        return objects;
    }
}
