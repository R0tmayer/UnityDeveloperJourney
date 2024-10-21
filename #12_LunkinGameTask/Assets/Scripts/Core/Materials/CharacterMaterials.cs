using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Materials
{
    public class CharacterMaterials : MonoBehaviour
    {
        [SerializeField] [Required] private MeshRenderer _meshRendererOne;
        [SerializeField] [Required] private MeshRenderer _meshRendererTwo;

        public Material SharedMaterialOne => _meshRendererOne.sharedMaterial;
        public Material SharedMaterialTwo => _meshRendererTwo.sharedMaterial;
    }
}