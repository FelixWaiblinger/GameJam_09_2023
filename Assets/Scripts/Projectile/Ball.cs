using UnityEngine;

public class Ball : Projectile
{
    [SerializeField] private float _speed;
    [SerializeField] private float _range;

    private Vector3 _targetDirection, ignoreY = new(1, 0, 1);
    private float _travelDistance;

    void Update()
    {
        if (_travelDistance > _range) 
        {
            Destroy(gameObject);
        }

        var distance = _speed * Time.deltaTime;
        transform.position += _targetDirection * distance;
        _travelDistance += distance;
    }

    public override void Init(Transform target, float dmg)
    {   
        _damage = dmg;
        _targetDirection = Vector3.Scale(target.position - transform.position, ignoreY).normalized;
    }
}
