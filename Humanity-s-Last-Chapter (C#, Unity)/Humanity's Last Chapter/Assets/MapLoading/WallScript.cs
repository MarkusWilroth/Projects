using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    List<float> values = new List<float>();
    public void AddValue(float x)
    {
        values.Add(x);
    }
    public float GetValue(int x)
    {
        return values[x];
    }
}
