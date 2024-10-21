using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class FillBar : MonoBehaviour
    {
        [SerializeField] [Required] private Image _fillImage;

        public void UpdateValue(float value)
        {
            gameObject.SetActive(true);
            _fillImage.fillAmount = value;
        }
    }
}