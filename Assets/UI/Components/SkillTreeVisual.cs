using UnityEngine;
using UnityEngine.UIElements;

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

