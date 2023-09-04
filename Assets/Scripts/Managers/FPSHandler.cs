using UnityEngine;
using TMPro;

public class FPSHandler : MonoBehaviour
{
    [Header("Framerate")]
    [Tooltip("Framerate to target at all times")]
    [SerializeField] private int _targetFPS = 60;
    [Tooltip("Display current framerate in top-right corner")]
    [SerializeField] private bool _showFPS = false;
    [Tooltip("Canvas to display the framerate in")]
    [SerializeField] private Canvas _framesUI;
    [Tooltip("Text box in top-right corner")]
    [SerializeField] private TMP_Text _FPS;

    void Start()
    {
        Application.targetFrameRate = _targetFPS;
        if (_showFPS) _framesUI.enabled = true;
    }

    void FixedUpdate() // yes this is intentional
    {
        _FPS.text = $"{(int)(1 / Time.deltaTime)} FPS";
    }
}
