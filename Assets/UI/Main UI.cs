using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{

    [SerializeField] private UIDocument _UIDocument;
    [SerializeField] private StyleSheet _styleSheet;

    [SerializeField] private string[] _skills;


    private UpgradeScreen _upgradeScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
    }

    private void OnValidate() {
        if (Application.isPlaying) return;

        StartCoroutine(Generate());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            _upgradeScreen.ShowScreen();
        }
        if(Input.GetKeyDown(KeyCode.U)) {
            _upgradeScreen.HideScreen();
        }
    }


    private IEnumerator Generate() {
            
        yield return null;

        VisualElement root = _UIDocument.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        _upgradeScreen = new UpgradeScreen(_skills);
        root.Add(_upgradeScreen.GetVisualElement());
        _upgradeScreen.HideScreen();

        Label label = new Label("Hello");
        root.Add(label);

        Button button = new Button();
        root.Add(button);

    }


}
