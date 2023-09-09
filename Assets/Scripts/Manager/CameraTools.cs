using UnityEngine;

// Only one CameraTools in any project, please!
public class CameraTools : MonoBehaviour
{
    [SerializeField] private LayerMask _environmentLayer;
    private static LayerMask _environment;
    private static Camera _camera;

    void Start()
    {
        _camera = Camera.main.GetComponent<Camera>();
        _environment = _environmentLayer;
    }

    public static bool GetScreenToWorld(Vector2 mousePos, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;
        if (!_camera) return false;

        var ray = _camera.ScreenPointToRay(new(mousePos.x, mousePos.y, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, _environment))
        {
            worldPos = hit.point;
            return true;
        }

        return false;
    }
}
