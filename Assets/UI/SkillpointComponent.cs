using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillpointComponent<T> where T : class {

    public static event Action<T> OnClicked;

    private VisualElement _parent;

    public SkillpointComponent(VisualElement parent, T value) {
        _parent = parent;

        AddButtonToParent(value);
    }

    private void AddButtonToParent(T value) {
        Button button = new Button();
        button.text = "SkillpointLabel";
        button.name = "This is my button name";
        button.userData = value;
        button.RegisterCallback<ClickEvent>(OnButtonClick);

        _parent.Add(button);    
    }

    private void OnButtonClick(ClickEvent evt) {
        VisualElement target = evt.currentTarget as VisualElement;
        Debug.Log(target.name);
        T userData = target.userData as T;

        OnClicked?.Invoke(userData);
    }

}