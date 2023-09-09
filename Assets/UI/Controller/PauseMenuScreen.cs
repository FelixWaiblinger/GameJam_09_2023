using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : UIScreen
{
    public static event Action OnResumeClicked;
    public static event Action OnSettingsClicked;
    public static event Action OnQuitClicked;

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
        Debug.Log("We are here" + shouldShow);

        if (shouldShow) {
            ShowScreen();
        } else {
            HideScreen();
        }
    }


    private void Generate() {
        VisualElement container = Create("menuContainer");
        VisualElement menuButtons = Create("menuButtons");
        container.Add(menuButtons);

        Button resumeBtn = Create<Button>();
        resumeBtn.text = "Resume";
        resumeBtn.RegisterCallback<ClickEvent>(v => OnResumeClicked?.Invoke());

        Button settingsBtn = Create<Button>();
        settingsBtn.text = "Settings";
        settingsBtn.RegisterCallback<ClickEvent>(v => OnSettingsClicked?.Invoke());

        Button quitBtn = Create<Button>();
        quitBtn.text = "Quit";
        quitBtn.RegisterCallback<ClickEvent>(v => OnQuitClicked?.Invoke());

        menuButtons.Add(resumeBtn);
        menuButtons.Add(settingsBtn);
        menuButtons.Add(quitBtn);

        _screen.Add(container);
    }
}
