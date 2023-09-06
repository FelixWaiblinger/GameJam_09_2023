using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new Skillnode", menuName = "Skillnode")]
public class SkillNodeSO : ScriptableObject {

    [SerializeField] public string Identifier;
    [SerializeField] public string Name;
    [SerializeField] public string Description;
    [SerializeField] public List<SkillNodeSO> Children;
    [SerializeField] public int Effect;

}