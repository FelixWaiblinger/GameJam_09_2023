using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenController : MonoBehaviour
{
    [SerializeField] private SkillTreeSO skillTreeSO;
    [SerializeField] private IntEventChannel _levelUpEventChannel;

    [SerializeField] private int _skillPoints = 0;

    public static event Action<SkillNodeSO> OnUpgradeLearned;

    private UpgradeTree _tree;

    void Start() {
        _tree = new UpgradeTree(skillTreeSO.GetSkillTree());
    }

    void OnEnable() {
        UpgradeComponent<SkillNodeSO>.OnClicked += OnUpgradeClicked;
        _levelUpEventChannel.OnIntEventRaised += OnLevelUp;
    }

    private void OnDisable() {
        UpgradeComponent<SkillNodeSO>.OnClicked -= OnUpgradeClicked;    
        _levelUpEventChannel.OnIntEventRaised -= OnLevelUp;
    }

    private void OnLevelUp(int level) {
        _skillPoints++;
    }

    private void OnUpgradeClicked(SkillNodeSO upgradeData) {
        Debug.Log("Oh you want to upgrade: " + upgradeData.Name);
        TryToLearnUpgrade(upgradeData);
    }

    private void TryToLearnUpgrade(SkillNodeSO upgradeData) {
        if (_skillPoints < 1) return; // Fail because no points available

        UpgradeNode node = _tree.GetUpgrade(upgradeData.Identifier);

        if (node == null || node.IsSkilled) return; // Fail because Upgrade doesn't exist or is already skilled

        // Check if preRequirement is fullfilled
        if (node.PreRequirement == null || node.PreRequirement.IsSkilled) {
            // Success
            node.IsSkilled = true;
            _skillPoints--;
            OnUpgradeLearned?.Invoke(upgradeData);
        }; 

    }
}
