using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;


    [Header("InputReader")]
    [SerializeField] private InputReader _input;
    [SerializeField] private VoidEventChannel _deathEvent;

    private void Awake() {
        if(_instance == null) {
            _instance = this;
        }else {
            Destroy(this.gameObject);
        }
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneChanged;
        _deathEvent.OnVoidEventRaised += Disable;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChanged;
        _deathEvent.OnVoidEventRaised -= Disable;

    }

    void SceneChanged(Scene _, LoadSceneMode __)
    {
        _input.InitGameInput();
        if (SceneManager.sceneCount > 1) _input.ClearAllSubscribers();
    }

    void Disable()
    {
        _input.DisablePlayerInputs();
    }
}
