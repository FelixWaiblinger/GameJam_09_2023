using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNode
{
    public UpgradeNode PreRequirement { get; private set; }

    private List<UpgradeNode> _children = new List<UpgradeNode>();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Identifier { get; private set; }
    public SkillNodeSO.StatBuff Effect { get; private set; }

    public bool IsSkilled = false;

    public UpgradeNode(UpgradeNode preRequirement, List<UpgradeNode> children, string name, string description, string identifier, SkillNodeSO.StatBuff effect) {
        PreRequirement = preRequirement;
        if(children != null) {
            _children.AddRange(children);
        }
        Name = name;
        Description = description;
        Identifier = identifier;
        Effect = effect;
    }

    public void AddChild(UpgradeNode child) {
        _children.Add(child);
    }


    public string GetName() {
        return Name;
    }

}
