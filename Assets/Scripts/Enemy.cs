using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [Tooltip("Starting HP of this enemy")]
    [SerializeField] private int _maxHealth;
    [Tooltip("Amount of damage to apply with each attack")]
    [SerializeField] private int _attackDamage;
    [Tooltip("Attacks per second")]
    [SerializeField] private float _attackRate;
    [Tooltip("Maximum range to trigger an attack")]
    [SerializeField] private float _attackRange;
    [Tooltip("Travel speed of this enemy")]
    [SerializeField] private float _moveSpeed;

    private int _currentHealth;
    private float _attackCooldown;
    private Transform _playerTarget;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (!_playerTarget)
        {
            Patrol();
            return;
        }

        var distance = Vector3.Distance(_playerTarget.position, transform.position);

        if (distance < _attackRange) Attack();
        else MoveToTarget();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        _playerTarget = other.transform;
    }

    void Patrol()
    {
        // TODO
    }

    void Attack()
    {
        // TODO
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _playerTarget.position,
            _moveSpeed * Time.deltaTime
        );

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.LookRotation(_playerTarget.position - transform.position),
            1 // TODO adjust turn speed
        );
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0) Destroy(gameObject);
    }
}
