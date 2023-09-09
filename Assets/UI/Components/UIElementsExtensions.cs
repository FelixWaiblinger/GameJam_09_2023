using UnityEngine.UIElements;

public static class UIElementsExtensions {

    public static T Create<T>(this VisualElement ele, params string[] classNames) where T : VisualElement, new() {
        T element = new T();
        foreach (string className in classNames) {
            element.AddToClassList(className);
        }

        return element;
    }
}

