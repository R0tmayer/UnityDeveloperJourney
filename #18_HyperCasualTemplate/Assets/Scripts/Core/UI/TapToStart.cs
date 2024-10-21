using System;
using Cinemachine;
using Core.Input;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class TapToStart : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] [Required] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] [Required] private TMP_Text _text;

        private InputJoystickReceiver _input;
        private GameParameters _gameParameters;

        public void Construct(InputJoystickReceiver input, GameParameters gameParameters)
        {
            _gameParameters = gameParameters;
            _input = input;
        }

        private void Start()
        {
            _input.Enabled = _gameParameters.FirstTouchInput;
            
            _text.DOFade(0, _gameParameters.TapToStartDuration)
                .SetId(this)
                .SetEase(_gameParameters.TapToStartEase)
                .SetLoops(int.MaxValue, LoopType.Yoyo);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DOTween.Kill(this);
            _mainCamera.Priority = 100;
            _text.gameObject.SetActive(false);
            _input.Enabled = true;
        }
    }
}