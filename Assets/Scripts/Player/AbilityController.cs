using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Slot
{
    Attack, Primary, Secondary, All
}

public class AbilityController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private Transform _abilityOrigin;
    [SerializeField] private Transform _target;
    [SerializeField] private LayerMask _groundLayers, _enemyLayer;

    private Vector3 _hitPoint, ignoreY = new(1, 0, 1);
    private Camera _camera;
    private Dictionary<Slot, Ability> _slots = new Dictionary<Slot, Ability>();
    private Dictionary<Slot, float> _cooldownTimers = new Dictionary<Slot, float>()
    {
        {Slot.Attack, 0},
        {Slot.Primary, 0},
        {Slot.Secondary, 0}
    };
    private Dictionary<Slot, float> _cooldownMultipliers = new Dictionary<Slot, float>()
    {
        {Slot.Attack, 1},
        {Slot.Primary, 1},
        {Slot.Secondary, 1},
        {Slot.All, 1}
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
    
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        _slots[Slot.Attack] = _gameData.Attack;
        _slots[Slot.Primary] = _gameData.Primary;
        _slots[Slot.Secondary] = _gameData.Secondary;
    }

    #endregion

    void Update()
    {
        UpdateCooldowns();
    }

    void FindTarget(Vector2 mousePos)
    {
        var ray = _camera.ScreenPointToRay(mousePos);
        if (!Physics.Raycast(ray, out RaycastHit hit, 100, _groundLayers)) return;

        _hitPoint = hit.point;

        _target.position = Vector3.Scale(hit.point, ignoreY) + Vector3.up * transform.position.y;
        _abilityOrigin.rotation = Quaternion.LookRotation(_target.position - _abilityOrigin.position);
    }

    bool FindEnemy(out Transform enemy)
    {
        enemy = null;
        var enemies = Physics.OverlapSphere(_hitPoint, 10, _enemyLayer, QueryTriggerInteraction.Ignore);

        if (enemies.Length == 0) return false;
        
        var minDistance = float.MaxValue;
        foreach (Collider c in enemies)
        {
            var distance = Vector3.Distance(c.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemy = c.transform;
            }
        }

        return true;
    }

    #region ABILITY

    void Attack()
    {
        if (!TryAddCooldown(Slot.Attack)) return;

        // _slots[(int)Slot.Attack].Activate(_abilityOrigin, _target);
    }

    void Primary()
    {
        if (!TryAddCooldown(Slot.Primary)) return;

        _slots[Slot.Primary].Activate(_abilityOrigin, FindEnemy(out Transform enemy) ? enemy : _target);
    }

    void Secondary()
    {
        if (!TryAddCooldown(Slot.Secondary)) return;

        _slots[Slot.Secondary].Activate(_abilityOrigin, _target);
    }

    bool TryAddCooldown(Slot name)
    {
        if (_cooldownTimers[name] > 0) return false;

        _cooldownTimers[name] = _slots[name].Cooldown
                                * _cooldownMultipliers[name]
                                * _cooldownMultipliers[Slot.All];

        return true;
    }

    void UpdateCooldowns()
    {
        foreach (Slot s in _slots.Keys)
        {
            if (_cooldownTimers[s] < 0) continue;
            _cooldownTimers[s] -= Time.deltaTime;
        }
    }

    #endregion
}
