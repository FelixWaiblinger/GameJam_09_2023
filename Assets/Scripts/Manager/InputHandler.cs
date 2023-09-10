using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;


    [Header("InputReader")]
    [SerializeField] private InputReader _input;

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
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneChanged;
    }

    void SceneChanged(Scene _, LoadSceneMode __)
    {
        _input.InitGameInput();
        if (SceneManager.sceneCount >= 1) _input.ClearAllSubscribers();
    }
}
