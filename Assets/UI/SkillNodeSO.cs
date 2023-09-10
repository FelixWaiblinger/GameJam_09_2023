using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new Skillnode", menuName = "Skillnode")]
public class SkillNodeSO : ScriptableObject {

    [SerializeField] public string Identifier;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public Texture image;
    [SerializeField] public List<SkillNodeSO> Children;
    [SerializeField] public StatBuff statBuff;

    [Serializable]
    public struct StatBuff
    {
        public StatType type;
        public float amount;
    }

}

public enum StatType {
    Attack,
    Cooldown,
    Speed
}

