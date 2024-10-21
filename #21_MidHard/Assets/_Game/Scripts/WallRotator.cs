using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class WallRotator : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _model;
        [SerializeField] private bool _aroundY;

        private void Update()
        {
            _model.Rotate(_aroundY
                ? new Vector3(0, _speed * Time.deltaTime, 0)
                : new Vector3(0, 0, _speed * Time.deltaTime));
        }
    }
}