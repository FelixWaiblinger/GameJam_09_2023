using System;
using Unity.VisualScripting;
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
        UpgradeComponent<SkillNodeSO>.OnMouseOver += ShowTooltip;
        UpgradeComponent<SkillNodeSO>.OnMouseOut += HideTooltip;
    }

    private void OnDisable() {
        UpgradeComponent<SkillNodeSO>.OnMouseOver -= ShowTooltip;
        UpgradeComponent<SkillNodeSO>.OnMouseOut -= HideTooltip;
           
    }

    private void Generate() {
        _screen.Add(new Label("This is my UpgradeScreen"));

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
        //Debug.Log(gameData + ": " + position);
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
        //Debug.Log(arg1 + "OUT: " + vector);
        _tooltipContainer.style.display = DisplayStyle.None;
    }
}

