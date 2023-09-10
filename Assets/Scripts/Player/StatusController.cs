using System;
using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    [SerializeField] private FloatEventChannel _healthEvent;
    [SerializeField] private FloatEventChannel _experienceEvent;
    [SerializeField] private VoidEventChannel _hitEvent;
    [SerializeField] private VoidEventChannel _deathEvent;
    [SerializeField] private IntEventChannel _levelUpEventChannel;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private LevelSystem levelSystem;


    void Awake() {
        levelSystem = new LevelSystem(10, 5);
        levelSystem.OnLevelUp += (v => _levelUpEventChannel.RaiseIntEvent(v));
    }
    void Start()
    {
        _currentHealth = _maxHealth;
    }


    public void AddExp(float exp) {
        levelSystem.AddExp(exp);
        _experienceEvent.RaiseFloatEvent(levelSystem.GetExpRequiredNormalized());
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        
        _healthEvent.RaiseFloatEvent(Mathf.Clamp01(_currentHealth / _maxHealth));
        _hitEvent.RaiseVoidEvent();

        if (_currentHealth < 0) _deathEvent.RaiseVoidEvent();
    }
}



