using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private FloatEventChannel _timeScaleEvent;
    [SerializeField] private float _timeSmoothness;
    private float _targetTimeScale = 1;

    void OnEnable()
    {
        _timeScaleEvent.OnFloatEventRaised += (scale) => _targetTimeScale = scale;
    }

    void Update()
    {
        if (Time.timeScale == _targetTimeScale) return;

        Time.timeScale = Mathf.Lerp(Time.timeScale, _targetTimeScale, _timeSmoothness);
    }
}
