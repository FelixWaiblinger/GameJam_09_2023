using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cooldown
{
    Attack, Primary, Secondary, Dash, All
}

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameData _gameData;

    [Header("Movement")]
    [SerializeField] private float _dashDuration;
    [SerializeField] private float _dashCooldown;

    private Vector3 _moveDirection;
    private float _dashDurationTimer;

    [Header("Abilities")]
    [SerializeField] private Ability _attackSlot;
    [SerializeField] private Ability _primarySlot;
    [SerializeField] private Ability _secondarySlot;
    [SerializeField] private Transform _abilityOrigin;

    private Transform _target;

    private Dictionary<Cooldown, float> _cooldownTimers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 0},
        {Cooldown.Primary, 0},
        {Cooldown.Secondary, 0}, 
        {Cooldown.Dash, 0}
    };
    private Dictionary<Cooldown, float> _cooldownMultipliers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 1},
        {Cooldown.Primary, 1},
        {Cooldown.Secondary, 1}, 
        {Cooldown.Dash, 1},
        {Cooldown.All, 1}
    };

    #region SETUP

    void OnEnable()
    {
        InputReader.attackSlotEvent += Attack;
        InputReader.primarySlotEvent += Primary;
        InputReader.secondarySlotEvent += Secondary;
    }

    void OnDisable()
    {
        InputReader.attackSlotEvent -= Attack;
        InputReader.primarySlotEvent -= Primary;
        InputReader.secondarySlotEvent -= Secondary;
    }

    void Awake()
    {
        // load chosen abilities
        _attackSlot = _gameData.Attack;
        _primarySlot = _gameData.Primary;
        _secondarySlot = _gameData.Secondary;
    }

    #endregion

    void Update()
    {
        UpdateCooldowns();

        UpdateDashDuration();
    }

    #region ABILITY

    void Attack()
    {
        if (!AddCooldown(Cooldown.Attack)) return;

        _attackSlot.Activate(_abilityOrigin, _target);
    }

    void Primary()
    {
        if (!AddCooldown(Cooldown.Primary)) return;

        _primarySlot.Activate(_abilityOrigin, _target);
    }

    void Secondary()
    {
        if (!AddCooldown(Cooldown.Secondary)) return;

        _secondarySlot.Activate(_abilityOrigin, _target);
    }

    void Dash()
    {
        if (!AddCooldown(Cooldown.Dash) || _dashDuration > 0) return;

        _dashDurationTimer = _dashDuration;
    }

    bool AddCooldown(Cooldown cd)
    {
        if (_cooldownTimers[cd] > 0) return false;

        _cooldownTimers[cd] = _attackSlot.Cooldown *
            _cooldownMultipliers[cd] * _cooldownMultipliers[Cooldown.All];

        return true;
    }

    void UpdateCooldowns()
    {
        foreach (Cooldown cd in _cooldownTimers.Keys)
        {
            if (_cooldownTimers[cd] < 0) continue;
            _cooldownTimers[cd] -= Time.deltaTime;
        }
    }

    void UpdateDashDuration()
    {
        if (_dashDurationTimer > 0)
        {
            _dashDurationTimer -= Time.deltaTime;

            if (_dashDurationTimer < 0)
                _cooldownTimers[Cooldown.Dash] = _dashCooldown;
        }
    }

    #endregion
}
