using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeComponent<T> where T : class {

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
        button.name = value.ToString();
        button.text = value.ToString();
        button.userData = value;
        button.RegisterCallback<MouseOverEvent>(OnMouseEntered);
        button.RegisterCallback<MouseOutEvent>(OnMouseLeaved);
        button.RegisterCallback<ClickEvent>(OnButtonClick);

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