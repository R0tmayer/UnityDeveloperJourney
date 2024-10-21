using UnityEngine;

namespace Core.Extensions
{
    public static class MyExtensions
    {
        public static void SetMaterial(MeshRenderer meshRenderer, Material newMaterial)
        {
            meshRenderer.material = newMaterial;
        }
        
        
    }
}