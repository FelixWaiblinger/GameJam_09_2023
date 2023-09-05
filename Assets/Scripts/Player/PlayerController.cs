using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cooldown
{
    Attack, Primary, Secondary, Dash, All
}

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameData _gameData;
    private Transform _camera;

    [Header("Movement")]
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _sprintSpeed;
    private Rigidbody _rigidBody;
    private Vector3 _moveDirection, ignoreY = new(1, 0, 1);
    private float _accelerationTime = 0, _targetYaw = 0, _rotationSmoothness = 0.12f;
    private float _tempRotation;
    private bool _isSprinting = false;

    [Header("Dash")]
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;
    private float _dashDurationTimer = 0;

    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _playerGravity;
    private float _groundOffset = 0.7f, _groundRadius = 0.335f;
    private LayerMask _groundLayers;
    private bool _isGrounded = true;

    [Header("Abilities")]
    [SerializeField] private Ability _attackSlot;
    [SerializeField] private Ability _primarySlot;
    [SerializeField] private Ability _secondarySlot;
    [SerializeField] private Transform _abilityOrigin;
    private Transform _target;
    private Dictionary<Cooldown, float> _cooldownTimers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 0},
        {Cooldown.Primary, 0},
        {Cooldown.Secondary, 0}, 
        {Cooldown.Dash, 0}
    };
    private Dictionary<Cooldown, float> _cooldownMultipliers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 1},
        {Cooldown.Primary, 1},
        {Cooldown.Secondary, 1}, 
        {Cooldown.Dash, 1},
        {Cooldown.All, 1}
    };

    #region SETUP

    void OnEnable()
    {
        // TODO unsubscribe lambdas
        InputReader.moveEvent += (direction) => _moveDirection = new(direction.x, 0, direction.y);
        InputReader.sprintEvent += (active) => _isSprinting = active;
        InputReader.jumpEvent += Jump;
        InputReader.dashEvent += Dash;
        InputReader.attackSlotEvent += Attack;
        InputReader.primarySlotEvent += Primary;
        InputReader.secondarySlotEvent += Secondary;
    }

    void OnDisable()
    {
        InputReader.jumpEvent -= Jump;
        InputReader.dashEvent -= Dash;
        InputReader.attackSlotEvent -= Attack;
        InputReader.primarySlotEvent -= Primary;
        InputReader.secondarySlotEvent -= Secondary;
    }

    void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        
        // load chosen abilities
        _attackSlot = _gameData.Attack;
        _primarySlot = _gameData.Primary;
        _secondarySlot = _gameData.Secondary;
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
        UpdateCooldowns();

        UpdateDashDuration();
    }

    #region MOVEMENT

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

    #endregion

    #region ABILITY

    void Attack()
    {
        if (!TryAddCooldown(Cooldown.Attack)) return;

        // _attackSlot.Activate(_abilityOrigin, _target);
    }

    void Primary()
    {
        if (!TryAddCooldown(Cooldown.Primary)) return;

        _primarySlot.Activate(_abilityOrigin, _target);
    }

    void Secondary()
    {
        if (!TryAddCooldown(Cooldown.Secondary)) return;

        _secondarySlot.Activate(_abilityOrigin, _target);
    }

    void Dash()
    {
        if (!TryAddCooldown(Cooldown.Dash) || _dashDurationTimer > 0) return;

        _dashDurationTimer = _dashDuration;
    }

    bool TryAddCooldown(Cooldown name)
    {
        if (_cooldownTimers[name] > 0) return false;

        float cd = 0;
        switch (name)
        {
            case Cooldown.Attack: cd = _attackSlot.Cooldown; break;
            case Cooldown.Primary: cd = _primarySlot.Cooldown; break;
            case Cooldown.Secondary: cd = _secondarySlot.Cooldown; break;
            case Cooldown.Dash: cd = _dashCooldown; break;
        }

        _cooldownTimers[name] = cd * _cooldownMultipliers[name] * _cooldownMultipliers[Cooldown.All];

        return true;
    }

    void UpdateCooldowns()
    {
        var cdList = new List<Cooldown>(_cooldownTimers.Keys);
        foreach (Cooldown cd in cdList)
        {
            if (_cooldownTimers[cd] < 0) continue;
            _cooldownTimers[cd] -= Time.deltaTime;
        }
    }

    void UpdateDashDuration()
    {
        if (_dashDurationTimer > 0)
        {
            _dashDurationTimer -= Time.deltaTime;

            if (_dashDurationTimer < 0)
                _cooldownTimers[Cooldown.Dash] = _dashCooldown;
        }
    }

    #endregion
}
