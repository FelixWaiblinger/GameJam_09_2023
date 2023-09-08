using UnityEngine;

public class Missile : Projectile
{
    [SerializeField] private float _offsetMultiplier = 1;
    [SerializeField] private float _duration = 1.5f;

    private Transform _target;
    private Vector3 _origin, _controlPoint, _initialTarget;
    private float _currentTime = 0;

    void Update()
    {
        if (_currentTime > _duration) Destroy(gameObject);

        transform.position = CalculateBezierPoint(
            _currentTime / _duration,
            _origin,
            _target.gameObject.layer == LayerMask.NameToLayer("Enemy") ? _target.position : _initialTarget,
            _controlPoint
        );

        _currentTime += Time.deltaTime;
    }

    Vector3 CalculateBezierPoint(float t, Vector3 startPosition, Vector3 endPosition, Vector3 controlPoint) {
		float u = 1 - t;
		float uu = u * u;

		Vector3 point = uu * startPosition;
		point += 2 * u * t * controlPoint;
		point += t * t * endPosition;

		return point;
	}

    public override void Init(Transform target)
    {
        _target = target;
        _initialTarget = target.position;
        _origin = transform.position - 1.5f * transform.forward;

        var rand = 5 * _offsetMultiplier * Random.insideUnitSphere;
        var offsetRandom = (rand.y > 0 ? rand : Vector3.Scale(rand, Vector3.down));
        var offsetX = Vector3.right * Random.Range(-3 * _offsetMultiplier, 3 * _offsetMultiplier);
        var offsetY = 3 * _offsetMultiplier * Vector3.up;
        var offsetZ = -5 * _offsetMultiplier * transform.forward;

        _controlPoint = _origin + offsetX + offsetZ + offsetY + offsetRandom;
    }
}
