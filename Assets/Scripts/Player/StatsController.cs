using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsController
{
    private List<float> attackBuffs = new List<float>();
    private List<float> moveSpeedBuffs = new List<float>();
    private List<float> coolDownBuffs = new List<float>();

    public void ProcessUpgradeLearned(SkillNodeSO skillNodeSO) {
        switch (skillNodeSO.statBuff.type) {
            case StatType.Attack:
                attackBuffs.Add(skillNodeSO.statBuff.amount);
                break;
            case StatType.Cooldown:
                coolDownBuffs.Add(skillNodeSO.statBuff.amount);
                break;
            case StatType.Speed:
                moveSpeedBuffs.Add(skillNodeSO.statBuff.amount);
                break;
            default:
                break;
        }
    }

    public float GetFinalMultiplier(StatType type) {
        float finalMultiplier = 1;

        switch (type) {
            case StatType.Attack:
                foreach (float multiplier in attackBuffs)
                {
                    finalMultiplier *= 1 + multiplier/100;
                }
                break;
            case StatType.Cooldown:
                foreach (float multiplier in coolDownBuffs) {
                    finalMultiplier *= 1 + multiplier / 100;
                }
                break;
            case StatType.Speed:
                foreach (float multiplier in moveSpeedBuffs) {
                    finalMultiplier *= 1 + multiplier / 100;
                }
                break;
            default:
                throw new NotImplementedException();
        }
        Debug.Log("Final Multiplier: " + finalMultiplier);
        return finalMultiplier;
    }


}
