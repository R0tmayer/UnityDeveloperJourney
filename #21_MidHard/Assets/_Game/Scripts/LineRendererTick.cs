using System;
using Dreamteck.Splines;
using UnityEngine;

namespace _Game.Scripts
{
    public class LineRendererTick : MonoBehaviour
    {
        [SerializeField] private SplineRenderer _splineRenderer;
        [SerializeField] private float _speed;
        private float _offset = 0;

        private void Update()
        {
            _offset += Time.deltaTime * _speed;
            _splineRenderer.uvOffset = new Vector2(0, _offset);
        }
    }
}
