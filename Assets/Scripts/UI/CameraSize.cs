using UnityEngine;

[ExecuteInEditMode]
public class CameraSize : MonoBehaviour
{
    public float targetWidth = 768;
    public float pixelsToUnits;

    private Camera camera;

    private void OnEnable()
    {
        camera = Camera.main;
    }

    private void LateUpdate()
    {
        int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);

        camera.orthographicSize = height / pixelsToUnits / 2;
    }
}
