using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenController : MonoBehaviour
{
    [SerializeField] private SkillTreeSO skillTreeSO;

    [SerializeField] private int _skillPoints = 0;

    public static event Action<string> OnUpgradeLearned;

    private UpgradeTree _tree;

    void Start() {
        _tree = new UpgradeTree(skillTreeSO.GetSkillTree());
    }

    void OnEnable() {
        UpgradeComponent<SkillNodeSO>.OnClicked += OnUpgradeClicked;    
    }

    private void OnDisable() {
        UpgradeComponent<SkillNodeSO>.OnClicked -= OnUpgradeClicked;    
    }

    private void OnUpgradeClicked(SkillNodeSO upgradeData) {
        Debug.Log("Oh you want to upgrade: " + upgradeData.Name);
        TryToLearnUpgrade(upgradeData.Identifier);
    }

    private void TryToLearnUpgrade(string identifier) {
        if (_skillPoints < 1) return; // Fail because no points available

        UpgradeNode node = _tree.GetUpgrade(identifier);

        if (node == null || node.IsSkilled) return; // Fail because Upgrade doesn't exist or is already skilled

        // Check if preRequirement is fullfilled
        if (node.PreRequirement == null || node.PreRequirement.IsSkilled) {
            // Success
            node.IsSkilled = true;
            _skillPoints--;
            OnUpgradeLearned?.Invoke(identifier);
        }; 

    }
}
