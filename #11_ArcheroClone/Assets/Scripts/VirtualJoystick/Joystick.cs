using System;
using Assets.Scripts.Character;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.VirtualJoystick
{
    public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] [Required] private MovementController _movementController;
        [SerializeField] [Required] private Image _joystickBackground;
        [SerializeField] [Required] private Image _joystick;
        [SerializeField] [Required] [Min(0f)] private float _deadZoneDiameter;
        private float _deadZoneRadius;

        private Vector3 _direction;
        private float _joystickRadius;
        private Vector2 _sizeDelta;

        private void Start()
        {
            _sizeDelta = _joystickBackground.rectTransform.sizeDelta;
            _joystickRadius = _sizeDelta.x / 2f;
            _deadZoneRadius = _deadZoneDiameter / 2f;
        }

        private void Update()
        {
            _movementController.MoveInDirection(_direction);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform,
                    eventData.position, eventData.pressEventCamera, out Vector2 mouseInput))
            {
                Vector2 normalizedDirection = new Vector2(mouseInput.x, mouseInput.y).normalized;

                if (CheckMouseOverJoystick(mouseInput, normalizedDirection)) return;
                if (CheckMouseInsideDeadZone(mouseInput)) return;

                MoveJoystickNormally(mouseInput);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _joystickBackground.gameObject.SetActive(true);
            _joystickBackground.transform.position = eventData.position;
            OnDrag(eventData);
            JoystickClickDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _direction = default;
            _movementController.CanRun = false;
            _joystick.rectTransform.anchoredPosition = default(Vector3);
            _joystickBackground.gameObject.SetActive(false);
            JoystickClickUp?.Invoke();
        }

        public event Action JoystickClickDown;
        public event Action JoystickClickUp;

        private void MoveJoystickNormally(Vector2 mouseInput)
        {
            mouseInput.x /= _sizeDelta.x;
            mouseInput.y /= _sizeDelta.y;

            _joystick.rectTransform.anchoredPosition =
                new Vector2(mouseInput.x * _joystickRadius * 2f, mouseInput.y * _joystickRadius * 2f);

            _direction = new Vector3(mouseInput.x, 0, mouseInput.y);
            _movementController.CanRun = true;
        }

        private bool CheckMouseInsideDeadZone(Vector2 mouseInput)
        {
            MoveJoystickNormally(mouseInput);

            if (CircleRadiusWithZeroCenter(mouseInput.x, mouseInput.y) > _deadZoneRadius) return false;

            _movementController.CanRun = false;
            _direction = Vector3.zero;
            return true;
        }

        private bool CheckMouseOverJoystick(Vector2 mouseInput, Vector2 normalizedDirection)
        {
            if (CircleRadiusWithZeroCenter(mouseInput.x, mouseInput.y) <= _joystickRadius) return false;

            _movementController.CanRun = true;
            _joystick.rectTransform.anchoredPosition = normalizedDirection * _joystickRadius;
            _direction = new Vector3(normalizedDirection.x / 2f, 0, normalizedDirection.y / 2f);
            return true;
        }

        private float CircleRadiusWithZeroCenter(float x, float y)
        {
            return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
        }
    }
}