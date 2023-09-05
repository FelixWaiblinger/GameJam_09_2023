using System;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public class UpgradeScreen : UIScreen {
    
    private string[] _skills;

    public UpgradeScreen(params string[] skills) {
        _screen.AddToClassList("upgradeMenu");
        _skills = skills;
        Generate();
    }

    private void Generate() {
        _screen.Add(new Label("This is my UpgradeScreen"));

        for(int i = 0; i<_skills.Length; i++) {
            UpgradeComponent<String> upgradeComponent =
                new UpgradeComponent<String>(_screen, _skills[i]);
        }
    }
}
