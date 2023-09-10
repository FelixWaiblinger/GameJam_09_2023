using UnityEngine;

[CreateAssetMenu(fileName = "Fireball", menuName = "Ability/Fireball")]
public class Fireball : Ability
{
    [SerializeField] private Projectile _projectile;

    public override void Activate(Transform origin, Transform target)
    {
        ChannelBall(origin, target);
    }

    async void ChannelBall(Transform origin, Transform target)
    {
        if (!target) return;
            
        await System.Threading.Tasks.Task.Delay(600);

        var p = Instantiate(_projectile, origin.position, origin.rotation);
        p.Init(target);
    }
}
