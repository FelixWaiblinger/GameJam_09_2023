using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private IntEventChannel loadSceneChannel;

    public static event Action OnRunStarted;

    private void OnEnable() {
        MainMenuScreen.OnPlayClicked += StartRun;
        MainMenuScreen.OnSettingsClicked += StartRun;
        MainMenuScreen.OnQuitClicked += Quit;
    }
    private void OnDisable() {
        MainMenuScreen.OnPlayClicked -= StartRun;
        MainMenuScreen.OnSettingsClicked -= StartRun;
        MainMenuScreen.OnQuitClicked -= Quit;
    }

    private void StartRun() {
        loadSceneChannel.RaiseIntEvent(SceneManager.GetActiveScene().buildIndex + 1);
        OnRunStarted?.Invoke();
    }

    private void Quit() {
        Application.Quit();
    }

}
