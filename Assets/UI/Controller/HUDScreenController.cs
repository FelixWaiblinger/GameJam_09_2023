using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScreenController : MonoBehaviour
{
    float tempHP = 0;
    float tempXP = 0;
    float cd = 15;


    public static event Action<float> OnExpChanged; // For now will pass in percentage
    public static event Action<float> OnHealthChanged; // For now will pass in percentage
    public static event Action<SkillInfo> OnCooldownChanged;
    public static event Action<SkillInfo[]> OnSkillsChanged;

    private void OnEnable() {
        InputReader.attackSlotEvent += DoSomething;
        InputReader.primarySlotEvent += DoSomethingElse;
    }

    private void OnDisable() {
        InputReader.attackSlotEvent -= DoSomething;
        InputReader.primarySlotEvent -= DoSomethingElse;
    } 

    private void DoSomethingElse() {
        tempHP += 0.1f;
        OnHealthChanged?.Invoke(tempHP);
        cd += 10;
    }

    private void DoSomething() {
        tempXP += 0.1f;
        OnExpChanged?.Invoke(tempXP);
    }

    private void Update() {
        SkillInfo info = new SkillInfo();
        info.Name = "Fireball";
        info.MaxCooldown = 15;
        cd -= Time.deltaTime;
        info.CurrentCoolDown = cd;
        OnCooldownChanged?.Invoke(info);
    }
}
