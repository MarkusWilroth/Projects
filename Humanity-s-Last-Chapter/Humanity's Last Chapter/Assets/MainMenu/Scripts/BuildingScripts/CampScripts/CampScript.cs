using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampScript : MonoBehaviour
{
    public GameObject character;
    public GameObject UIscreenthingy;
    public GameObject UIforCamp;
    public GameObject Controller;

    
    private List<GameObject> campRoster = new List<GameObject>();
    private bool existingRoster = false;

    float offsetX = 1, offsetY = -1.5f;
   
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCharacter(string name, Vector3 pos)
    {
        GameObject go = (GameObject)Instantiate(character);
        go.transform.SetParent(UIforCamp.transform, false);
        //go.GetComponent<RectTransform>().SetParent(UIforCamp.transform, false);
        go.GetComponent<RectTransform>().position = pos;
        go.GetComponent<AddToPlayerRoster>().UIParent = UIscreenthingy;
        go.GetComponent<Button>().onClick.AddListener(go.GetComponent<AddToPlayerRoster>().AddToPlayer);
        go.GetComponentInChildren<Text>().text = name;
        campRoster.Add(go);
    }

    public void CreateRoster()
    {
        if (!existingRoster)
        {
            int rand = Random.Range(2, 5);
            for (int i = 0; i < rand; i++)
            {                                                   //Added transform.localPosition.x and y here to make the position dynamic. Y-pos appears to be at the bottom of the camp-window, though.
                CreateCharacter("placeholder " + i, new Vector3(transform.localPosition.x + offsetX, -1 * transform.localPosition.y - i + offsetY,0)/*310, 550 - (i * 75), 0)*/);
            }
            existingRoster = true;
        }
    }

    public void SetRoster(List<GameObject> playerRoster)
    {
        //this.playerRoster = playerRoster;
    }

    static public void addToPlayer(GameObject character)
    {
        //character.transform.SetParent(UIscreenthingy.transform);
    }
}
