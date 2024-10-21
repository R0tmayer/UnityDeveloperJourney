using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.UI;

public class RestartLevelButton : MonoBehaviour
{
    [SerializeField] private Button _restartLevelButton;
    
    private CubesHolder _cubesHolder;
    private SplineFollower _splineFollower;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _cubesHolder = FindObjectOfType<CubesHolder>();
        _splineFollower = _cubesHolder.GetComponent<SplineFollower>();
        
        _splineFollower.followSpeed = 0;

        _restartLevelButton.onClick.AddListener(() => 
        {
            _splineFollower.followSpeed = 10;
            UISingleton.Instance.LoadNewScene();
        });
    }

    private void OnDestroy()
    {
        _restartLevelButton.onClick.RemoveAllListeners();
    }

}
