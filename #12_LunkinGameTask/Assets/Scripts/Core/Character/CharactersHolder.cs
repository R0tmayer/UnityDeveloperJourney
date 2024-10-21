using Core.Pillars;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Character
{
    public class CharactersHolder : MonoBehaviour
    {
        [SerializeField] [Required] private CharacterComponents[] _characters;

        public CharacterComponents[] Characters => _characters;
    }
}