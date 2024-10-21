using System.Collections;
using UnityEngine;

namespace Core.Snake
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private float _space = 2f;
        [SerializeField] private float _xLerp = 2f;
        [SerializeField] private float _zLerp = 2f;

        private IEnumerator _movementCoroutine;

        public void StartFollowingObject(SnakePart target)
        {
            if (_movementCoroutine != null) return;

            _movementCoroutine = MovementCoroutine(target);
            StartCoroutine(_movementCoroutine);
        }

        private IEnumerator MovementCoroutine(SnakePart target)
        {
            while (true)
            {
                var x = Mathf.Lerp(transform.position.x, target.transform.position.x, Time.smoothDeltaTime * _xLerp);
                var z = Mathf.Lerp(transform.position.z, target.transform.position.z + _space,
                    Time.smoothDeltaTime * _zLerp);
                transform.position = new Vector3(x, transform.position.y, z);
                yield return null;
            }
        }
    }
}