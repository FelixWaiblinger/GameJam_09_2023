using UnityEngine;

[CreateAssetMenu(fileName = "Inferno", menuName = "Ability/Inferno")]
public class Inferno : Ability
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _projectileNumber;

    public override void Activate(Transform origin, Transform target)
    {
        for (int i = 0; i < _projectileNumber; i++)
        {
            var p = Instantiate(_projectile, origin.position, origin.rotation);
            p.Init(target);
        }
    }
}
