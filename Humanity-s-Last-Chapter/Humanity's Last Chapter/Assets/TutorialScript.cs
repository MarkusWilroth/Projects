using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    SpriteRenderer imag;
    public Sprite[] image;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = image[i]; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && i != (image.Length - 1))
        {
            i++;
            this.GetComponent<SpriteRenderer>().sprite = image[i];
        }
        
    }
}
