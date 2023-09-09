using UnityEngine;
using TMPro;

public class FPSHandler : MonoBehaviour
{
    [Header("Framerate")]
    [SerializeField] private int _targetFPS = 60;
    [SerializeField] private bool _showFPS = false;
    [SerializeField] private Canvas _framesUI;
    [SerializeField] private TMP_Text _FPS;

    private float _timer = 1;

    void Start()
    {
        Application.targetFrameRate = _targetFPS;
        if (_showFPS) _framesUI.enabled = true;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1)
        {
            _FPS.text = $"{(int)(1 / Time.deltaTime)} FPS";
            _timer = 0;
        }
    }
}
