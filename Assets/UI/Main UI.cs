using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    [SerializeField] private UIDocument _UIDocument;
    [SerializeField] private StyleSheet _styleSheet;

    [SerializeField] private UIScreens currentScreen;

    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private UpgradeScreen _upgradeScreen;
    [SerializeField] private HUDScreen _hudScreen;
    [SerializeField] private PauseMenuScreen _pauseMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElements();
        ShowScreen(currentScreen);
    }

    private void OnEnable() {
        MainMenuController.OnRunStarted += ShowHUD;
        InputReader.upgradeMenuEvent += ToggleUpgradeMenu;
    }

    private void ToggleUpgradeMenu() {
        if (currentScreen != UIScreens.UpgradeMenu) {
            ShowScreen(UIScreens.UpgradeMenu);
        } else {
            ShowScreen(UIScreens.HUD);
        }
    }

    private void ShowHUD(GameData gameData) {
        ShowScreen(UIScreens.HUD);
    }

    private void OnDisable() {
        MainMenuController.OnRunStarted -= ShowHUD;
        InputReader.upgradeMenuEvent -= ToggleUpgradeMenu;
    }

    private UIScreens GetNextMode(UIScreens current) {
        Array modes = Enum.GetValues(current.GetType());

        int index = Array.IndexOf(modes, current);
        index = (index + 1) % modes.Length;
        return (UIScreens)modes.GetValue(index);
    }

    private void ShowScreen(UIScreens uiScreen) {
        HideAll();
        currentScreen = uiScreen;

        switch (uiScreen) {
            case UIScreens.MainMenu:
                _mainMenuScreen.ShowScreen();
                break;
            case UIScreens.UpgradeMenu:
                _upgradeScreen.ShowScreen();
                break;
            case UIScreens.HUD:
                _hudScreen.ShowScreen();
                break;
            case UIScreens.PauseMenu:
                _pauseMenuScreen.ShowScreen();
                break;
            default:
                break; throw new NotImplementedException();
        }
    }

    private void HideAll() {
        _mainMenuScreen.HideScreen();
        _upgradeScreen.HideScreen();
        _hudScreen.HideScreen();
        _pauseMenuScreen.HideScreen();
    }

    private void SetVisualElements() {
        VisualElement root = _UIDocument.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);

        root.Add(_upgradeScreen.GetVisualElement());
        root.Add(_mainMenuScreen.GetVisualElement());
        root.Add(_hudScreen.GetVisualElement());
        root.Add(_pauseMenuScreen.GetVisualElement());
    }
}

public enum UIScreens {
    MainMenu,
    UpgradeMenu,
    HUD,
    PauseMenu
}
