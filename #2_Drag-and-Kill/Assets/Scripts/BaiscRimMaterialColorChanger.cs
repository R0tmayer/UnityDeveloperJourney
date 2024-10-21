using System.Collections;
using UnityEngine;

public class BaiscRimMaterialColorChanger : MonoBehaviour
{
    [SerializeReference] private GameObject _eventContainer;
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _maxHue;
    [SerializeField] private float _speed;

    private const string _mainColor = "_Color";
    private const string _colorRim = "_RimColor";

    private float _currentHue;

    private IEnumerator _colorChangeCoroutine;


    private void OnEnable()
    {
        if (_eventContainer.TryGetComponent(out IFloatEventable eventFloat))
            eventFloat.ValueSetted += ValueChange;
    }

    private void OnDisable()
    {
        if (_eventContainer.TryGetComponent(out IFloatEventable eventFloat))
            eventFloat.ValueSetted -= ValueChange;
    }

    private void Start()
    {
        _currentHue = GetHuePercent(1f);

        UpdateColorHue();
    }

    private void ValueChange(float valuePercent)
    {
        if (_colorChangeCoroutine != null)
            StopCoroutine(_colorChangeCoroutine);

        _colorChangeCoroutine = HueChange(valuePercent);
        StartCoroutine(_colorChangeCoroutine);
    }

    private void UpdateColorHue()
    {
        Color color = Color.HSVToRGB(_currentHue, 1, 1);
        color.a = 0f;

        foreach(Material material in _materials)
        {
            material.SetColor(_mainColor, color);
            material.SetColor(_colorRim, color);
        }
    }

    private float GetHuePercent(float valuePercent) => (_maxHue * valuePercent) / 360f;

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
