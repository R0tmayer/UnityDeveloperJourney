using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.AI
{
    [RequireComponent(typeof(Rigidbody))]
    public class BotsMovement : MonoBehaviour
    {
        
        [SerializeField] private float _speed;
        [SerializeField] private float _period;
        private Rigidbody _rigidbody;

        private Vector3 _direction;
        private float _timer;

        private bool _started;
        private int _index;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            ChangeDirection();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _period)
            {
                _timer = 0;
                ChangeDirection();
            }
            
            _rigidbody.velocity = _direction * _speed;
        }

        private void ChangeDirection()
        {
            if (_started == false)
            {
                var x = Random.Range(-10, 10);
                var z = Random.Range(-10, 10);

                _direction = new Vector3(x, 0, z).normalized;
                return;
            }


            var random = Random.Range(0, 4);

            switch (random)
            {
                case 0:
                    if(_index == 0)
                        ChangeDirection();
                    _direction.x = Random.Range(0, 10);
                    _direction.z = Random.Range(0, 10);
                    _index = 0;
                    break;
                case 1:
                    if(_index == 1)
                        ChangeDirection();
                    _direction.x = Random.Range(-10, 0);
                    _direction.z = Random.Range(0, 10);
                    _index = 1;
                    break;
                case 2:
                    if(_index == 2)
                        ChangeDirection();
                    _direction.x = Random.Range(-10, 0);
                    _direction.z = Random.Range(-10, 0);
                    _index = 2;
                    break;
                case 3:
                    if(_index == 3)
                        ChangeDirection();
                    _direction.x = Random.Range(0, 10);
                    _direction.z = Random.Range(-10, 0);
                    _index = 3;
                    break;
            }
            
            print(_direction);
            
            _direction.Normalize();
        }
    }
}