using UnityEngine;

public class SwordProjectile : Projectile
{
    void Start()
    {
        if (_damage == 0) _damage = 19;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
        }
    }

    public override void Init(Transform target, float dmg)
    {
        _damage = dmg;
    }
}
