using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{

    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate() {
        if (Application.isPlaying) return;

        StartCoroutine(Generate());
    }


    private IEnumerator Generate() {
            
        yield return null;

        VisualElement root = _document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        Label label = new Label("Hello");
        root.Add(label);


        SkillpointComponent<String> skillPoint = new SkillpointComponent<String>(root, "NONONONO");
        SkillpointComponent<String>.OnClicked += DoSomething;

        Button button = new Button();
        root.Add(button);

    }

    private void DoSomething(String text) {
        Debug.Log("Hello World " + text);
    }

}
