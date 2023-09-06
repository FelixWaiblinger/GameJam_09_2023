using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    [SerializeField] private UIDocument _UIDocument;
    [SerializeField] private StyleSheet _styleSheet;

    [SerializeField] private int workingOn;
    [SerializeField] private int screenAmount;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private UpgradeScreen _upgradeScreen;
    [SerializeField] private HUDScreen _hudScreen;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElements();
        ShowScreen(workingOn);
    }

    private void OnEnable() {
        InputReader.pauseEvent += DoSomething;
    }

    private void OnDisable() {
        InputReader.pauseEvent -= DoSomething;
    }

    void Update() {
        ShowScreen(workingOn);
    }

    public void DoSomething() {
        workingOn %= screenAmount;
        workingOn++;
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

    private void SetVisualElements() {
        VisualElement root = _UIDocument.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        root.Add(_upgradeScreen.GetVisualElement());
        root.Add(_mainMenuScreen.GetVisualElement());
        root.Add(_hudScreen.GetVisualElement());
    }
}
