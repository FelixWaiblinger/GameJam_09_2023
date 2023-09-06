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
        tooltip.style.marginLeft = 70;
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

public class UpgradeTooltip : VisualElement {

    public UpgradeTooltip(string name, string description) {
        this.AddToClassList("upgradeTooltip");

        Label _name = this.Create<Label>("title");
        _name.text = name;
        this.Add(_name);

        Label _description = this.Create<Label>("text");
        _description.text = description;
        this.Add(_description);

    }

}

public static class UIElementsExtensions {

    public static T Create<T>(this VisualElement ele, params string[] classNames) where T : VisualElement, new() {
        T element = new T();
        foreach (string className in classNames) {
            element.AddToClassList(className);
        }

        return element;
    }
}



public class SkillTreeVisual : VisualElement {

    private SkillTreeSO _skillTreeSO;

    private VisualElement _container;
    
    public SkillTreeVisual(SkillTreeSO skillTreeSO) {

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
    }

    private void CreateTree(VisualElement parent, SkillNodeSO node) {
        VisualElement visualNode = new VisualElement();
        visualNode.AddToClassList("visualNode");
        parent.Add(visualNode);

        new UpgradeComponent<SkillNodeSO>(visualNode, node);
        VisualElement childContainer = new VisualElement();
        childContainer.AddToClassList("childContainer");
        visualNode.Add(childContainer);

        foreach (SkillNodeSO child in node.Children) {
            CreateTree(childContainer, child);
        }
    }

    private void OnGenerateVisualContent(MeshGenerationContext context) {
        SkillNodeSO root = _skillTreeSO.GetSkillTree();
        PaintTreeLines(root, root, context);
    }

    public void PaintTreeLines(SkillNodeSO parent, SkillNodeSO node, MeshGenerationContext context) {
        Painter2D painter = context.painter2D;

        Vector2 containerPosition = context.visualElement.worldBound.position;

        Button lastElement = _container.Q<Button>(parent.Identifier);

        Button currentElement = _container.Q<Button>(node.Identifier);

        Vector2 startPoint = lastElement.worldBound.position - containerPosition;
        startPoint.y += lastElement.worldBound.height;
        startPoint.x += lastElement.worldBound.width / 2;

        Vector2 endPoint = currentElement.worldBound.position - containerPosition;
        endPoint.y += 0;
        endPoint.x += currentElement.worldBound.width / 2;

        DrawLine(painter, startPoint, endPoint);

        foreach (SkillNodeSO child in node.Children) {
            PaintTreeLines(node, child, context);
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

