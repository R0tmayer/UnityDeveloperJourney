using UnityEngine;
using UnityEngine.UI;

public class ProgressFill : MonoBehaviour
{
    private CubesHolder _cubesHolder;
    private Finish _finish;
    private Image _fillImage;

    private float _fullDistance;

    private void Awake()
    {
        _fillImage = GetComponent<Image>();
        _cubesHolder = FindObjectOfType<CubesHolder>();
        _finish = FindObjectOfType<Finish>();

        _fullDistance = GetDistance();
    }

    private void Update()
    {
        var progressPercentage = 1 - (GetDistance() / _fullDistance);
        _fillImage.fillAmount = progressPercentage; 

        if(progressPercentage > 0.98f)
        {
            _fillImage.fillAmount = 1;
        }
    }

    private float GetDistance()
    {
        return (_cubesHolder.transform.position - _finish.transform.position).sqrMagnitude;
    }
}
