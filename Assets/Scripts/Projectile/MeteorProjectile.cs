using UnityEngine;

public class MeteorProjectile : Projectile
{
    [SerializeField] private BOOM _explosion;
    [SerializeField] private float _speed;

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;

        Debug.Log($"{other.gameObject.layer} == {LayerMask.NameToLayer("Environment")}");
        
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
        transform.position += Vector3.down * _speed * Time.deltaTime;
    }

    void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public override void Init(Vector3 target, float dmg)
    {
        _damage = dmg;
    }
}
