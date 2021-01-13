using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPrefabFinisher : MonoBehaviour
{
    public Sprite roadSprite;
    public Sprite wallSprite;
    void Start()
    {
        foreach(GameObject road in GameObject.FindGameObjectsWithTag("road"))
        {
            road.AddComponent<SpriteRenderer>();
            road.GetComponent<SpriteRenderer>().sprite = roadSprite;
            road.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
            road.GetComponent<SpriteRenderer>().size = new Vector2(road.GetComponent<RoadScript>().GetValue(2), road.GetComponent<RoadScript>().GetValue(3));
            road.AddComponent<BoxCollider2D>();
            road.GetComponent<BoxCollider2D>().size = new Vector2(road.GetComponent<RoadScript>().GetValue(2), road.GetComponent<RoadScript>().GetValue(3));
            road.transform.position = new Vector3(road.GetComponent<RoadScript>().GetValue(0), road.GetComponent<RoadScript>().GetValue(1), 0);
        }
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("wall"))
        {
            wall.AddComponent<SpriteRenderer>();
            wall.GetComponent<SpriteRenderer>().sprite = wallSprite;
            wall.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Simple;
            wall.GetComponent<SpriteRenderer>().size = new Vector2();
            wall.transform.position = new Vector3(wall.GetComponent<RoadScript>().GetValue(0), wall.GetComponent<RoadScript>().GetValue(1), 0);
        }
    }
}
