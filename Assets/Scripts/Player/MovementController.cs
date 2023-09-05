using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    private Rigidbody _rigidBody;
    private Transform _camera;
    private Vector3 _moveDirection, ignoreY = new(1, 0, 1);
    private float _accelerationTime = 0, _targetYaw = 0, _rotationSmoothness = 0.12f;
    private float _tempRotation;
    private bool _isSprinting = false;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    private float _dashDurationTimer = 0, _dashCooldownTimer = 0;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _playerGravity;
    private float _groundOffset = 0.7f, _groundRadius = 0.335f;
    private LayerMask _groundLayers;
    private bool _isGrounded = true;

    #region SETUP

    void OnEnable()
    {
        // TODO unsubscribe lambdas
        InputReader.moveEvent += (direction) => _moveDirection = new(direction.x, 0, direction.y);
        InputReader.sprintEvent += (active) => _isSprinting = active;
        InputReader.jumpEvent += Jump;
        InputReader.dashEvent += Dash;
    }

    void OnDisable()
    {
        InputReader.jumpEvent -= Jump;
        InputReader.dashEvent -= Dash;
    }

    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    #endregion

    void FixedUpdate()
    {
        UpdateSpeed();

        UpdateSmoothFalling();

        UpdateGrounded();
    }

    void Update()
    {        
        UpdateDash();
    }

    void UpdateSpeed()
    {
        var targetSpeed = _dashDurationTimer > 0 ? _dashSpeed : _isSprinting ? _sprintSpeed : _walkSpeed;
        var velocityXZ = Vector3.Scale(_rigidBody.velocity, ignoreY).magnitude;

        if (_moveDirection == Vector3.zero)
        {
            _accelerationTime = 0;
            targetSpeed = 0;
        }
        else
        {
            _targetYaw =
                Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg
                + _camera.eulerAngles.y;

            var currentYaw = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                _targetYaw,
                ref _tempRotation,
                _rotationSmoothness
            );
            
            transform.rotation = Quaternion.Euler(0, currentYaw, 0);
        }

        if (velocityXZ < targetSpeed)
        {
            velocityXZ = targetSpeed * _accelerationCurve.Evaluate(_accelerationTime);
            _accelerationTime += Time.deltaTime;
        }
        else velocityXZ = targetSpeed;

        var targetDirection = Quaternion.Euler(0, _targetYaw, 0) * Vector3.forward;
        _rigidBody.AddForce(targetDirection * velocityXZ, ForceMode.VelocityChange);
    }

    void UpdateSmoothFalling()
    {
        if (_rigidBody.velocity.y >= 0) return;

        _rigidBody.velocity += 0.01f * _playerGravity * Vector3.up;
    }

    void Jump()
    {
        if (!_isGrounded) return;
        
        var force = _jumpForce * _rigidBody.mass - _playerGravity;
        _rigidBody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    void UpdateGrounded()
    {
        var position = transform.position + Vector3.down * _groundOffset;
        _isGrounded = Physics.CheckSphere(
            position,
            _groundRadius,
            _groundLayers,
            QueryTriggerInteraction.Ignore
        );
    }

    void Dash()
    {
        if (_dashCooldownTimer > 0 || _dashDurationTimer > 0) return;

        _dashDurationTimer = _dashDuration;
    }

    void UpdateDash()
    {
        if (_dashDurationTimer > 0)
        {
            _dashDurationTimer -= Time.deltaTime;

            if (_dashDurationTimer < 0)
                _dashCooldownTimer = _dashCooldown;
        }
        else if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }
    }
}
