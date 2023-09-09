using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScreenController : MonoBehaviour
{
    [SerializeField] private FloatEventChannel _HealthChangeEventChannel;
    [SerializeField] private FloatEventChannel _ExperienceChangeEventChannel;
    [SerializeField] private SkillInfoEventChannel _SkillInfoEventChannel;

    public static event Action<float> OnExpChanged; // For now will pass in percentage
    public static event Action<float> OnHealthChanged; // For now will pass in percentage
    public static event Action<SkillInfo> OnCooldownChanged;
    public static event Action<GameData> OnSkillsChanged;

    private void OnEnable() {
        MainMenuController.OnRunStarted += OnSkillsChanged;
        _HealthChangeEventChannel.OnFloatEventRaised += (v => OnHealthChanged?.Invoke(v));
        _ExperienceChangeEventChannel.OnFloatEventRaised += (v => OnExpChanged?.Invoke(v));
        _SkillInfoEventChannel.OnSkillInfoEventRaised += (v => OnCooldownChanged?.Invoke(v));
    }

    private void OnDisable() {
        MainMenuController.OnRunStarted -= OnSkillsChanged;
    } 
}
