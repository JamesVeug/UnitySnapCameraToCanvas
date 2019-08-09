using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCanvasSizeToCamera : MonoBehaviour
{
    public bool ScaleToSetSize;
    public Vector2 SetSize = new Vector2(1920, 1080);
    
    public RectTransform Canvas;
    public Camera Camera;

    private void Awake()
    {
        Resize();
    }

    private void Resize()
    {
        float width = Camera.pixelWidth;
        float height = Camera.pixelHeight;

        if (ScaleToSetSize)
        {
            // Scale the Canvas to the same ratio as the camera
            float ratio = 1;
            if (width > height)
                ratio = width < SetSize.x ? SetSize.x / width : width / SetSize.x;
            else
                ratio = height < SetSize.y ? SetSize.y / height : height / SetSize.y;

            width *= ratio;
            height *= ratio;

            // Scale the Canvas to fit the Size we want to fit
            float scale = 1;
            if (width > SetSize.x)
                scale = width / SetSize.x;
            else if (height > SetSize.y)
                scale = height / SetSize.y;

            width /= scale;
            height /= scale;
        }

        Canvas.sizeDelta = new Vector2(width, height);
    }

    private void OnDrawGizmos()
    {
        Resize();
    }
}
