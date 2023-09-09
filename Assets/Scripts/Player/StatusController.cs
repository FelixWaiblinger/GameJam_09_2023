using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    [SerializeField] private FloatEventChannel _healthEvent;
    [SerializeField] private FloatEventChannel _experienceEvent;
    [SerializeField] private VoidEventChannel _hitEvent;
    [SerializeField] private VoidEventChannel _deathEvent;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    private float _maxExperience, _currentExperience;

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        _healthEvent.RaiseFloatEvent(Mathf.Clamp01(_currentHealth / _maxHealth));
        _hitEvent.RaiseVoidEvent();

        if (_currentHealth < 0) _deathEvent.RaiseVoidEvent();
    }
}
