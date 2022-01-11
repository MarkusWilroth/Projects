using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySlotScript : MonoBehaviour
{
    public KeyCode BoundKey;
    public GameObject AttachedAbility;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (AttachedAbility == null)
        //    return;
        if (Input.GetKeyDown(BoundKey))
        {
            //Debug.Log("" + AttachedAbility.name);
            Activate();
        }
    }

    public void Activate()
    {
        if (AttachedAbility == null)
            return;
        //Debug.Log(BoundKey + " Activated");
        AttachedAbility.GetComponent<AbilityScript>().Activate();
    }
}
