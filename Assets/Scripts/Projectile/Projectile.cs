using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask _damagables;
    [SerializeField] protected LayerMask _breaking;
    protected float _damage;

    public virtual void Init(Transform target, float dmg) {}

    public virtual void Init(Vector3 target, float dmg) {}
}
