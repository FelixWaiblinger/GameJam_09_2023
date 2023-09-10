using UnityEngine;

[CreateAssetMenu(fileName = "Meteor", menuName = "Ability/Meteor")]
public class Meteor : Ability
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _range;

    public override void Activate(Transform origin, Transform target, float dmgMultiplier)
    {
        var distance = target.position - origin.position;
        var position = distance.magnitude > _range ?
                       origin.position + distance.normalized * _range :
                       target.position;

        ChannelBall(position, dmgMultiplier);
    }

    async void ChannelBall(Vector3 target, float dmgMultiplier)
    {            
        await System.Threading.Tasks.Task.Delay(600);

        var p = Instantiate(_projectile, target + 20 * Vector3.up, Quaternion.identity);
        p.Init(target, Damage * dmgMultiplier);
    }
}