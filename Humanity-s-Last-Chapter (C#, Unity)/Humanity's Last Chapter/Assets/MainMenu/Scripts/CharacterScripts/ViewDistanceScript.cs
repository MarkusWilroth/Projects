using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDistanceScript : MonoBehaviour
{
    public int ViewRadius;
    public Sprite OriginalTexture;

    private SpriteMask Mask;
    Color[] Colors;
    int Width, Height;
    float scaleX, scaleY;

    void Start()
    {
        //Get objects
        Mask = GameObject.FindGameObjectWithTag("Mask").GetComponent<SpriteMask>();

        //Extract color data once
        Colors = OriginalTexture.texture.GetPixels();

        //Store Mask dimensionns
        Width = Mask.sprite.texture.width;
        Height = Mask.sprite.texture.height;

        scaleX = Width / Mask.GetComponent<Collider2D>().bounds.size.x;
        scaleY = Height / Mask.GetComponent<Collider2D>().bounds.size.y;

        ClearMask();
    }

    void ClearMask()
    {
        Mask.sprite.texture.SetPixels(Colors);
        Mask.sprite.texture.Apply(false);
    }

    void Update()
    {
        //Get mouse coordinates
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Check if we hit the collider
        if (Mask.GetComponent<Collider2D>().OverlapPoint(transform.position))
        {
            //Normalize to the texture coodinates
            int x = (int)(((Mask.transform.position.x + transform.position.x) - (Mask.transform.position.x * 2 - Mask.transform.localScale.x)) * scaleX);
            int y = (int)(((Mask.transform.position.y + transform.position.y) - (Mask.transform.position.y * 2 - Mask.transform.localScale.y)) * scaleY);
            DrawOnMask(x, y, (int)(ViewRadius * scaleX));
        }
    }

    //This function will draw a circle onto the texture at position cx, cy with radius r
    public void DrawOnMask(int mouseX, int mouseY, int r)
    {
        int d;
        for (int x = 0; x <= r; x++)
        {
            d = (int)Mathf.Ceil(Mathf.Sqrt(r * r - x * x));
            for (int y = 0; y <= d; y++)
            {
                Mask.sprite.texture.SetPixel(mouseX + x, mouseY + y, new Color(1, 1, 1, 0));
                Mask.sprite.texture.SetPixel(mouseX + x, mouseY - y, new Color(1, 1, 1, 0));
                Mask.sprite.texture.SetPixel(mouseX - x, mouseY - y, new Color(1, 1, 1, 0));
                Mask.sprite.texture.SetPixel(mouseX - x, mouseY + y, new Color(1, 1, 1, 0));
            }
        }
        Mask.sprite.texture.Apply(false);
    }
}
