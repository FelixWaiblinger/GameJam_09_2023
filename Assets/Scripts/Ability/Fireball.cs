using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/Fireball")]
public class Fireball : Ability
{
    [SerializeField] private Projectile _projectile;

    public override void Activate(Transform origin, Transform target, float dmgMultiplier)
    {
        ChannelBall(origin, target, dmgMultiplier);
    }

    async void ChannelBall(Transform origin, Transform target, float dmgMultiplier)
    {
        if (!target) return;
            
        await System.Threading.Tasks.Task.Delay(600);

        var p = Instantiate(_projectile, origin.position, origin.rotation);
        p.Init(target, Damage * dmgMultiplier);
    }
}
