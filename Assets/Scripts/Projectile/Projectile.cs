using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private LayerMask _damagables;
    [SerializeField] private LayerMask _breaking;
    [SerializeField] private float _damage;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == _breaking)
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == _damagables)
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
        }
    }

    public virtual void Init(Transform target) {}
}
