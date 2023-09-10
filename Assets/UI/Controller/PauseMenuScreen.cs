using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : UIScreen
{
    public static event Action OnResumeClicked;
    public static event Action OnSettingsClicked;
    public static event Action OnMainMenuClicked;
    public static event Action OnQuitClicked;

    [SerializeField] private Texture gameIcon;

    protected override void Awake() {
        base.Awake();
        _screen.AddToClassList("pauseMenu");

        Generate();
    }

    private void OnEnable() {
        PausemenuScreenController.OnPauseStatusChanged += ShowScreen;
    }

    private void OnDisable() {
        PausemenuScreenController.OnPauseStatusChanged -= ShowScreen;
    }

    private void ShowScreen(bool shouldShow) {
        if (shouldShow) {
            ShowScreen();
        } else {
            HideScreen();
        }
    }


    private void Generate() {
        VisualElement container = Create("menuContainer");

        Image gameIcon = new Image();
        gameIcon.image = this.gameIcon;
        container.Add(gameIcon);

        VisualElement menuButtons = Create("menuButtons");
        container.Add(menuButtons);

        Button resumeBtn = Create<Button>();
        resumeBtn.text = "Resume";
        resumeBtn.RegisterCallback<ClickEvent>(v => OnResumeClicked?.Invoke());
        menuButtons.Add(resumeBtn);

        Button mainMenuBtn = Create<Button>();
        mainMenuBtn.text = "Mainmenu";
        mainMenuBtn.RegisterCallback<ClickEvent>(v => OnMainMenuClicked?.Invoke());
        menuButtons.Add(mainMenuBtn);

        Button settingsBtn = Create<Button>();
        settingsBtn.text = "Settings";
        settingsBtn.RegisterCallback<ClickEvent>(v => OnSettingsClicked?.Invoke());
        menuButtons.Add(settingsBtn);

        Button quitBtn = Create<Button>();
        quitBtn.text = "Quit";
        quitBtn.RegisterCallback<ClickEvent>(v => OnQuitClicked?.Invoke());
        menuButtons.Add(quitBtn);

        _screen.Add(container);
    }
}
