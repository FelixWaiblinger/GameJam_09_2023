using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausemenuScreenController : MonoBehaviour
{
    [SerializeField] private MainUI mainUI;

    public static event Action<bool> OnPauseStatusChanged;

    [SerializeField] private BoolEventChannel _ShowPauseMenuScreenEventChannel;
    [SerializeField] private BoolEventChannel _PauseEventChannel;
    [SerializeField] private IntEventChannel _loadSceneChannel;

    [SerializeField] private InputReader _inputReader;


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
        _inputReader.ClearAllSubscribers();
        _loadSceneChannel.RaiseIntEvent(1);
    }



    private void Quit() {
        Application.Quit();
    }



}
