using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Pillars
{
    public class Pillar : MonoBehaviour
    {
        public Material DefaultMaterial { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }
        public Material SharedMaterial => MeshRenderer.sharedMaterial;
        public bool IsDefault => SharedMaterial == DefaultMaterial;

        private void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
            DefaultMaterial = MeshRenderer.sharedMaterial;
        }
    }
}
