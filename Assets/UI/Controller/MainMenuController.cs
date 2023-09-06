using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private void OnEnable() {
        MainMenuScreen.OnPlayClicked += DoSomething;
        MainMenuScreen.OnSettingsClicked += DoSomething;
        MainMenuScreen.OnQuitClicked += DoSomething;
    }
    private void OnDisable() {
        MainMenuScreen.OnPlayClicked -= DoSomething;
        MainMenuScreen.OnSettingsClicked -= DoSomething;
        MainMenuScreen.OnQuitClicked -= DoSomething;
    }

    private void DoSomething() {
        Debug.Log("Action from MainMenuScreen triggered");
    }

}
