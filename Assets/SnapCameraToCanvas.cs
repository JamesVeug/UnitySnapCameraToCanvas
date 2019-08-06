using UnityEngine;

/// <summary>
/// Snaps the Camera to the Canvas per frame in the editor or once when Awake is called.
/// https://github.com/JamesVeug/UnitySnapCameraToCanvas
/// </summary>
public class SnapCameraToCanvas : MonoBehaviour
{
    public Canvas canvas;
    public Camera camera;

    private void Awake()
    {
        SnapToCanvas();
    }

    private void SnapToCanvas()
    {
        camera.transform.rotation = canvas.transform.rotation;
        
        // Align camera to the correct x direction to laptop. 
        // | (• ◡•)| (❍ᴥ❍ʋ) - (Mathematical!)
        // 
        Vector3 canvasPos = canvas.transform.position;
        float x = canvasPos.x;
        float y = canvasPos.y;
        float z = canvasPos.z;

        float fov = camera.fieldOfView / 2.0f;
        float canvasHeight = ((RectTransform)canvas.transform).rect.height * canvas.transform.lossyScale.y;

        // SOH CAH (TOA)
        float opposite = canvasHeight / 2;
        float adjacent = opposite / Mathf.Tan(Mathf.Deg2Rad * fov);
        camera.transform.position =  new Vector3(x, y, z) - camera.transform.forward * adjacent;
        camera.farClipPlane = Mathf.Ceil(adjacent);
    }

    private void OnDrawGizmos()
    {
        // Snap
        SnapToCanvas();
        
        // Draw pretty lines for decoration
        Vector3 canvasPos = canvas.transform.position;
        
        float fov = camera.fieldOfView / 2.0f;
        float canvasHeight = ((RectTransform)canvas.transform).rect.height * canvas.transform.lossyScale.y;

        // SOH CAH (TOA)
        float opposite = canvasHeight / 2;
        float adjacent = opposite / Mathf.Tan(Mathf.Deg2Rad * fov);
        
        Gizmos.color = Color.green; // Vertical
        Gizmos.DrawLine(canvasPos, canvasPos + canvas.transform.up * opposite);
        
        Gizmos.color = Color.red; // Horizontal
        Gizmos.DrawLine(canvasPos, canvasPos + -canvas.transform.forward * adjacent);
        
        Gizmos.color = Color.blue; // Connects top of Vertical to tip of Horizontal
        Gizmos.DrawLine(canvasPos + canvas.transform.up * opposite, canvasPos + -canvas.transform.forward * adjacent);
    }
}
