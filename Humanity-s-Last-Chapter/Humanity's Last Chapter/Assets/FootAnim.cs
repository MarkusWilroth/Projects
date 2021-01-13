using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootAnim : MonoBehaviour
{
    float OffsetY = 0;
    public float OffsetX;
    float toAddY = 0.005f;
    public float StartOffetY = 0;
    // Start is called before the first frame update
    void Start()
    {
        OffsetX = transform.localPosition.x;
        StartOffetY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponentInParent<PersonalMovement>().moving)
        {
            transform.localPosition = new Vector3(OffsetX, 0, 0);
            return;
        }
        if (StartOffetY + OffsetY > 0.19f)
        {
            toAddY *= -1;
        }else if (StartOffetY + OffsetY < -0.1f)
        {
            toAddY *= -1;
        }

        OffsetY += toAddY;
        transform.localPosition = new Vector3(OffsetX, StartOffetY + OffsetY, 0);
    }
}
