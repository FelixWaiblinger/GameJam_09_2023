using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : UIScreen
{

    public static event Action OnPlayClicked;
    public static event Action OnSettingsClicked;
    public static event Action OnQuitClicked;

    [SerializeField] private Texture gameIcon;

    protected override void Awake() {
        base.Awake();
        Generate();
    }

    private void Generate() {
        _screen.AddToClassList("mainMenu");
        AddMenuButtons(); 
    }

    private void AddMenuButtons() {
        VisualElement container = Create("menuContainer");

        Image gameIcon = new Image();
        gameIcon.image = this.gameIcon;
        container.Add(gameIcon);

        VisualElement menuButtons = Create("menuButtons");
        container.Add(menuButtons);

        Button playBtn= Create<Button>();
        playBtn.text = "Play";
        playBtn.RegisterCallback<ClickEvent>(v => OnPlayClicked?.Invoke());

        Button settingsBtn = Create<Button>();
        settingsBtn.text = "Settings";
        settingsBtn.RegisterCallback<ClickEvent>(v => OnSettingsClicked?.Invoke());

        Button quitBtn = Create<Button>();
        quitBtn.text = "Quit";
        quitBtn.RegisterCallback<ClickEvent>(v => OnQuitClicked?.Invoke());

        menuButtons.Add(playBtn);
        menuButtons.Add(settingsBtn);
        menuButtons.Add(quitBtn);

        _screen.Add(container);
    }

}
