using UnityEngine;
using Cinemachine;

// danger zone
// using System.Runtime.InteropServices;

public class CameraController : MonoBehaviour
{
    // danger zone
    // [DllImport("user32.dll")]
    // static extern bool SetCursorPos(int X, int Y);

    [SerializeField] private Transform _focusPoint;

    [Header("Fine-tuning")]
    [SerializeField] private float _topClamp = 50;
    [SerializeField] private float _bottomClamp = -30;
    [SerializeField] private float _panSpeed = 1;
    [SerializeField] private float _minDistance = 2;
    [SerializeField] private float _maxDistance = 20;
    [SerializeField] private float _zoomPerTick = 1;
    [SerializeField] private float _zoomSmoothness = 0.2f;

    private Transform _camera;
    private CinemachineTransposer _cameraFrame;
    private Vector2 _mousePos, _bla, _mouseDelta, _mouseSpeedThreshold = new(0.112f, 0.189f);
    private Vector3 _targetDistance,_tempZoom;
    private float _targetYaw, _targetPitch, _panThreshold = 0.01f;
    private bool _isPanning = false;

    #region SETUP

    void OnEnable()
    {
        InputReader.zoomEvent += SetZoomLevel;
        InputReader.mousePosEvent += (position) => _mousePos = position;
        InputReader.lookEvent += (delta) => _mouseDelta = delta * _mouseSpeedThreshold; // maybe remove this threshold
        InputReader.panEvent += (active) => {
            _isPanning = active;
            // if (_isPanning) _bla = _mousePos;
            // else SetCursorPos((int)_bla.x, (int)_bla.y);
        };
    }

    void OnDisable()
    {
        InputReader.zoomEvent -= SetZoomLevel;
    }

    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("VirtualCamera").transform;
        _cameraFrame = _camera.GetComponent<CinemachineVirtualCamera>()
                              .GetCinemachineComponent<CinemachineTransposer>();

        _targetDistance = _cameraFrame.m_FollowOffset;
        _targetYaw = _focusPoint.rotation.eulerAngles.y;
    }

    #endregion

    void Update()
    {
        UpdatePan();

        UpdateZoom();
    }

    void UpdatePan()
    {
        if (_isPanning)
        {
            // Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;

            if (_mouseDelta.magnitude >= _panThreshold)
            {
                _targetYaw += _mouseDelta.x;
                _targetPitch -= _mouseDelta.y;
            }

            _targetYaw = _targetYaw % 360;
            _targetPitch = Mathf.Clamp(_targetPitch, _bottomClamp, _topClamp);
        }
        else
        {
            // Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        _focusPoint.rotation = Quaternion.RotateTowards(
            _focusPoint.rotation,
            Quaternion.Euler(_targetPitch, _targetYaw, 0),
            _panSpeed * Time.deltaTime
        );
    }

    void UpdateZoom()
    {
        _cameraFrame.m_FollowOffset = Vector3.SmoothDamp(
            _cameraFrame.m_FollowOffset,
            _targetDistance,
            ref _tempZoom,
            _zoomSmoothness
        );
    }

    void SetZoomLevel(float direction)
    {
        if (direction == 0) return;

        _targetDistance = new(0, 0, Mathf.Clamp(
            _cameraFrame.m_FollowOffset.z + direction * _zoomPerTick,
            _minDistance,
            _maxDistance
        ));
    }
}
