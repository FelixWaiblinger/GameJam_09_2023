using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

public abstract class UIScreen : MonoBehaviour
{
    protected VisualElement _screen;
    public VisualElement GetVisualElement() { return _screen; }

    protected virtual void Awake() {
        _screen = Create("screen");
    }

    public virtual void ShowScreen() {
        _screen.style.display = DisplayStyle.Flex;
    }

    public virtual void HideScreen() {
        _screen.style.display = DisplayStyle.None;
    }

    // Convenience Method
    protected static VisualElement Create(params string[] classNames) {
        return Create<VisualElement>(classNames);
    }

    // Convenience Method
    protected static T Create<T>(params string[] classNames) where T : VisualElement, new(){
        T element = new T();
        foreach (string className in classNames) {
            element.AddToClassList(className);
        }

        return element;
    }

}
