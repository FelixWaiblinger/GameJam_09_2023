using UnityEngine;

[CreateAssetMenu(fileName = "Inferno", menuName = "Ability/Inferno")]
public class Inferno : Ability
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private int _projectileNumber;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _targetLayer;

    public override void Activate(Transform origin, Transform target)
    {
        if (!FindEnemy(origin.position, out Transform enemy)) return;

        CreateMissiles(origin, enemy);
    }

    bool FindEnemy(Vector3 origin, out Transform enemy)
    {
        enemy = null;
        var enemies = Physics.OverlapSphere(
            origin,
            _detectionRadius,
            _targetLayer,
            QueryTriggerInteraction.Ignore
        );

        if (enemies.Length == 0) return false;
        
        var minDistance = float.MaxValue;
        foreach (Collider c in enemies)
        {
            var distance = Vector3.Distance(c.transform.position, origin);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemy = c.transform;
            }
        }

        return true;
    }

    async void CreateMissiles(Transform origin, Transform target)
    {
        for (int i = 0; i < _projectileNumber; i++)
        {
            if (!target) return;
            
            var p = Instantiate(_projectile, origin.position, origin.rotation);
            p.Init(target);
            await System.Threading.Tasks.Task.Delay(100);
        }
    }
}
