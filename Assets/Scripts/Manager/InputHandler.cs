using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    [Header("InputReader")]
    [SerializeField] private InputReader _input;

    void OnEnable()
    {
        SceneManager.sceneLoaded += (_, _) => _input.InitGameInput();
    }

    void OnDisable()
    {
        
    }
}
