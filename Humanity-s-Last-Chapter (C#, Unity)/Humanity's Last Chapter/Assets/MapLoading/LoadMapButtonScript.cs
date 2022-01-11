using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LoadMapButtonScript : MonoBehaviour
{
    public Sprite emptyroadSprite;
    public Sprite roadSprite;
    public Sprite wallSprite;
    public void OnClick()
    {
        Debug.Log("Mapload started");
        //Thread thread = new Thread(ReadString);
        //thread.Start();
        ReadFileScript.ReadString(emptyroadSprite, emptyroadSprite, wallSprite);
        Debug.Log("Mapload finished");
    }


    //public void ReadString()
    //{
    //    ReadFileScript.ReadString(roadSprite, wallSprite);
    //    Debug.Log("Mapload finished");
    //}
}
