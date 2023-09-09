using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausemenuScreenController : MonoBehaviour
{

    public static event Action<bool> OnPauseStatusChanged;

    [SerializeField] private BoolEventChannel _ShowPauseMenuScreenEventChannel;
    [SerializeField] private BoolEventChannel _PauseEventChannel;

    private void OnEnable() {
        PauseMenuScreen.OnResumeClicked += Resume;
        PauseMenuScreen.OnSettingsClicked += Resume;
        PauseMenuScreen.OnMainMenuClicked += MainMenu;
        PauseMenuScreen.OnQuitClicked += Quit;
        _ShowPauseMenuScreenEventChannel.OnBoolEventRaised += ShowPauseMenu;

    }
    private void OnDisable() {
        PauseMenuScreen.OnResumeClicked -= Resume;
        PauseMenuScreen.OnSettingsClicked -= Resume;
        PauseMenuScreen.OnMainMenuClicked -= MainMenu;
        PauseMenuScreen.OnQuitClicked -= Quit;
        _ShowPauseMenuScreenEventChannel.OnBoolEventRaised -= ShowPauseMenu;
    }

    private void ShowPauseMenu(bool show) {
        OnPauseStatusChanged?.Invoke(show);
    }

    private void Resume() {
        _PauseEventChannel.RaiseBoolEvent(false);
    }

    private void MainMenu() {
    }


    private void Quit() {
        Application.Quit();
    }



}
