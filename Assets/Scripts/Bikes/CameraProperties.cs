using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProperties : MonoBehaviour
{
    public static Vector3 mousePosition => mainCamera.ScreenToWorldPoint(Input.mousePosition);

    private static Camera _mainCamera;
    public static Camera mainCamera
    {
        get
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;
            return _mainCamera;
        }
    }

    public static Vector3 PercentToWorldPoint(Vector2 percent)
    {
        percent += Vector2.one;
        percent /= 2;
        percent *= new Vector2(Screen.width, Screen.height);
        return mainCamera.ScreenToWorldPoint(percent);
    }
}
