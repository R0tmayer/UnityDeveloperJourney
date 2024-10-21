using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIMover : MonoBehaviour
{
    [SerializeField] private Vector2 _targetPosition;
    [SerializeField] private Vector2 _originPosition;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMoveOnEnable = false;

    private RectTransform _rectTransform;

    private IEnumerator _moveCoroutine;


    private void OnEnable()
    {
        _rectTransform.anchoredPosition = _originPosition;
        if (_isMoveOnEnable)
            StartMovement();
    }

    private void OnDisable()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);
    }

    private void Awake() => _rectTransform = GetComponent<RectTransform>();

    public void StartMovement()
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = Move();
        StartCoroutine(_moveCoroutine);
    }

    private IEnumerator Move()
    {
        Vector2 currentPosition = _rectTransform.anchoredPosition;
        while (currentPosition != _targetPosition)
        {
            currentPosition = Vector3.Lerp(currentPosition, _targetPosition, _speed * Time.deltaTime);

            _rectTransform.anchoredPosition = currentPosition;

            yield return null;
        }
    }
}
