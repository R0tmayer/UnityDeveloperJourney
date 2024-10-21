using Core.Snake;
using UnityEngine;

namespace Core.Pickups
{
    public class Pickup : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        public Follower Follower { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Follower = GetComponent<Follower>();
        }

        public void AddSnakesComponents()
        {
            gameObject.AddComponent<SnakePart>();
        }

        public void RemoveRigidbody()
        {
            Destroy(_rigidbody);
        }

        public void RemoveThisComponent()
        {
            Destroy(this);
        }
    }
}