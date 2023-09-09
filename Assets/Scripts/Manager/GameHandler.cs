using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] private VoidEventChannel _winEvent;
    [SerializeField] private VoidEventChannel _loseEvent;
    [SerializeField] private VoidEventChannel _playerDeathEvent;
    [SerializeField] private VoidEventChannel _areaClearEvent;
    [SerializeField] private FloatEventChannel _timeScaleEvent;
    [SerializeField] private FloatEventChannel _windSpeedEvent;
    [SerializeField] private StringEventChannel _musicEvent;
    
    private int _clearedAreas = 0;
    private float _gameOverTimer = 0;

    #region SETUP

    void OnEnable()
    {
        _playerDeathEvent.OnVoidEventRaised += () => _gameOverTimer = 3;
        _areaClearEvent.OnVoidEventRaised += RegisterAreaClear;
        _windSpeedEvent.OnFloatEventRaised += WindHasChanged;
    }

    void OnDisable()
    {
        _areaClearEvent.OnVoidEventRaised -= RegisterAreaClear;
    }

    void Start()
    {
        _musicEvent.RaiseStringEvent("Game");
    }

    #endregion

    void Update()
    {
        if (_gameOverTimer > 0)
        {
            _gameOverTimer -= Time.deltaTime;
            if (_gameOverTimer <= 0)
            {
                _loseEvent.RaiseVoidEvent();
                _timeScaleEvent.RaiseFloatEvent(0);
            }
        }

        if (3 < 4)
        {
            _windSpeedEvent.RaiseFloatEvent(5);
        }
    }

    void RegisterAreaClear()
    {
        _clearedAreas++;

        if (_clearedAreas == 1) _winEvent.RaiseVoidEvent();
    }

    void WindHasChanged(float speed)
    {
        // do something
    }
}
