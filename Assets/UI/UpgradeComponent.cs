using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradeComponent<T> where T : class {

    public static event Action<T> OnClicked;

    private VisualElement _parent;

    public UpgradeComponent(VisualElement parent, T value) {
        _parent = parent;

        AddButtonToParent(value);
    }

    private void AddButtonToParent(T value) {
        Button button = new Button();
        button.text = value.ToString();
        button.userData = value;
        button.RegisterCallback<ClickEvent>(OnButtonClick);

        _parent.Add(button);    
    }

    private void OnButtonClick(ClickEvent evt) {
        VisualElement target = evt.currentTarget as VisualElement;
        T userData = target.userData as T;

        OnClicked?.Invoke(userData);
    }

}