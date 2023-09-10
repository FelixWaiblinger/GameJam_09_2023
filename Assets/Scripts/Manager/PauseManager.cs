using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField] private BoolEventChannel _pauseEventChannel;
    [SerializeField] private BoolEventChannel _showPauseMenuEventChannel;
    [SerializeField] private FloatEventChannel _timeScaleEventChannel;

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
    }

    private void OnPauseInput() {
        OnPauseEvent(!isPaused);
    }




}
