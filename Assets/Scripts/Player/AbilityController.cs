using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cooldown
{
    Attack, Primary, Secondary, All
}

public class AbilityController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Transform _abilityOrigin;

    private Camera _camera;
    private Transform _target;
    private Ability _attackSlot;
    private Ability _primarySlot;
    private Ability _secondarySlot;
    private Dictionary<Cooldown, float> _cooldownTimers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 0},
        {Cooldown.Primary, 0},
        {Cooldown.Secondary, 0}
    };
    private Dictionary<Cooldown, float> _cooldownMultipliers = new Dictionary<Cooldown, float>()
    {
        {Cooldown.Attack, 1},
        {Cooldown.Primary, 1},
        {Cooldown.Secondary, 1}, 
        {Cooldown.All, 1}
    };

    #region SETUP

    private void OnEnable()
    {
        InputReader.mousePosEvent += FindTarget;
        InputReader.attackSlotEvent += Attack;
        InputReader.primarySlotEvent += Primary;
        InputReader.secondarySlotEvent += Secondary;
    }

    private void OnDisable()
    {
        InputReader.mousePosEvent -= FindTarget;
        InputReader.attackSlotEvent -= Attack;
        InputReader.primarySlotEvent -= Primary;
        InputReader.secondarySlotEvent -= Secondary;
    }
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _attackSlot = _gameData.Attack;
        _primarySlot = _gameData.Primary;
        _secondarySlot = _gameData.Secondary;
    }

    #endregion

    void Update()
    {
        UpdateCooldowns();
    }

    void FindTarget(Vector2 mousePos)
    {
        var ray = _camera.ScreenPointToRay(mousePos);
        if (!Physics.Raycast(ray, out RaycastHit hit, 100)) return;

        // TODO 
    }

    #region ABILITY

    void Attack()
    {
        if (!TryAddCooldown(Cooldown.Attack)) return;

        // _attackSlot.Activate(_abilityOrigin, _target);
    }

    void Primary()
    {
        if (!TryAddCooldown(Cooldown.Primary)) return;

        _primarySlot.Activate(_abilityOrigin, _target);
    }

    void Secondary()
    {
        if (!TryAddCooldown(Cooldown.Secondary)) return;

        _secondarySlot.Activate(_abilityOrigin, _target);
    }

    bool TryAddCooldown(Cooldown name)
    {
        if (_cooldownTimers[name] > 0) return false;

        float cd = 0;
        switch (name)
        {
            case Cooldown.Attack: cd = _attackSlot.Cooldown; break;
            case Cooldown.Primary: cd = _primarySlot.Cooldown; break;
            case Cooldown.Secondary: cd = _secondarySlot.Cooldown; break;
        }

        _cooldownTimers[name] = cd * _cooldownMultipliers[name] * _cooldownMultipliers[Cooldown.All];

        return true;
    }

    void UpdateCooldowns()
    {
        var cdList = new List<Cooldown>(_cooldownTimers.Keys);
        foreach (Cooldown cd in cdList)
        {
            if (_cooldownTimers[cd] < 0) continue;
            _cooldownTimers[cd] -= Time.deltaTime;
        }
    }

    #endregion
}
