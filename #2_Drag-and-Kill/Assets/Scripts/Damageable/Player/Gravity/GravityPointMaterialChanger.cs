using UnityEngine;

public class GravityPointMaterialChanger : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _distortionLimits;

    private const string _distortion = "_Distortion";

    private float _currentDistortion = 0f;
    private float _targetDistortion = float.NaN;


    private void Update()
    {
        if (float.IsNaN(_targetDistortion))
            _targetDistortion = Random.Range(_distortionLimits.x, _distortionLimits.y);

        _currentDistortion = Mathf.MoveTowards(_currentDistortion, _targetDistortion, Time.deltaTime * _speed);
        _material.SetFloat(_distortion, _currentDistortion);

        if (_currentDistortion == _targetDistortion)
            _targetDistortion = float.NaN;
    }
}
