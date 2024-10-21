using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Materials
{
    public class PillarsMaterialsHolder : MonoBehaviour
    {
        [SerializeField] [Required] private Material[] materials;

        public Material[] Materials => materials;
    }
}