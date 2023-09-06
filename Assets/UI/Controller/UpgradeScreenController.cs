using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeScreenController : MonoBehaviour
{
    [SerializeField] private SkillTreeSO skillTreeSO;

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

    private void OnUpgradeClicked(SkillNodeSO so) {
        Debug.Log("Oh you want to upgrade: " + so.Name);
    }

}
