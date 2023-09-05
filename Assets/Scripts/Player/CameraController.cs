using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _focusPoint;

    [Header("Fine-tuning")]
    [SerializeField] private float _topClamp = 50;
    [SerializeField] private float _bottomClamp = -30;
    [SerializeField] private float _minDistance = 1;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _zoomPerTick = 1;
    [SerializeField] private float _zoomSmoothness = 0.2f;

    private Transform _camera;
    private Cinemachine3rdPersonFollow _cameraFollow;
    private Vector2 _mouseDelta;
    private Vector2 _mouseSpeedThreshold = new(0.112f, 0.189f);
    private float _targetYaw, _targetPitch, _targetDistance, _tempZoom;
    private float _threshold = 0.01f;
    private bool _isPanning = false;

    #region SETUP

    void OnEnable()
    {
        InputReader.lookEvent += (delta) => _mouseDelta = delta * _mouseSpeedThreshold;
        InputReader.panEvent += (active) => _isPanning = active;
        InputReader.zoomEvent += Zoom;
    }

    void OnDisable()
    {
        InputReader.zoomEvent -= Zoom;
    }

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("VirtualCamera").transform;
        _cameraFollow = _camera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        _targetDistance = _cameraFollow.CameraDistance;
        _targetYaw = _focusPoint.rotation.eulerAngles.y;
        // _targetPitch = transform.rotation.eulerAngles.x;
    }

    #endregion

    void Update()
    {
        UpdateRotation();

        UpdateZoom();
    }

    void UpdateRotation()
    {
        if (_isPanning)
        {
            Cursor.lockState = CursorLockMode.Locked;

            if (_mouseDelta.magnitude >= _threshold)
            {
                _targetYaw += _mouseDelta.x;
                _targetPitch -= _mouseDelta.y;
            }

            _targetYaw = _targetYaw % 360;
            _targetPitch = Mathf.Clamp(_targetPitch, _bottomClamp, _topClamp);
        }
        else Cursor.lockState = CursorLockMode.None;

        _focusPoint.rotation = Quaternion.Euler(_targetPitch, _targetYaw, 0);
    }

    void UpdateZoom()
    {
        _cameraFollow.CameraDistance = Mathf.SmoothDamp(
            _cameraFollow.CameraDistance,
            _targetDistance,
            ref _tempZoom,
            _zoomSmoothness
        );
    }

    void Zoom(float direction)
    {
        if (direction == 0) return;

        _targetDistance = Mathf.Clamp(
            _cameraFollow.CameraDistance + direction * _zoomPerTick,
            _minDistance,
            _maxDistance
        );
    }
}
