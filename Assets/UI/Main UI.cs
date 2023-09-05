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

    [SerializeField] private int workingOn;


    private UpgradeScreen _upgradeScreen;
    private MainMenuScreen _mainMenuScreen;
    private HUDScreen _hudScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Generate());
        ShowScreen(workingOn);
    }

    private void OnValidate() {
        if (Application.isPlaying) return;

        StartCoroutine(Generate());
        

    }

    private void ShowScreen(int workingOn) {
        if (workingOn == 1) {
            _mainMenuScreen.ShowScreen();
            _upgradeScreen.HideScreen();
            _hudScreen.HideScreen();
        }
        if (workingOn == 2) {
            _mainMenuScreen.HideScreen();
            _upgradeScreen.ShowScreen();
            _hudScreen.HideScreen();
        }
        if (workingOn == 3) {
            _mainMenuScreen.HideScreen();
            _upgradeScreen.HideScreen();
            _hudScreen.ShowScreen();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ShowScreen(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            ShowScreen(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ShowScreen(3);
        }
    }


    private IEnumerator Generate() {
            
        yield return null;

        VisualElement root = _UIDocument.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        _upgradeScreen = new UpgradeScreen(_skills);
        root.Add(_upgradeScreen.GetVisualElement());

        _mainMenuScreen = new MainMenuScreen();
        root.Add(_mainMenuScreen.GetVisualElement());

        _hudScreen = new HUDScreen();
        root.Add(_hudScreen.GetVisualElement());

        ShowScreen(workingOn);
    }


}
