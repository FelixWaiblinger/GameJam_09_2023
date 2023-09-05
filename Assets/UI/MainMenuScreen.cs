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

    public MainMenuScreen() {
        Generate();
    }

    private void Generate() {
        _screen.AddToClassList("mainMenu");
        _screen.Add(new Label("Welcome to our game!"));
        AddMenuButtons(); 
    }

    private void AddMenuButtons() {
        VisualElement menuButtons = Create("mainMenuButtons");

        Button playBtn= Create<Button>();
        playBtn.text = "Play";
        Button settingsBtn = Create<Button>();
        settingsBtn.text = "Settings";
        Button exitBtn = Create<Button>();
        exitBtn.text = "Quit";

        menuButtons.Add(playBtn);
        menuButtons.Add(settingsBtn);
        menuButtons.Add(exitBtn);

        _screen.Add(menuButtons);
    }
}
