using System;
using Assets.Scripts.VirtualJoystick;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] [Required] private MovementController _movementController;
        [SerializeField] [Required] private Animator _animator;

        private void Update() => _animator.SetBool(IsRunning, _movementController.CanRun);

        private readonly int IsRunning = Animator.StringToHash("IsRunning");
    }
}