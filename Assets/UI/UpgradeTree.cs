using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTree
{

    Dictionary<string, UpgradeNode> keyValuePairs = new Dictionary<string, UpgradeNode>();

    public UpgradeTree(SkillNodeSO skillNodeSO) {

        // Fill Dictionary
        FillDictonary(skillNodeSO, null);
    }

    private void FillDictonary(SkillNodeSO node, UpgradeNode parent) {

        UpgradeNode upgrade = new UpgradeNode(parent, null , node.name, node.Description, node.Identifier, node.Effect);
        keyValuePairs.Add(node.Identifier, upgrade);

        if (parent != null) {
            parent.AddChild(upgrade);
        }

        foreach(SkillNodeSO child in node.Children) {
            FillDictonary(child, upgrade);
        }
    }
}
