using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask _damagables;
    [SerializeField] protected LayerMask _breaking;
    protected float _damage;

    public abstract void Init(Transform target, float dmg);
}
