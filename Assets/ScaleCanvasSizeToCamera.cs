using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCanvasSizeToCamera : MonoBehaviour
{
    public RectTransform Canvas;
    public Camera Camera;

    private void OnDrawGizmos()
    {
        Canvas.sizeDelta = new Vector2(Camera.pixelWidth, Camera.pixelHeight);
    }
}
