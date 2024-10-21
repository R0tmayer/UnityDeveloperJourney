using Assets.Scripts.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] [Required] private CharacterController _characterController;
        [SerializeField] [Required] [AssetSelector] private CharacterSettings _settings;

        public bool CanRun { get; set; }

        public void MoveInDirection(Vector3 direction)
        {
            _characterController.Move(direction * _settings.SpeedValue * Time.deltaTime);
            _characterController.transform.LookAt(transform.position + direction);
        }
    }
}
