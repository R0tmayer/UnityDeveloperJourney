using System;
using UnityEngine;

namespace Core.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _camera;

        private void Awake() => _camera = Camera.main;

        private void LateUpdate() => transform.LookAt(transform.position + _camera.transform.forward);
    }
}