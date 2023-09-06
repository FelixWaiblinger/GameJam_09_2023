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
        UpgradeComponent<String>.OnClicked += OnUpgradeClicked;    
    }

    private void OnDisable() {
        UpgradeComponent<String>.OnClicked -= OnUpgradeClicked;    
    }

    private void OnUpgradeClicked(string @string) {
        Debug.Log("Oh you want to upgrade: " + @string);
    }

}
