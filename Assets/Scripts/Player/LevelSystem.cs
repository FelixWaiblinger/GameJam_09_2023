using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelSystem {

    public event Action<int> OnLevelUp;

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
        while (_experience >= _expToNextLevel) {
            LevelUp();
        }
    }

    private void LevelUp() {
        _experience -= _expToNextLevel;
        Level++;
        _expToNextLevel += _expIncreasePerLevel;
        OnLevelUp?.Invoke(Level);
    }

    public float GetExpRequiredNormalized() {
        return _experience / _expToNextLevel;
    }
}