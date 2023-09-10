using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeScreen : UIScreen {

    private VisualElement _tooltipContainer;

    [SerializeField] private SkillTreeSO skillTreeSO;

    protected override void Awake() {
        base.Awake();
        _screen.AddToClassList("upgradeMenu");

        Generate();
    }

    private void OnEnable() {
        UpgradeScreenController.OnUpgradeLearned += UpdateUpgradeElement;
        UpgradeComponent<SkillNodeSO>.OnMouseOver += ShowTooltip;
        UpgradeComponent<SkillNodeSO>.OnMouseOut += HideTooltip;
    }

    private void UpdateUpgradeElement(SkillNodeSO upgradeData) {
        Button btn = _screen.Q<Button>(upgradeData.Identifier);
        btn.AddToClassList("learned");
    }

    private void OnDisable() {
        UpgradeComponent<SkillNodeSO>.OnMouseOver -= ShowTooltip;
        UpgradeComponent<SkillNodeSO>.OnMouseOut -= HideTooltip;
           
    }

    private void Generate() {
        GenerateSkillTree();
        AddTooltipContainer();
    }

    private void GenerateSkillTree() {
        SkillTreeVisual skillTree = new SkillTreeVisual(skillTreeSO);
        _screen.Add(skillTree);
    }

    private void AddTooltipContainer() {
        _tooltipContainer = Create<VisualElement>("tooltipContainer");
        _screen.Add(_tooltipContainer);
    }

    private void ShowTooltip(SkillNodeSO gameData, Vector2 position) {
        _tooltipContainer.Clear();

        Vector2 tooltipPosition = position - _screen.worldBound.position;
        UpgradeTooltip tooltip = new UpgradeTooltip(gameData.Name, gameData.Description);
        tooltip.pickingMode = PickingMode.Ignore;

        _tooltipContainer.Add(tooltip);

        _tooltipContainer.pickingMode = PickingMode.Ignore;
        _tooltipContainer.style.left = tooltipPosition.x;
        _tooltipContainer.style.top = tooltipPosition.y;
        _tooltipContainer.style.position = Position.Absolute;
        _tooltipContainer.style.display = DisplayStyle.Flex;
        _tooltipContainer.BringToFront();
    }

    private void HideTooltip(SkillNodeSO arg1, Vector2 vector) {
        _tooltipContainer.style.display = DisplayStyle.None;
    }
}

