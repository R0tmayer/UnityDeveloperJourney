using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts
{
    public class StickmanSpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private RagdollPusher _stickman;

        private List<RagdollPusher> _stickmans = new List<RagdollPusher>();
        
        private float _timer = 4;
        
        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= 4)
            {
                _timer = 0;
                
                for (var i = 0; i < _stickmans.Count; i++)
                {
                    Destroy(_stickmans[i].gameObject);
                }
                
                _stickmans.Clear();
                
                for (var i = 0; i < _points.Length; i++)
                {
                    _stickmans.Add(Instantiate(_stickman, _points[i]));
                }
            }
        }
    }
}