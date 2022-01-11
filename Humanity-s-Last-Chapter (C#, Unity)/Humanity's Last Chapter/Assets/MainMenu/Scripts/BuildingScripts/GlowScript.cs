using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowScript : MonoBehaviour
{
    GameObject Child;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Glow"))
                Child = child.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            Child.gameObject.SetActive(true);
        }
        else if (Child.activeSelf)
        {
            Child.gameObject.SetActive(false);
        }
    }
}
