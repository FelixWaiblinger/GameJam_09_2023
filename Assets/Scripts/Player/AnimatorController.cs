using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _visuals;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private BoolEventChannel _combatEvent;
    [SerializeField] private VoidEventChannel _hitEvent;
    [SerializeField] private VoidEventChannel _deathEvent;
    [SerializeField] private float _animCombatBlend;
    private Vector3 _moveDirection, _animCombatDirection;
    private Vector3 _ignoreY = new(1, 0, 1);

    void OnEnable()
    {
        InputReader.moveEvent += (direction) => _moveDirection = new(direction.x, 0, direction.y);
        InputReader.jumpEvent += () => _animator.SetTrigger("jump");
        InputReader.dashEvent += () => _animator.SetTrigger("dash");
        InputReader.attackSlotEvent += () => _animator.SetTrigger("attack");

        _combatEvent.OnBoolEventRaised += (active) => _animator.SetBool("inCombat", active);
        _hitEvent.OnVoidEventRaised += () => _animator.SetTrigger("hit");
        _deathEvent.OnVoidEventRaised += () => { _animator.SetTrigger("death"); this.enabled = false; };
    }

    void Update()
    {
        UpdateAnimationDirection();

        if (_animator.GetBool("inCombat"))
        {
            _animator.SetFloat("combatVelX", _animCombatDirection.x);
            _animator.SetFloat("combatVelZ", _animCombatDirection.z);
        }
        else
        {
            _animator.SetFloat("velocity", Vector3.Scale(_rigidBody.velocity, _ignoreY).magnitude);
        }
    }

    void UpdateAnimationDirection()
    {
        var direction = _visuals.rotation * _moveDirection;
        var rot = _visuals.rotation.eulerAngles.y;

        // looking right-ish
        if (rot >= 60 && rot <= 120)
        {
            direction = new Vector3(-direction.z, 0, direction.x);
        }
        // looking back-ish
        else if (rot > 120 && rot < 240)
        {
            direction = new Vector3(-direction.x, 0, -direction.z);
        }
        // looking left-ish
        else if (rot >= 240 && rot <= 300)
        {
            direction = new Vector3(direction.z, 0, -direction.x);
        }

        _animCombatDirection = Vector3.MoveTowards(
            _animCombatDirection,
            direction,
            _animCombatBlend * Time.deltaTime
        );
    }
}
