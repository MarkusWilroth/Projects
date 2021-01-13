using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootReaderScript : MonoBehaviour
{
    private List<string> items;
    private int prematurelyDeletedItem = 0;

    void Start()
    {
        items = new List<string>();
    }

    void Update()
    {

    }

    void UpdateText()
    {
        if (items.Count > 0)
        {
            GetComponent<TextMeshProUGUI>().text = "You found: " + items[0] + ".";
            for (int i = 1; i < items.Count; i++)
            {
                GetComponent<TextMeshProUGUI>().text += "\nYou found: " + items[i] + ".";
            }
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void AddItem(string id)
    {
        items.Add(Translator(id));
        if (items.Count > 6)
        {
            items.RemoveAt(0);
            prematurelyDeletedItem++;
        }
        Invoke("RemoveItem", 5);

        UpdateText();
    }

    void RemoveItem()
    {
        if (prematurelyDeletedItem <= 0)
        {
            items.RemoveAt(0);
            UpdateText();
            prematurelyDeletedItem = 0;
        }
        else
        {
            prematurelyDeletedItem--;
        }
    }

    string Translator(string id)
    {
        switch (id)
        {
            default:
                return "";
            case "wp0":
                return "Pistol";
            case "wp1":
                return "Heavy Handgun";
            case "wp2":
                return "Rifle";
            case "wp3":
                return "Automatic Rifle";
            case "wp4":
                return "Precision Rifle";
            case "wp5":
                return "Shotgun";
            case "wp6":
                return "Padde Killer";
            case "wp7":
                return "Minigun";
            case "ci0":
                return "Automatic Turret";
            case "ci1":
                return "Minigun Turret";
            case "ci2":
                return "Antique Grenade";
            case "ci3":
                return "MKV Grenade";
            case "hi0":
                return "Weak Bandage";
            case "hi1":
                return "Drugz";
        }
    }
}
