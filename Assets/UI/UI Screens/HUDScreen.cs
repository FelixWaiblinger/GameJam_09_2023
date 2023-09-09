using UnityEngine;
using UnityEngine.UIElements;

public class HUDScreen : UIScreen
{
    [SerializeField] private Texture texture;
    [SerializeField] private Sprite backgroundSprite;


    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject ExpBar;

    ProgressBar expBar;
    ProgressBar hpBar;

    SkillBar skillbar;


    protected override void Awake() {
        base.Awake();
        _screen.AddToClassList("hud");
        GenerateUI();
    }


    private void OnEnable() {
        HUDScreenController.OnExpChanged += UpdateExpBar;
        HUDScreenController.OnHealthChanged += UpdateHpBar;
        HUDScreenController.OnCooldownChanged += UpdateCooldown;
        HUDScreenController.OnSkillsChanged += UpdateSkills;
    }


    private void OnDisable() {
        HUDScreenController.OnExpChanged -= UpdateExpBar;
        HUDScreenController.OnHealthChanged -= UpdateHpBar;
        HUDScreenController.OnCooldownChanged -= UpdateCooldown;
        HUDScreenController.OnSkillsChanged -= UpdateSkills;
    }

    private void UpdateExpBar(float value) {
        if(ExpBar.TryGetComponent(out UnityEngine.UI.Image imageComponent)) {
            imageComponent.fillAmount = value;
        }
    }

    private void UpdateHpBar(float value) {
        if (HealthBar.TryGetComponent(out UnityEngine.UI.Image imageComponent)) {
            imageComponent.fillAmount = value;
        }
    }

    private void UpdateCooldown(SkillInfo info) {
        float progress = info.CurrentCoolDown / info.MaxCooldown;
        UISkill skill = skillbar.Q<UISkill>(info.Name);
        if(skill != null) {
            skill.UpdateCooldown(progress);
        }
    }
    private void UpdateSkills(GameData gameData) {
        SkillInfo info1 = new SkillInfo(0, gameData.Primary);
        SkillInfo info2 = new SkillInfo(0, gameData.Secondary);

        Debug.Log("Was here");

        _screen.Remove(skillbar);
        skillbar = new SkillBar(new SkillInfo[] { info1, info2 }, this.backgroundSprite);
        _screen.Add(skillbar);
    }

    private void GenerateUI() {
        Label label = Create<Label>();
        label.text = "This is ingame!";
        _screen.Add(label);

        skillbar = new SkillBar(new SkillInfo[] { }, this.backgroundSprite);
        _screen.Add(skillbar);
    }

    public override void ShowScreen() {
        base.ShowScreen();
        canvas.SetActive(true);

    }

    public override void HideScreen() {
        base.HideScreen();
        canvas.SetActive(false);
    }
}


public class SkillBar : VisualElement {

    public SkillBar(SkillInfo[] skills, Sprite bg) {
        AddToClassList("skillBar");
        this.style.backgroundImage = new StyleBackground(bg);

        foreach (SkillInfo skill in skills) {
            UISkill uiSkill = new UISkill(skill.UIIcon, skill.Name);
            Add(uiSkill);
        }
    }
}


public class UISkill : VisualElement{

    CircleCooldown circleCoolDown;

    public UISkill(Texture icon, string name) {
        this.AddToClassList("skill");

        this.name = name;

        Image image = new Image();
        image.style.position = Position.Absolute;
        image.image = icon;

        Add(image);

        circleCoolDown = new CircleCooldown();
        image.style.position = Position.Absolute;
        
        circleCoolDown.Radius = 40;
        circleCoolDown.style.rotate = new StyleRotate(new Rotate(-90));

        Add(circleCoolDown);
        circleCoolDown.BringToFront();

    }

    // value is percent remaining
    public void UpdateCooldown(float value) {
        circleCoolDown.UpdateAngle(value * 360);
    }
}

public class CircleCooldown: VisualElement {

    private float angle = 0;
 
    public float Radius { get; set; }

    public CircleCooldown() {
        this.generateVisualContent += GenerateCircle;
    }

    private void GenerateCircle(MeshGenerationContext context) {
        if (angle <= 0) return;

        Painter2D painter = context.painter2D;
        painter.fillColor = new Color(100, 100, 100, 0.5f);
        painter.BeginPath();
        //painter.Arc(new Vector2(0, 0), Radius, 0, 360 - Mathf.Min(angle, 359), ArcDirection.CounterClockwise);
        //if (angle < 360) {
        //    painter.LineTo(new Vector2(0, 0));
        //}
        //painter.Fill();

        if (angle > 360) {
            painter.LineTo(new Vector2(Radius, Radius));
            painter.LineTo(new Vector2(-Radius, Radius));
            painter.LineTo(new Vector2(-Radius, -Radius));
            painter.LineTo(new Vector2(Radius, -Radius));
            painter.Fill();
            return;
        }

        painter.LineTo(new Vector2(0, 0));

        Vector2 progressPoint = GetPoint(Radius, 360 - angle);
        painter.LineTo(progressPoint);

        if(progressPoint.y > 0 && progressPoint.x > 0) {
            painter.LineTo(new Vector2(Radius, Radius));
            painter.LineTo(new Vector2(-Radius, Radius));
            painter.LineTo(new Vector2(-Radius, -Radius));
            painter.LineTo(new Vector2(Radius, -Radius));
            painter.LineTo(new Vector2(Radius, 0));
            painter.Fill();
        }

        else if (progressPoint.y > 0 && progressPoint.x < Radius) {
            painter.LineTo(new Vector2(-Radius, Radius));
            painter.LineTo(new Vector2(-Radius, -Radius));
            painter.LineTo(new Vector2(Radius, -Radius));
            painter.LineTo(new Vector2(Radius, 0));
            painter.Fill();
        }
        else if(progressPoint.y < 0 && progressPoint.x < 0) {
            painter.LineTo(new Vector2(-Radius, -Radius));
            painter.LineTo(new Vector2(Radius, -Radius));
            painter.LineTo(new Vector2(Radius, 0));
            painter.Fill();
        }
        else if (progressPoint.y < 0 && progressPoint.x > 0) {
            painter.LineTo(new Vector2(Radius, -Radius));
            painter.LineTo(new Vector2(Radius, 0));
            painter.Fill();
        }

        painter.Stroke();

        Debug.Log(progressPoint);
        Debug.Log("Distance " + Vector2.Distance(progressPoint, new Vector2(0, 0)));



        //Debug.Log(asd(1, angle));

        //painter.LineTo(new Vector2(Radius * 2, 0));

        //painter.MoveTo(new Vector2(50, 50));
        //painter.LineTo(new Vector2(-50, 50));
        //painter.LineTo(new Vector2(-50, -50));
        //painter.LineTo(new Vector2(50, -50));
        //painter.LineTo(new Vector2(50, 50));
        //painter.ClosePath();
        //painter.fillColor = Color.red;
        //painter.Fill(FillRule.NonZero);
    }

    // Praise the gods! https://math.stackexchange.com/questions/2740317/simple-way-to-find-position-on-square-given-angle-at-center
    private Vector2 GetPoint(float R, float angle) {

        float alpha = angle - 90 * Mathf.Round((angle / 90));
        Debug.Log("Alpha: " + alpha);

        angle *= Mathf.Deg2Rad;
        alpha *= Mathf.Deg2Rad; 
       
        float x = R * Mathf.Cos(angle) / Mathf.Cos(alpha);
        float y = R * Mathf.Sin(angle) / Mathf.Cos(alpha);

        return new Vector2(x, y);

    }
 
    public void UpdateAngle(float angle) {
        this.angle = angle;
        this.MarkDirtyRepaint();
    }


}