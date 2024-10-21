using System;
using System.Collections;
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
        [SerializeField] [Required] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] [Required] private TMP_Text _text;
        private InputJoystickReceiver _input;
        private bool _pressed;

        public void Construct(InputJoystickReceiver input) => _input = input;

        private void Awake()
        {
            _text.DOFade(0, 1)
                .SetLoops(int.MaxValue, LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetId(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_pressed) return;
            
            StartCoroutine(EnableInputAfterDelay());
        }

        private IEnumerator EnableInputAfterDelay()
        {
            DOTween.Kill(this);
            _pressed = true;
            _virtualCamera.Priority = 1;
            _text.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            _input.Enabled = true;
        }

    }
}