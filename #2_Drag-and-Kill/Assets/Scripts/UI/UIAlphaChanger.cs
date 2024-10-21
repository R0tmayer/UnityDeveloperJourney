using System.Collections;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class UIAlphaChanger : MonoBehaviour
{
    [SerializeField] private float _alphaSpeed;
    [SerializeField] private float _fillSpeed;

    private Image _image;


    private void Awake() => _image = GetComponent<Image>();

    public void Fill() => StartCoroutine(FillCoroutine());

    private IEnumerator FillCoroutine()
    {
        Color color = _image.color;
        float currentFill = _image.fillAmount;
        while(color.a != 1 || currentFill != 1f)
        {
            color.a = Mathf.Lerp(color.a, 1f, _alphaSpeed * Time.deltaTime);
            currentFill = Mathf.MoveTowards(currentFill, 1f, _fillSpeed * Time.deltaTime);

            _image.color = color;
            _image.fillAmount = currentFill;

            yield return null;
        }
    }
}
