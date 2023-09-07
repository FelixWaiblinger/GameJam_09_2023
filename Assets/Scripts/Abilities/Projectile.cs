using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private float _damage;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }

    public abstract void Init(Transform target);
}
