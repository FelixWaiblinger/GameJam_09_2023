using System;
using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    [SerializeField] private FloatEventChannel _healthEvent;
    [SerializeField] private FloatEventChannel _experienceEvent;
    [SerializeField] private VoidEventChannel _hitEvent;
    [SerializeField] private VoidEventChannel _deathEvent;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    private LevelSystem levelSystem;


    void Awake() {
        levelSystem = new LevelSystem(10, 5);
    }


    private void Update() {
        AddExp(1);
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


public class LevelSystem {

    public int Level { get; private set; } = 1;
    private float _experience = 0;
    private float _expToNextLevel;
    private float _expIncreasePerLevel;

    public LevelSystem(float expToNextLevel, float expIncreasePerLevel) {
        _expToNextLevel = expToNextLevel;
        _expIncreasePerLevel = expIncreasePerLevel;
    }

    public void AddExp(float exp) {
        _experience += exp;
        ProcessExp();
    }

    private void ProcessExp() {
        while(_experience >= _expToNextLevel) {
            LevelUp();
        }
    }

    private void LevelUp() {
        _experience -= _expToNextLevel;
        Level++;
        _expToNextLevel += _expIncreasePerLevel;
    }

    public float GetExpRequiredNormalized() {
        return _experience / _expToNextLevel;
    }
}
