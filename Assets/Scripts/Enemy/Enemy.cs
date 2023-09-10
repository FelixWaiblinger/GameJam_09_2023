using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private VoidEventChannel _enemyDeathEvent;

    [Header("Movement")]
    [SerializeField] private Transform _visuals;
    [SerializeField] private AnimationCurve _accelerationCurve;
    [SerializeField] private float _moveSpeed;
    
    [Header("Stats")]
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _attackRange;

    private Rigidbody _rigidBody;
    private Transform _playerTarget;
    private Vector3 ignoreY = new(1, 0, 1);
    private float _currentHealth, _attackCooldown, _accelerationTimer;

    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _currentHealth = _maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        _playerTarget = other.transform;
    }

    void Update()
    {
        if (!_playerTarget) { Patrol(); return; }
        

        var distance = Vector3.Distance(_playerTarget.position, transform.position);
        if (distance > _attackRange) MoveToTarget();
        else Attack();

        _animator.SetFloat("velocity", _rigidBody.velocity.magnitude);

        UpdateTimers();
    }

    #region MOVEMENT

    void Patrol()
    {
        // TODO
    }

    void MoveToTarget()
    {
        var direction = Vector3.Scale(_playerTarget.position - transform.position, ignoreY).normalized;
        var force = _moveSpeed * _accelerationCurve.Evaluate(_accelerationTimer);

        _visuals.rotation = Quaternion.LookRotation(direction);
        _rigidBody.AddForce(force * direction, ForceMode.Acceleration);
        _accelerationTimer += Time.deltaTime;
    }

    #endregion

    void Attack()
    {
        _visuals.rotation = Quaternion.RotateTowards(
            _visuals.rotation,
            Quaternion.LookRotation(_playerTarget.position - transform.position),
            10
        );
        
        if (_attackCooldown > 0) return;

        _animator.SetTrigger("attack");
        _attackCooldown = 1 / _attackRate;
        _accelerationTimer = 0;
    }

    public void TakeDamage(float amount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Destroy(gameObject, 3f);
            _enemyDeathEvent.RaiseVoidEvent();
            _animator.SetTrigger("death");
        }
    }

    void UpdateTimers()
    {
        var duration = Time.deltaTime;

        if (_attackCooldown > 0) _attackCooldown -= duration;
    }
}
