using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButton : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _nextLevelButton.onClick.AddListener(() => { UISingleton.Instance.LoadNewScene(); });
    }

    private void OnDestroy()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
    }

}
