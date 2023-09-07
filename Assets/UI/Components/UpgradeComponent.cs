using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeComponent<T> where T : SkillNodeSO {

    public static event Action<T> OnClicked;
    public static event Action<T, Vector2> OnMouseOver;
    public static event Action<T, Vector2> OnMouseOut;

    private VisualElement _parent;

    public UpgradeComponent(VisualElement parent, T value) {
        _parent = parent;

        AddButtonToParent(value);
    }

    private void AddButtonToParent(T value) {
        Button button = new Button();
        button.name = value.Identifier;
        button.userData = value;
        button.RegisterCallback<MouseOverEvent>(OnMouseEntered);
        button.RegisterCallback<MouseOutEvent>(OnMouseLeaved);
        button.RegisterCallback<ClickEvent>(OnButtonClick);

        Image img = new Image();
        img.image = value.image;
        img.BringToFront();

        button.Add(img);

        _parent.Add(button);    
    }

    private void OnMouseLeaved(MouseOutEvent evt) {
        VisualElement target = evt.currentTarget as VisualElement;
        T userData = target.userData as T;

        OnMouseOut?.Invoke(userData, target.worldBound.position);
    }

    private void OnMouseEntered(MouseOverEvent evt) {
        VisualElement target = evt.currentTarget as VisualElement;
        T userData = target.userData as T;

        OnMouseOver?.Invoke(userData, target.worldBound.position);
    }

    private void OnButtonClick(ClickEvent evt) {
        VisualElement target = evt.currentTarget as VisualElement;
        T userData = target.userData as T;

        OnClicked?.Invoke(userData);
    }
}