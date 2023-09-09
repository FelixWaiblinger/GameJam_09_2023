using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [Header("Scene management")]
    [SerializeField] private IntEventChannel _sceneEvent;
    [SerializeField] private Canvas _loadingScreen;
    [SerializeField] private Image _progressBar;

    private float _targetProgress = 0;

    #region SETUP

    void Awake()
    {
        if (SceneManager.sceneCount == 1)
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    void OnEnable()
    {
        _sceneEvent.OnIntEventRaised += LoadScene;
        SceneManager.sceneLoaded += (scene, _) => SceneManager.SetActiveScene(scene);
    }

    void OnDisable()
    {
        _sceneEvent.OnIntEventRaised -= LoadScene;
    }

    #endregion

    void Update()
    {
        if (!_loadingScreen.enabled) return;

        _progressBar.fillAmount = Mathf.MoveTowards(
            _progressBar.fillAmount,
            _targetProgress,
            3 * Time.deltaTime
        );
    }

    #region EVENT

    async void LoadScene(int sceneIndex)
    {
        _targetProgress = 0;

        // load next scene (but dont show yet)
        var scene = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;

        // show loading screen
        _loadingScreen.enabled = true;

        do
        {
            _targetProgress = scene.progress;
            await System.Threading.Tasks.Task.Delay(100); // remove later
        }
        while (scene.progress < 0.9f);

        // remove current scene
        var indexToRemove = SceneManager.GetSceneAt(1).buildIndex;
        var unload = SceneManager.UnloadSceneAsync(indexToRemove);

        scene.allowSceneActivation = true;
        _loadingScreen.enabled = false;
    }

    #endregion
}
