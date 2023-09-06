using UnityEngine.UIElements;

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

