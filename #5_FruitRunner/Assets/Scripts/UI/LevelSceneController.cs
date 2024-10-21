using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSceneController : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _loadMenuButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _loadMenuButton.onClick.AddListener(OnLoadMenuButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _loadMenuButton.onClick.RemoveListener(OnLoadMenuButtonClick);
    }

    private void OnRestartButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(StaticSceneNames.LevelSceneName);
    }

    private void OnLoadMenuButtonClick()
    {
        SceneManager.LoadScene(StaticSceneNames.MenuSceneName);
    }
}