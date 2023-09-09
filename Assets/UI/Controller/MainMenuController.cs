using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public static event Action<GameData> OnRunStarted;

    [SerializeField] private IntEventChannel loadSceneChannel;
    [SerializeField] private GameData gameData;

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
        OnRunStarted?.Invoke(gameData);
    }

    private void Quit() {
        Application.Quit();
    }

}
