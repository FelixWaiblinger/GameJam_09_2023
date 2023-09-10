using System.Collections.Generic;
using UnityEngine;

public enum Slot
{
    Attack, Primary, Secondary, All
}

public class AbilityController : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    [SerializeField] private SkillInfoEventChannel _skillInfoEvent;

    [Header("Control")]
    [SerializeField] private BoolEventChannel _combatEvent;
    [SerializeField] private FloatEventChannel _silenceEvent;
    private float _silenceTimer;

    [Header("Casting")]
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _abilityOrigin;
    [SerializeField] private LayerMask _enemyLayer;
    private Vector3 ignoreY = new(1, 0, 1);
    private Camera _camera;

    private Dictionary<Slot, Ability> _slots = new Dictionary<Slot, Ability>();
    private Dictionary<Slot, float> _cooldownTimers = new Dictionary<Slot, float>()
        {
        {Slot.Attack, 0},
        { Slot.Primary, 0},
        { Slot.Secondary, 0}
    };

    private Dictionary<Slot, float> _cooldownMultipliers = new Dictionary<Slot, float>()
    {
        {Slot.Attack, 1},
        {Slot.Primary, 1},
        {Slot.Secondary, 1},
        {Slot.All, 1}
    };

    private StatsController statsController = new StatsController();


    #region SETUP

    void OnEnable()
    {
        InputReader.mousePosEvent += FindTarget;
        InputReader.attackSlotEvent += Attack;
        InputReader.primarySlotEvent += () => Cast(Slot.Primary);
        InputReader.secondarySlotEvent += () => Cast(Slot.Secondary);
        UpgradeScreenController.OnUpgradeLearned += OnUpgradeLearned;

        _silenceEvent.OnFloatEventRaised += (duration) => _silenceTimer = duration;
    }

    void OnDisable()
    {
        InputReader.mousePosEvent -= FindTarget;
        InputReader.attackSlotEvent -= Attack;

        UpgradeScreenController.OnUpgradeLearned -= OnUpgradeLearned;
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
        UpdateTimers();
    }

    #region TARGET

    void FindTarget(Vector2 mousePos)
    {
        if (CameraTools.GetScreenToWorld(mousePos, out Vector3 worldPos))
        {
            _target.position = worldPos - Vector3.up * (worldPos.y - _abilityOrigin.position.y);
        }

        _abilityOrigin.rotation = Quaternion.LookRotation(_target.position - _abilityOrigin.position);
    }

    #endregion

    #region ABILITY

    void Attack()
    {
        if (!TryAddCooldown(Slot.Attack)) return;

        _slots[Slot.Attack].Activate(_abilityOrigin, _target);
        _combatEvent.RaiseBoolEvent(true);
    }

    void Cast(Slot slot)
    {
        if (_silenceTimer > 0) return;

        if (!TryAddCooldown(slot)) return;

        _slots[slot].Activate(_abilityOrigin, _target);
        _combatEvent.RaiseBoolEvent(true);
    }

    bool TryAddCooldown(Slot name)
    {
        if (_cooldownTimers[name] > 0) return false;

        _cooldownTimers[name] = _slots[name].Cooldown * 
            (1 / this.statsController.GetFinalMultiplier(StatType.Cooldown));

        return true;
    }

    #endregion


    void OnUpgradeLearned(SkillNodeSO node) {
        statsController.ProcessUpgradeLearned(node);
    }


    void UpdateTimers()
    {
        foreach (Slot s in _slots.Keys)
        {
            if (_cooldownTimers[s] < 0) continue;
            _cooldownTimers[s] -= Time.deltaTime;
            _skillInfoEvent.RaiseSkillInfoEvent(new SkillInfo(_cooldownTimers[s], _slots[s]));
        }

        if (_silenceTimer > 0) _silenceTimer -= Time.deltaTime;
    }

}
