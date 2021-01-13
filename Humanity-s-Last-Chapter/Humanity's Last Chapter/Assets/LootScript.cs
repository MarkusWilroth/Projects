using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    private string strName;
    public string[] inventory;
    private GameObject[] characters;
    private float distance;
    private float range;
    private int charactersInRange;
    private int inventorySize;
    private bool isActive;

    public GameObject lootInfo;
    private GameObject holder;

    private CharacterScript cs;

    private void Start()
    {
        range = 99.3f;
    }

    private void OnMouseEnter()
    {
        //holder = Instantiate(lootInfo);
        //holder.transform.position = new Vector2(transform.position.x + 25, transform.position.y + 25);
        //holder.GetComponent<LootInfoScript>().GetData(strName, inventory);
    }

    private void OnMouseExit()
    {
        Destroy(holder);
    }

    private void OnMouseDown()
    {
        CheckRange();

        if (isActive)
        {
            //Give inventory to character :)

            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] != "")
                {
                    GameObject.FindGameObjectWithTag("LootFeed").GetComponent<LootReaderScript>().AddItem(inventory[i]);
                    WorldScript.world.AddItem(inventory[i], 1);
                }
            }
            Destroy(gameObject);
            //cs = character.GetComponent<CharacterScript>();
            //for (int i = 0; i < cs.inventory.Length; i++)
            //{
            //    if (cs.inventory[i] == "")
            //    {
            //        inventorySize--;
            //        for (int j = 0; j < inventory.Length; j++)
            //        {
            //            if (inventory[j] != null)
            //            {
            //                cs.inventory[i] = inventory[j];
            //                break;
            //            }
            //        }
            //    }
            //    if (inventorySize <= 0)
            //    {
            //        Destroy(gameObject);
            //        break;
            //    }
            //}
        }
    }

    public void GetItems(string[] inventory, string strName)
    {
        this.inventory = inventory;
        this.strName = strName;
        inventorySize = inventory.Length;
        characters = GameObject.FindGameObjectsWithTag("Character");
        Vector3 lootPos = new Vector3(transform.position.x, transform.position.y, -1);
        transform.position = lootPos;
    }

    #region CheckRange
    private void CheckRange()
    {
        charactersInRange = 0;
        if (!isActive)
        {
            foreach (GameObject character in characters)
            {
                if (character != null)
                {
                    distance = Vector3.Distance(character.transform.position, transform.position);
                    if (distance <= range)
                    {
                        charactersInRange = 1;
                    }
                }
            }
        }
        if (charactersInRange > 0)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }
    }
    #endregion
}
