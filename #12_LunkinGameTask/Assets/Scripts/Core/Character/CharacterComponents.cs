using Core.Materials;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Character
{
    public class CharacterComponents : MonoBehaviour
    {
        [SerializeField] [Required] private Movement _movement;
        [SerializeField] [Required] private CharacterMaterials _characterMaterials;

        public Movement Movement => _movement;
        public CharacterMaterials CharacterMaterials => _characterMaterials;
    }
}