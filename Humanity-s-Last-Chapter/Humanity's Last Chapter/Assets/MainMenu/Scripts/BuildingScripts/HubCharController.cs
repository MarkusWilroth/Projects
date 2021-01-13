using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HubCharController : MonoBehaviour
{
    private List<GameObject> playerRoster = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddToRoster(GameObject go)
    {
        int i = 0;
        playerRoster.Add(go);
        go.GetComponent<Button>().onClick.AddListener(go.GetComponent<Stats>().BringUpStats);
        foreach (GameObject character in playerRoster)
        {
            i++;                                  //Removed the off-sets here, because I think they should only be in the CampScript (maybe not?) YES IT IS NEEDED
            character.GetComponent<RectTransform>().position = new Vector3(character.transform.parent.position.x - 0.6f,
                                                                            (character.transform.parent.position.y + 3.3f) - (i * 1), 0);
        }
    }

    public void CloseAllWindows()
    {
        foreach (GameObject character in playerRoster)
        {
            character.GetComponent<Stats>().characterUI.SetActive(false);
        }
    }
}
