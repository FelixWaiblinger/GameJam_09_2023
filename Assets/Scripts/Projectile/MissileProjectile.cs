using UnityEngine;

public class MissileProjectile : Projectile
{
    [SerializeField] private BOOM _explosion;
    [SerializeField] private float _offsetMultiplier = 1;
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private AudioSource _audioSource;

    private Transform _target = null;
    private Vector3 _origin, _controlPoint, _targetPosition;
    private float _currentTime = 0;

    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.pitch = Random.Range(0.85f, 1.2f);
        _audioSource.volume = Random.Range(0.05f, 0.08f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger) return;
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<IDamagable>().TakeDamage(_damage);
            Explode();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Explode();
        }
    }

    void Update()
    {
        if (_target == null) { Destroy(gameObject); return; }
        if (_currentTime > _duration + 0.2f) { Explode(); return; }

        transform.position = CalculateBezierPoint(
            _currentTime / _duration,
            _origin,
            _target.position + Vector3.up,
            _controlPoint
        );

        _currentTime += Time.deltaTime;
    }

    void Explode()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 start, Vector3 end, Vector3 control)
    {
		var u = 1 - t;
		var uu = u * u;

		var point = uu * start;
		point += 2 * u * t * control;
		point += t * t * end;

		return point;
	}

    public override void Init(Transform target, float dmg)
    {
        this._damage = dmg;
        _target = target;
        _origin = transform.position - 1.5f * transform.forward;

        var rand = 5 * _offsetMultiplier * Random.insideUnitSphere;
        var offsetRandom = (rand.y > 0 ? rand : Vector3.Scale(rand, Vector3.down));
        var offsetX = Vector3.right * Random.Range(-3 * _offsetMultiplier, 3 * _offsetMultiplier);
        var offsetY = 3 * _offsetMultiplier * Vector3.up;
        var offsetZ = -5 * _offsetMultiplier * transform.forward;

        _controlPoint = _origin + offsetX + offsetZ + offsetY + offsetRandom;
    }
}
