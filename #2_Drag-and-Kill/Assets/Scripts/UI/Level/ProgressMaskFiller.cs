using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressMaskFiller : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ProgressDisplay _display;
    [SerializeField] private float _fillSpeed;

    private readonly List<MaskElement> _maskElements = new List<MaskElement>();
    private struct MaskElement
    {
        public readonly float Length;
        private readonly Image _image;

        public MaskElement(Image image)
        {
            Length = image.GetComponent<RectTransform>().rect.width;
            _image = image;
        }

        public void Fill(float fillAmount) => _image.fillAmount = fillAmount;
    }

    private float _totalLength;
    private float _currentFill = 0f;
    private float _targetFill = 0f;


    private void OnEnable()
    {
        _player.LevelProgressChanged += SetTargetFill;
        _display.ProgressUpdated += Restart;
    }

    private void OnDisable()
    {
        _player.LevelProgressChanged -= SetTargetFill;
        _display.ProgressUpdated -= Restart;
    }

    private void Start()
    {
        SetMaskElements();

        _totalLength = GetTotalLength();
    }

    private void Update()
    {
        _currentFill = Mathf.MoveTowards(_currentFill, _targetFill, _fillSpeed * Time.deltaTime);

        Fill();
    }

    private void Restart()
    {
        _currentFill = 0f;
        _targetFill = 0f;
        foreach (MaskElement element in _maskElements)
            element.Fill(0f);
    }

    private void SetTargetFill(float levelPercent) => _targetFill = ConvertToTotalLength(levelPercent);

    private void Fill()
    {
        float fillAmount = _currentFill;
        for (int i = 0; i < _maskElements.Count; i++)
        {
            if (fillAmount >= _maskElements[i].Length)
            {
                _maskElements[i].Fill(1f);
                
                fillAmount -= _maskElements[i].Length;
            }
            else
            {
                float percent = ConvertToElementPercent(_maskElements[i].Length, fillAmount);

                _maskElements[i].Fill(percent);

                fillAmount = 0f;
            }
        }
    }

    private void SetMaskElements()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
            _maskElements.Add(new MaskElement(image));
    }

    private float ConvertToElementPercent(float fullLength, float length) => length / fullLength;

    private float ConvertToTotalLength(float completePercent) => _totalLength * completePercent;

    private float GetTotalLength()
    {
        float length = 0f;
        foreach (MaskElement element in _maskElements)
            length += element.Length;

        return length;
    }
}
