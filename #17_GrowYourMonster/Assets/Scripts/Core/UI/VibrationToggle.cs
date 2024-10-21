using Core.Vibrations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class VibrationToggle : MonoBehaviour, IContextToggle
    {
        [SerializeField] [Required] private Image _turnedOnImage;
        [SerializeField] [Required] private Button _button;
        private VibrationsPlayer _vibrationsPlayer;

        public void Construct(VibrationsPlayer vibrationsPlayer)
        {
            _vibrationsPlayer = vibrationsPlayer;
        }

        private void Start()
        {
            UpdateImage();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(Toggle);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Toggle);
        }

        public void Toggle()
        {
            _vibrationsPlayer.Enabled = !_vibrationsPlayer.Enabled;
            UpdateImage();
        }

        private void UpdateImage()
        {
            _turnedOnImage.gameObject.SetActive(_vibrationsPlayer.Enabled);
        }
    }
}