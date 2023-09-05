using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Ability
{
    [SerializeField] private Projectile _projectile;

    public override void Activate(Transform origin, Transform target)
    {
        var p = Instantiate(_projectile, origin.position, origin.rotation);
        p.Init(target);
    }
}
