using System.Collections;
using UnityEngine;

public class GravityPointMaterialBorder : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Material _material;
    [SerializeField] private float _maxHue;
    [SerializeField] private float _speed;

    private const string _mainColor = "_Color";
    private const string _colorRim = "_RimColor";

    private float _currentHue;

    private IEnumerator _colorChangeCoroutine;


    //private void OnEnable() => _player.HealthChanged += HealthChange;

    //private void OnDisable() => _player.HealthChanged -= HealthChange;

    private void Start()
    {
        _currentHue = GetHuePercent(1f);

        UpdateColorHue();
    }

    private void HealthChange(float healthPercent)
    {
        if (_colorChangeCoroutine != null)
            StopCoroutine(_colorChangeCoroutine);

        _colorChangeCoroutine = HueChange(healthPercent);
        StartCoroutine(_colorChangeCoroutine);
    }

    private void UpdateColorHue()
    {
        Color color = Color.HSVToRGB(_currentHue, 1, 1);
        color.a = 0f;

        _material.SetColor(_mainColor, color);
        _material.SetColor(_colorRim, color);
    }

    private float GetHuePercent(float healthPercent) => (_maxHue * healthPercent) / 360f;

    private IEnumerator HueChange(float targetHue)
    {
        targetHue = GetHuePercent(targetHue);
        while (_currentHue != targetHue)
        {
            _currentHue = Mathf.MoveTowards(_currentHue, targetHue, _speed * Time.deltaTime);

            UpdateColorHue();

            yield return null;
        }

        _colorChangeCoroutine = null;
    }
}
