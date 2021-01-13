using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;
using System;

public static class ReadFileScript //Rad 93 & 94 är bortkommenterad
{
    private static string mapPath;
    private static string savePath;
    public static int mapID;
    private static string[] words;
    private static GameObject roadsPrefab;
    private static GameObject wallsPrefab;
    public static void ReadString(Sprite emptyroadSprite, Sprite roadSprite, Sprite wallSprite)
    {
        mapID = 10;
        mapPath = "MapFiles/pcgmap" + mapID + ".txt";
        savePath = "Assets/MainMenu/Scenes/Map" + mapID;
        StreamReader sr = new StreamReader(mapPath);
        words = sr.ReadToEnd().Split(' ', '\n');
        sr.Close();
        PreparePrefabs();
        MapLoader(emptyroadSprite, roadSprite, wallSprite);
    }

    private static void PreparePrefabs()
    {
        roadsPrefab = new GameObject();
        roadsPrefab.AddComponent<GameObjectList>();
        roadsPrefab.GetComponent<GameObjectList>().SetType("Road");
        wallsPrefab = new GameObject();
        wallsPrefab.AddComponent<GameObjectList>();
        wallsPrefab.GetComponent<GameObjectList>().SetType("Wall");
    }


    public static void MapLoader(Sprite emptyroadSprite, Sprite roadSprite, Sprite wallSprite)
    {
        for (int i = 0; i < words.Length; i++)
        {
            //if (words[i] == "b")
            //{
            //    NewBuilding(Convert.ToInt32(words[i + 1]) * 2);
            //}
            //else if (words[i] == "w")
            //{
            //    NewWall(Convert.ToSingle(words[i + 1].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 2].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 3].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 4].Replace(',', '.')) * 2);
            //}
            //else if (words[i] == "r")
            //{
            //    if (words[i + 4] == "h")
            //        NewRoad(Convert.ToSingle(words[i + 1].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 2].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 3].Replace(',', '.')) * 2);
            //    else
            //        NewRoad(Convert.ToSingle(words[i + 1].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 2].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 3].Replace(',', '.')) * 2);
            //}
            //else if (words[i] == "j")
            //{
            //    NewJunction(Convert.ToSingle(words[i + 1].Replace(',', '.')) * 2, Convert.ToSingle(words[i + 2].Replace(',', '.')) * 2);
            //}
            //Davids dator
            if (words[i] == "b")
            {
                NewBuilding(Convert.ToInt32(words[i + 1]) * 2);
            }
            else if (words[i] == "w")
            {
                NewWall(Convert.ToSingle(words[i + 1]) * 2, Convert.ToSingle(words[i + 2]) * 2, Convert.ToSingle(words[i + 3]) * 2, Convert.ToSingle(words[i + 4]) * 2);
            }
            else if (words[i] == "r")
            {
                if (words[i + 4].Contains("h"))
                {
                    NewRoad(Convert.ToSingle(words[i + 1]) * 2, Convert.ToSingle(words[i + 2]) * 2, 1);
                }
                else
                {
                    NewRoad(Convert.ToSingle(words[i + 1]) * 2, Convert.ToSingle(words[i + 2]) * 2, 2);
                }
            }
            else if(words[i] == "j")
            {
                NewJunction(Convert.ToSingle(words[i + 1]) * 2, Convert.ToSingle(words[i + 2]) * 2);
            }
        }

        foreach (GameObject wall in wallsPrefab.GetComponent<GameObjectList>().GetObjects())
        {
            wall.AddComponent<SpriteRenderer>();
            wall.GetComponent<SpriteRenderer>().sprite = wallSprite;
            wall.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
            wall.GetComponent<SpriteRenderer>().size = new Vector2(wall.GetComponent<WallScript>().GetValue(2), wall.GetComponent<WallScript>().GetValue(3));
            wall.AddComponent<BoxCollider2D>();
            wall.GetComponent<BoxCollider2D>().size = new Vector2(wall.GetComponent<WallScript>().GetValue(2), wall.GetComponent<WallScript>().GetValue(3));
            wall.transform.position = new Vector3(wall.GetComponent<WallScript>().GetValue(0), wall.GetComponent<WallScript>().GetValue(1), 0);
        }
        foreach (GameObject road in roadsPrefab.GetComponent<GameObjectList>().GetObjects())
        {
            road.AddComponent<SpriteRenderer>();
            road.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Tiled;
            road.GetComponent<SpriteRenderer>().size = new Vector2(2, 2);
            if (road.GetComponent<RoadScript>().ValuesCount() > 2)
            {
                road.GetComponent<SpriteRenderer>().sprite = roadSprite;
            }
            else
            {
                road.GetComponent<SpriteRenderer>().sprite = emptyroadSprite;
            }
            road.transform.position = new Vector3(road.GetComponent<RoadScript>().GetValue(0), road.GetComponent<RoadScript>().GetValue(1), 0);
        }
        //PrefabUtility.SaveAsPrefabAsset(roadsPrefab, savePath + ".roads.prefab");
        //PrefabUtility.SaveAsPrefabAsset(wallsPrefab, savePath + ".walls.prefab");
    }

    private static void NewBuilding(int ID)
    {

    }

    private static void NewWall(float posX, float posY, float width, float height)
    {
        wallsPrefab.GetComponent<GameObjectList>().Add(posX, posY, width, height);
    }

    private static void NewRoad(float posX, float posY, float alignment)
    {
        roadsPrefab.GetComponent<GameObjectList>().Add(posX, posY, alignment);
    }

    private static void NewJunction(float posX, float posY)
    {
        roadsPrefab.GetComponent<GameObjectList>().Add(posX, posY);
    }

    private static void EditAsset(string str)
    {

    }
}
