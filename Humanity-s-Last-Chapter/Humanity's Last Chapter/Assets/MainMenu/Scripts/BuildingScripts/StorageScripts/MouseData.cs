using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MouseData
{
    public static GameObject Item;
    public static void HoldItem(GameObject item)
    {
        Item = item;
    }
    public static void RemoveItem()
    {
        Item = null;
    }
    public static GameObject GetItem()
    {
        return Item;
    }
}
