using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translator : MonoBehaviour
{
    //public float screenDimensionX;
    //public float screenDimensionY;

    public float inGameCanvasSizeX = 1000; //reference size
    public float inGameCanvasSizeY = 800; //reference size

    public float controlEditorCanvasSizeX = 800; //reference size
    public float controlEditorCanvasSizeY = 600; //reference size

    /*void Start()
    {
        screenDimensionX = Screen.width;
        screenDimensionY = Screen.height;
    }*/

    public Vector3 realPosition(float currentX, float currentY){
        float scaleX = inGameCanvasSizeX / controlEditorCanvasSizeX;
        float scaleY = inGameCanvasSizeY / controlEditorCanvasSizeY;

        float translatedX = currentX * scaleX;
        float translatedY = currentY * scaleY;

        Debug.Log("scaledX: " + scaleX + " scaledY: " + scaleY);
        Debug.Log("translatedX: " + translatedX + " translatedY: " + translatedY);

        return new Vector3(translatedX, translatedY, 0);
    }
}
