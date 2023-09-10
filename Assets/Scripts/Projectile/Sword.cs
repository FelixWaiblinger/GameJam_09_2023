using UnityEngine;

public class Sword : Projectile
{
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
