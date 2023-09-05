using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeScreen
{
    private VisualElement _screen = new VisualElement();

    private string[] _skills;

    public UpgradeScreen(params string[] skills) {
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

    public VisualElement GetVisualElement() { return _screen; }


    public void ShowScreen() {
        _screen.style.display = DisplayStyle.Flex;    
    }

    public void HideScreen() {
        _screen.style.display = DisplayStyle.None;    
    }


}
