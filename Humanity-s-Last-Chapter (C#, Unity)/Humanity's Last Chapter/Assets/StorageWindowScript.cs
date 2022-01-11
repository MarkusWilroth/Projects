using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageWindowScript : MonoBehaviour { //Markus - Detta script ser till att storage sparas i WorldScript world

    private GameObject[] itemSlots;

    //private void OnDestroy() {
    //    itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");
    //    Debug.Log("Length of ItemSlots: " + itemSlots.Length);

    //    for (int i = 0; i < itemSlots.Length; i++) {
    //        WorldScript.world.storageArr[i] = itemSlots[i].GetComponentInChildren<ItemInfo>().id;

    //    }
    //}
}
