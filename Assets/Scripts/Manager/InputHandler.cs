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
        SceneManager.sceneLoaded += (_, _) => _input.InitGameInput();
    }

    void OnDisable()
    {
        
    }
}
