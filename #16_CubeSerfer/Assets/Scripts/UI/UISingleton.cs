using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISingleton : MonoBehaviour
{
    public static UISingleton Instance;

    private Canvas _mainCanvas;

    private const string _tapToStartButton = "Assets/Prefabs/UI/TapToStartButton.prefab";
    private const string _nextLevelButton = "Assets/Prefabs/UI/NextLevel.prefab";
    private const string _restartLevelButton = "Assets/Prefabs/UI/RestartLevel.prefab";

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        var spawned = new GameObject();
        _mainCanvas = spawned.AddComponent<Canvas>();
        _mainCanvas.name = "Canvas";
        _mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        _mainCanvas.gameObject.AddComponent<GraphicRaycaster>();
        _mainCanvas.gameObject.AddComponent<CanvasScaler>();
        DontDestroyOnLoad(_mainCanvas);
    }

    private void Start()
    {
        ShowTapToStartButton();
    }

    private void ClearCanvas()
    {
        foreach (Transform child in _mainCanvas.transform)
        {
            Addressables.ReleaseInstance(child.gameObject);
        }
    }

    private void CreateUIElement(string path)
    {
        ClearCanvas();
        Addressables.InstantiateAsync(path, _mainCanvas.transform);
    }

    public void ShowTapToStartButton()
    {
        CreateUIElement(_tapToStartButton);
    }

    public void ShowNextLevelButton()
    {
        CreateUIElement(_nextLevelButton);
    }

    public void LoadNewScene()
    {
        ClearCanvas();
        SceneManager.LoadSceneAsync(PlayerProgress.CURRENT_LEVEL);
    }

    public void ShowRestartLevelButton()
    {
        CreateUIElement(_restartLevelButton);
    }

}
