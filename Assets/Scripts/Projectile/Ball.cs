using UnityEngine;

public class Ball : Projectile
{
    [SerializeField] private BOOM _explosion;
    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private Vector3 _targetDirection, ignoreY = new(1, 0, 1);
    private float _travelDistance;

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            Explode();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Explode();
        }
    }

    void Update()
    {
        if (_travelDistance > _range) { Explode(); return; }

        var distance = _speed * Time.deltaTime;
        transform.position += _targetDirection * distance;
        _travelDistance += distance;
    }

    void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void Init(Transform target)
    {
        _targetDirection = Vector3.Scale(target.position - transform.position, ignoreY).normalized;
    }
}
