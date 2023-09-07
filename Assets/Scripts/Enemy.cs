using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackRate;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _moveSpeed;

    private float _currentHealth, _attackCooldown;
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

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0) Destroy(gameObject);
    }
}
