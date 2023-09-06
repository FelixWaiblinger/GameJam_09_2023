using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeNode
{
    private UpgradeNode _preRequirement;
    private List<UpgradeNode> _children = new List<UpgradeNode>();

    private string _name;
    private string _description;
    private string _identifier;
    private int _effect;

    public UpgradeNode(UpgradeNode preRequirement, List<UpgradeNode> children, string name, string description, string identifier, int effect) {
        _preRequirement = preRequirement;
        if(children != null) {
            _children.AddRange(children);
        }
        _name = name;
        _description = description;
        _identifier = identifier;
        _effect = effect;
    }

    public void AddChild(UpgradeNode child) {
        _children.Add(child);
    }

    public string GetName() {
        return _name;
    }

}
