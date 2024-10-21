using System;
using Core.Input;
using DG.Tweening;
using UnityEngine;

namespace Core.Hero
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour
    {
        private InputJoystickReceiver _input;
        private Animator _animator;
        private static readonly int blend = Animator.StringToHash("Blend");
        private static readonly int _attack = Animator.StringToHash("Attack");

        public void Construct(InputJoystickReceiver input) => _input = input;

        private void Awake() => _animator = GetComponent<Animator>();

        private void Update() => _animator.SetFloat(blend, _input.DistanceRation);

        public void SetAttackState() => _animator.SetBool(_attack, true);

        public void ResetAttackState() => _animator.SetBool(_attack, false);
    }
}