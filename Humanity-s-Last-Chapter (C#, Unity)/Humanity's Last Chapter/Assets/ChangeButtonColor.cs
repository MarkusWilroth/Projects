using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeButtonColor : MonoBehaviour
{
    // Start is called before the first frame update
    Image color;
    void Start()
    {
        color = GetComponent<Image>();
        color.GetComponent<Image>().color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }

    public void ChangeColor()
    {
        color.GetComponent<Image>().color = Color.green;
    }
}
