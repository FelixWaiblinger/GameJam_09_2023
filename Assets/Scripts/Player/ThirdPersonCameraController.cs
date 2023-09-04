using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonCameraController : MonoBehaviour {

    //Inputs
    private ThirdPersonInputs _input;
#if ENABLE_INPUT_SYSTEM
    private PlayerInput _playerInput;
#endif
    private bool _IsCurrentDeviceMouse {
        get {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }

    private InputAction look;
    private InputAction _rightClick;
    private InputAction _mouseScrollY;

    //References Camera
    [Header("Camera")]
    [Space(5)]
    [Tooltip("Main Camera Target - Leave Public as Reference")]
    public GameObject CinemachineCameraTarget;

    [Tooltip ("Is the Camera Currently Locked, meaning it cant Move")]
    public bool LockCameraPosition = false;

    [Tooltip("How far in degrees can you _move the camera up")]
    public float TopClamp = 50.0f;

    [Tooltip("How far in degrees can you _move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degreed to override the camera. e.G for tuning the Camera when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("The Maximum Distance the Camera can go from the Player")]
    public float MaxCameraDistance = 20f;

    [Tooltip("The Closest Distance the Camera can get to the Player")]
    public float MinCameraDistance = 1f;

    private GameObject _mainCamera;
    private const float _threshold = 0.01f;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;

    private string _cameraSettingsTag = "VirtualCameraSettings";
    private CinemachineVirtualCamera _cinemachineVirtualCameraSettings;

    //References Mouse/Cursor/Scroll
    [Header("Mouse Cursor Settings")]
    [Space(5)]
    public bool IsCursorLocked = false;

    [Tooltip("MouseSpeed on the X Axis")]
    [Range(0.00f, 0.40f)]
    [SerializeField] private float _mouseSpeedX;

    [Tooltip("MouseSpeed on the Y Axis")]
    [Range(0.00f, 0.40f)]
    [SerializeField] private float _mouseSpeedY;

    [Tooltip("The Distance the Camera Scrolls per MouseScroll-Tick")]
    [SerializeField] private float _scrollDistancePerTick;

    private bool _isRightClickHeld = false;
    private float _targetCameraDistance;



    private void Awake() {
        if (_mainCamera == null) {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if (_cinemachineVirtualCameraSettings == null) {
            GameObject virtualCameraObject = GameObject.FindWithTag(_cameraSettingsTag);
            _cinemachineVirtualCameraSettings = virtualCameraObject.GetComponent<CinemachineVirtualCamera>();//FindObjectOfType<CinemachineVirtualCamera>();
        } else {
            Debug.Log("There is a big Problem with the Tag '" + _cameraSettingsTag + "'");
        }
        _input = new ThirdPersonInputs();
    }

    private void Start() {
        //Playerinput
#if ENABLE_INPUT_SYSTEM
        _playerInput = GetComponent<PlayerInput>();
#endif
        //Camera Rotation Translation to Euler
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

        //GetComponent der CameraSettings for: Scroll
        _cinemachineVirtualCameraSettings.GetComponent<CinemachineVirtualCamera>();
                        
        //Set Initial Floats for: LookAroundSpeed
        _mouseSpeedX = 0.112f;
        _mouseSpeedY = 0.189f;

        //Grab initial Cameradistance to Player to Enable SmoothScrolling
        
        _targetCameraDistance = _cinemachineVirtualCameraSettings.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;
    }

    private void OnEnable() {
        _input.PlayerDefault.Enable();
        Debug.Log(_input.PlayerDefault);
        look = _input.PlayerDefault.Look;
        _rightClick = _input.PlayerDefault.Rightclick;
        _mouseScrollY = _input.PlayerDefault.MouseScrollY;
        _mouseScrollY.performed += OnMouseScrollPerform;
        _rightClick.started += OnRightClickStarted;
        _rightClick.canceled += OnRightClickReleased;
    }

    private void OnRightClickStarted(InputAction.CallbackContext obj) {
        _isRightClickHeld = true;
    }

    private void OnDisable() {
        _input.PlayerDefault.Disable();
        _mouseScrollY.performed -= OnMouseScrollPerform;
        _rightClick.started -= OnRightClickStarted;
        _rightClick.canceled -= OnRightClickReleased;

    }

    private void OnRightClickReleased(InputAction.CallbackContext obj) {
        _isRightClickHeld = false;
    }

    private void Update() {

        HandleScroll();

    }

    private void LateUpdate() {
        //Debug.Log(_isRightClickHeld);
        
        CameraRotation();

    }

    private void CameraRotation() {

        if (_IsCurrentDeviceMouse && _isRightClickHeld) {
            SetCursorLockedState(true);
            Vector2 lookInput = new Vector2(look.ReadValue<Vector2>().x * _mouseSpeedX, look.ReadValue<Vector2>().y * _mouseSpeedY);

            //Debug.Log(lookInput);

            // if there is an input and camera position is not fixed
            if (lookInput.sqrMagnitude >= _threshold && !LockCameraPosition) {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = _IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += lookInput.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += lookInput.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        } else {
            SetCursorLockedState(false);
        }
        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax) {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void SetCursorLockedState(bool newState) {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    //Scroll-Handling
    private void OnMouseScrollPerform(InputAction.CallbackContext obj) {
        //expected Value is 120 or -120 or 0
        float givenScrollValue = obj.ReadValue<float>();

        if (givenScrollValue == 0) return;

        _targetCameraDistance += Mathf.Sign(givenScrollValue) * _scrollDistancePerTick;

        _targetCameraDistance = Mathf.Clamp(_targetCameraDistance, MinCameraDistance, MaxCameraDistance);
        /*if (_targetCameraDistance > MaxCameraDistance) _targetCameraDistance = MaxCameraDistance;
        if (_targetCameraDistance < MinCameraDistance) _targetCameraDistance = MinCameraDistance;*/
    }

    private float _smoothDampVelocity = 1f;
    private void HandleScroll() {

        float currentCameraDistance = _cinemachineVirtualCameraSettings.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance;

        float cameraDistance = Mathf.SmoothDamp(currentCameraDistance, _targetCameraDistance, ref _smoothDampVelocity, .2f);



        _cinemachineVirtualCameraSettings.GetCinemachineComponent<Cinemachine3rdPersonFollow>().CameraDistance = cameraDistance;

    }



}
