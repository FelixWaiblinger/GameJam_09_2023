using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/Fireball")]
public class Fireball : Ability
{
    [SerializeField] private Projectile _projectile;

    public override void Activate(Transform origin, Transform target, float dmgMultiplier)
    {
        var p = Instantiate(_projectile, origin.position, origin.rotation);
        p.Init(target, Damage * dmgMultiplier);
    }

    
}
