using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRTFilter : MonoBehaviour
{
    // Start is called before the first frame update

    private int Width;
    private int Height;

    private Color rayCol;
    private Ray2D CRTray;
    private int rayX;
    private int rayY;
    private Vector2 rayPosition;
    private Vector2 right;

    void Start()
    {

        Width = Screen.width;
        Height = Screen.height;
        rayX = 0;
        rayY = 0;
        right = new Vector2(0,1);
        rayPosition = new Vector2(rayX, rayY);
        CRTray = new Ray2D(rayPosition, right);

        rayX = 0;
        rayY = 0;

    }

    // Update is called once per frame
    void Update()
    {
        rayY++;
        setRay(rayY);
        drawRay(CRTray, Color.black, 1);

        if (rayY >= Height)
        {
            rayY = 0;
        }
    }
    void DrawCRT()
    {
        for (int i = 0; i < Height; i++)
        {
            setRay(i);
            drawRay(CRTray, Color.black, 0.9f);
        }
    }
    void setRay(int y)
    {
        //rayX = x;
        rayY = y;

        rayPosition = new Vector2(rayX, rayY);

        CRTray = new Ray2D(rayPosition, right);
    }

    void setColour(Color col, float alpha)
    {

    }

    void drawRay(Ray2D r, Color col, float alpha)
    {
        rayCol = col;
        rayCol.a = alpha;

        Debug.DrawLine(r.origin, r.direction, rayCol, 3.0f);
        print("Drawing Ray");
    }
}
