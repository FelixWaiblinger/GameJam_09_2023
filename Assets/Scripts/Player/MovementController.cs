using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Control")]
    [SerializeField] private BoolEventChannel _combatEvent;
    [SerializeField] private FloatEventChannel _rootEvent;
    private float _combatTimer, _rootTimer;

    [Header("Movement")]
    [SerializeField] private Transform _visuals;
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    private Rigidbody _rigidBody;
    private Transform _camera;
    private Vector2 _mousePosition;
    private Vector3 _mouseInWorld, _moveDirection, ignoreY = new(1, 0, 1);
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
    [SerializeField] private float _gravityMultiplier;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private float _groundOffset = 0.5f;
    private float _groundRadius;
    private bool _isGrounded = true;

    #region SETUP

    void OnEnable()
    {
        InputReader.jumpEvent += Jump;
        InputReader.dashEvent += Dash;
        InputReader.moveEvent += (direction) => _moveDirection = new(direction.x, 0, direction.y);
        InputReader.sprintEvent += (active) => _isSprinting = _combatTimer <= 0 && active;
        InputReader.mousePosEvent += (position) => _mousePosition = position;

        _combatEvent.OnBoolEventRaised += (active) => _combatTimer = active ? 10 : 0;
        _rootEvent.OnFloatEventRaised += (duration) => _rootTimer = duration;
    }

    void OnDisable()
    {
        InputReader.jumpEvent -= Jump;
        InputReader.dashEvent -= Dash;
    }

    void Awake()
    {
        _camera = Camera.main.transform;
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _groundRadius = 1.1f * _groundOffset;
    }

    #endregion

    void FixedUpdate()
    {
        SmoothFalling();

        GroundCheck();

        if (_rootTimer > 0) return;

        if (_combatTimer > 0) CombatMove();
        else ExploreMove();
    }

    void Update()
    {        
        UpdateTimers();

        if (CameraTools.GetScreenToWorld(_mousePosition, out Vector3 worldPos))
            _mouseInWorld = worldPos;
    }

    #region MOVEMENT

    void ExploreMove()
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
                _visuals.eulerAngles.y,
                _targetYaw,
                ref _tempRotation,
                _rotationSmoothness
            );
            
            _visuals.rotation = Quaternion.Euler(0, currentYaw, 0);
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

    void CombatMove()
    {
        CombatRotation();

        if (_moveDirection == Vector3.zero)
        {
            _accelerationTime = 0;
            return;
        }

        var targetSpeed = _dashDurationTimer > 0 ? _dashSpeed : _walkSpeed;
        var velocityXZ = targetSpeed * _accelerationCurve.Evaluate(_accelerationTime);
        var targetDirection = Vector3.Scale(_camera.rotation * _moveDirection, ignoreY);

        _rigidBody.AddForce(targetDirection * velocityXZ, ForceMode.VelocityChange);
        _accelerationTime += Time.deltaTime;
    }

    void CombatRotation()
    {
        var direction = (_mouseInWorld - transform.position).normalized;

        _targetYaw = Quaternion.LookRotation(direction).eulerAngles.y;

        var currentYaw = Mathf.SmoothDampAngle(
            _visuals.eulerAngles.y,
            _targetYaw,
            ref _tempRotation,
            _rotationSmoothness
        );
            
        _visuals.rotation = Quaternion.Euler(0, currentYaw, 0);
    }

    void SmoothFalling()
    {
        if (_rigidBody.velocity.y >= 0) return;

        _rigidBody.AddForce(Physics.gravity * _gravityMultiplier, ForceMode.Acceleration);
    }

    void Jump()
    {
        if (!_isGrounded) return;
        
        var force = _jumpForce * _rigidBody.mass;
        _rigidBody.AddForce(Vector3.up * force, ForceMode.Impulse);

        _isGrounded = false;
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(
            transform.position + Vector3.down * _groundOffset,
            _groundRadius,
            _groundLayers,
            QueryTriggerInteraction.Ignore
        );
    }

    void Dash()
    {
        if (_combatTimer <= 0) return;

        if (_dashCooldownTimer > 0 || _dashDurationTimer > 0) return;

        _dashDurationTimer = _dashDuration;
    }

    #endregion

    void UpdateTimers()
    {
        var decrement = Time.deltaTime;

        if (_dashDurationTimer > 0)
        {
            _dashDurationTimer -= decrement;

            if (_dashDurationTimer < 0)
                _dashCooldownTimer = _dashCooldown;
        }
        else if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= decrement;
        }

        if (_rootTimer > 0) _rootTimer -= decrement;

        if (_combatTimer > 0)
        {
            _combatTimer -= decrement;
            if (_combatTimer < 0) _combatEvent.RaiseBoolEvent(false);
        }
    }
}
