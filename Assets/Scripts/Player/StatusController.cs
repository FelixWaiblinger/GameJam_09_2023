using System;
using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    [SerializeField] private FloatEventChannel _healthEvent;
    [SerializeField] private FloatEventChannel _experienceEvent;
    [SerializeField] private VoidEventChannel _hitEvent;
    [SerializeField] private VoidEventChannel _deathEvent;
    [SerializeField] private VoidEventChannel _enemyDeathEvent;
    [SerializeField] private IntEventChannel _levelUpEventChannel;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private LevelSystem levelSystem;


    void Awake() {
        levelSystem = new LevelSystem(10, 5);
        levelSystem.OnLevelUp += (v => _levelUpEventChannel.RaiseIntEvent(v));
        _enemyDeathEvent.OnVoidEventRaised += () => AddExp(5);
        AddExp(0);
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
        Debug.Log($"Took {amount} damage");
        _currentHealth -= amount;
        
        _healthEvent.RaiseFloatEvent(Mathf.Clamp01(_currentHealth / _maxHealth));
        _hitEvent.RaiseVoidEvent();

        if (_currentHealth < 0) _deathEvent.RaiseVoidEvent();
    }
}



