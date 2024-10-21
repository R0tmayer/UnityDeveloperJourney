using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPointTransform : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _scaleChangementSpeed;
    [SerializeField] private Vector2 _scaleLimits;
    [SerializeField] private float _scaleStep;
    [SerializeField] private float _bonusScaleModifier;

    private void OnEnable() => _player.LevelUpped += SetScale;

    private void OnDisable() => _player.LevelUpped -= SetScale;

    private void Update() => transform.rotation = Quaternion.Euler(Vector3.zero);

    private void SetScale(int value)
    {
        float targetSize = _scaleLimits.x + (value * _scaleStep);
        if (targetSize >= _scaleLimits.y)
            targetSize = _scaleLimits.y;

        StopAllCoroutines();
        StartCoroutine(ScaleChange(targetSize));
    }

    private IEnumerator ScaleChange(float targetSize)
    {
        Vector3 targetScale = Vector3.one * (targetSize);
        Vector3 bonusScale = targetScale * _bonusScaleModifier;
        while ((transform.localScale - bonusScale).magnitude > 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, bonusScale, _scaleChangementSpeed * Time.deltaTime);

            yield return null;
        }

        while (targetScale != transform.localScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, _scaleChangementSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
