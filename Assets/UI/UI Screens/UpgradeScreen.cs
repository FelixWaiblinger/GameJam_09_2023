using System;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public class UpgradeScreen : UIScreen {

    private VisualElement _tooltipContainer;

    [SerializeField] private string[] _skills;
    [SerializeField] private SkillTreeSO skillTreeSO;

    protected override void Awake() {
        base.Awake();
        _screen.AddToClassList("upgradeMenu");

        Generate();
    }

    private void OnEnable() {
        UpgradeComponent<string>.OnMouseOver += ShowTooltip;
        UpgradeComponent<string>.OnMouseOut += HideTooltip;
    }

    private void OnDisable() {
        UpgradeComponent<string>.OnMouseOver -= ShowTooltip;
        UpgradeComponent<string>.OnMouseOut -= HideTooltip;
           
    }

    private void Generate() {
        _screen.Add(new Label("This is my UpgradeScreen"));

        GenerateSkillTree();
        AddTooltipContainer();
    }

    private void GenerateSkillTree() {
        SkillTreeVisual skillTree = new SkillTreeVisual(_skills, skillTreeSO);
        _screen.Add(skillTree);
    }

    private void AddTooltipContainer() {
        _tooltipContainer = Create<VisualElement>("tooltipContainer");
        Label tooltip = Create<Label>("tooltip");
        _tooltipContainer.Add(tooltip);
        _tooltipContainer.style.display = DisplayStyle.None;

        _screen.Add(_tooltipContainer);
    }

    private void ShowTooltip(string gameData, Vector2 position) {
        //Debug.Log(gameData + ": " + position);

        Vector2 tooltipPosition = position - _screen.worldBound.position;

        Label tooltip = _tooltipContainer.Q<Label>();
        tooltip.style.marginLeft = 70;

        tooltip.text = "This will be filled... eventually :" + gameData;
        tooltip.pickingMode = PickingMode.Ignore;

        _tooltipContainer.pickingMode = PickingMode.Ignore;
        _tooltipContainer.style.left = tooltipPosition.x;
        _tooltipContainer.style.top = tooltipPosition.y;
        _tooltipContainer.style.position = Position.Absolute;
        _tooltipContainer.style.display = DisplayStyle.Flex;
        _tooltipContainer.BringToFront();
    }

    private void HideTooltip(string arg1, Vector2 vector) {
        //Debug.Log(arg1 + "OUT: " + vector);
        _tooltipContainer.style.display = DisplayStyle.None;
    }


}

public class SkillTreeVisual : VisualElement {

    private string[] _skills;
    private SkillTreeSO _skillTreeSO;

    private VisualElement _container;
    
    public SkillTreeVisual(string[] skills, SkillTreeSO skillTreeSO) {

        _skills = skills;
        _skillTreeSO = skillTreeSO;

        _container = new VisualElement();
        _container.AddToClassList("upgradeTree");
        this.Add(_container);

        CreateUpgradeComponents();

        this.generateVisualContent += OnGenerateVisualContent; 
    }

    private void CreateUpgradeComponents() {

        SkillNodeSO root = _skillTreeSO.GetSkillTree();

        VisualElement visualElementRoot = new VisualElement();
        
        CreateTree(visualElementRoot, root);
        
        _container.Add(visualElementRoot);

        for (int i = 0; i < _skills.Length; i++) {
            UpgradeComponent<String> upgradeComponent =
                new UpgradeComponent<String>(_container, _skills[i]);
        }

    }

    private void CreateTree(VisualElement parent, SkillNodeSO node) {
        VisualElement visualNode = new VisualElement();
        visualNode.AddToClassList("visualNode");
        parent.Add(visualNode);

        new UpgradeComponent<String>(visualNode, node.Name);
        VisualElement childContainer = new VisualElement();
        childContainer.AddToClassList("childContainer");
        visualNode.Add(childContainer);

        foreach (SkillNodeSO child in node.Children) {
            CreateTree(childContainer, child);
        }
    }

    private void OnGenerateVisualContent(MeshGenerationContext context) {
        Painter2D painter = context.painter2D;

        Vector2 containerPosition = context.visualElement.worldBound.position;

        Button lastElement = context.visualElement.Q<Button>(_skills[0]);

        foreach(string s in _skills) {
            Button currentElement = context.visualElement.Q<Button>(s);

            Vector2 startPoint = lastElement.worldBound.position - containerPosition;
            startPoint.y += lastElement.worldBound.height;
            startPoint.x += lastElement.worldBound.width / 2;

            Vector2 endPoint = currentElement.worldBound.position - containerPosition;
            endPoint.y += 0;
            endPoint.x += currentElement.worldBound.width / 2;

            DrawLine(painter, startPoint, endPoint);

            lastElement = currentElement;
        }
    }

    private void DrawLine(Painter2D painter, Vector2 from, Vector2 to) {
        painter.BeginPath();
        painter.lineWidth = 2;
        painter.LineTo(from);
        painter.LineTo(to);
        painter.ClosePath();
        painter.Stroke();
    }

}

