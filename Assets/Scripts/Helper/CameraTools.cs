using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTools : MonoBehaviour
{
    private static Camera _camera;

    void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
    }

    public static bool GetScreenToWorld(Vector2 mousePos, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;
        if (!_camera) return false;

        var ray = _camera.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hit, LayerMask.NameToLayer("Environment")))
        {
            worldPos = hit.point;
            return true;
        }

        return false;
    }
}
