using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private BoolEventChannel _combatEvent;
    private bool _inCombat = false;

    private Vector3 ignoreY = new(1, 0, 1);

    void OnEnable()
    {
        InputReader.jumpEvent += () => _animator.SetTrigger("jump");
        InputReader.dashEvent += () => _animator.SetTrigger("dash");
        InputReader.attackSlotEvent += () => _animator.SetTrigger("attack");

        _combatEvent.OnBoolEventRaised += (active) => _inCombat = active;
    }

    void Update()
    {
        if (_inCombat)
        {
            _animator.SetFloat("combatVelX", _rigidBody.velocity.x);
            _animator.SetFloat("combatVelZ", _rigidBody.velocity.z);
        }
        else
        {
            _animator.SetFloat("velocity", Vector3.Scale(_rigidBody.velocity, ignoreY).magnitude);
        }
    }
}
