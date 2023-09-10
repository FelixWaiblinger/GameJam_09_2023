using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] private BoolEventChannel _pauseEventChannel;
    [SerializeField] private BoolEventChannel _showPauseMenuEventChannel;
    [SerializeField] private FloatEventChannel _timeScaleEventChannel;


    [SerializeField] private InputReader _inputReader;

    private void OnEnable() {
        InputReader.pauseEvent += OnPauseInput;
        _pauseEventChannel.OnBoolEventRaised += OnPauseEvent;
    }

    private void OnDisable() {
        InputReader.pauseEvent -= OnPauseInput;
        _pauseEventChannel.OnBoolEventRaised -= OnPauseEvent;
    }

    private void OnPauseEvent(bool IsPaused) {
        isPaused = IsPaused;
        // Do stuff when we pause the game
        _showPauseMenuEventChannel.RaiseBoolEvent(isPaused);
        _timeScaleEventChannel.RaiseFloatEvent(isPaused ? 0 : 1);

        SetPlayerInputsActive(!isPaused);
    }

    private void SetPlayerInputsActive(bool setActive) {
        if (setActive) {
            _inputReader.EnablePlayerInputs();
        } else {
            _inputReader.DisablePlayerInputs();
        }
    } 

    private void OnPauseInput() {
        OnPauseEvent(!isPaused);
    }

}
