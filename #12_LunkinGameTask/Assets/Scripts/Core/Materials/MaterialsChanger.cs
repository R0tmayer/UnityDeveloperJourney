using System;
using System.Collections;
using Core.Extensions;
using Core.Pillars;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Materials
{
    public class MaterialsChanger : MonoBehaviour
    {
        [SerializeField] private float _interval = 1;
        private PillarsMaterialsHolder _pillarsMaterialsHolder;
        private PillarsHolder _pillarsHolder;
        private WaitForSeconds _waitForInterval;

        public void Construct(PillarsMaterialsHolder pillarsMaterialsHolder, PillarsHolder pillarsHolder)
        {
            _pillarsHolder = pillarsHolder;
            _pillarsMaterialsHolder = pillarsMaterialsHolder;
        }

        private void Awake()
        {
            _waitForInterval = new WaitForSeconds(_interval);
        }

        private void Start()
        {
            StartCoroutine(ChangeMaterialLoop());
        }
        
        private void ChangeRandomPillarMaterial()
        {
            var randomPillarIndex = Random.Range(0, _pillarsHolder.Pillars.Length);
            var randomMaterialIndex = Random.Range(0, _pillarsMaterialsHolder.Materials.Length);

            var pickedPillar = _pillarsHolder.Pillars[randomPillarIndex];
            var pickedMaterial = _pillarsMaterialsHolder.Materials[randomMaterialIndex];

            MyExtensions.SetMaterial(pickedPillar.MeshRenderer, pickedMaterial);
        }
        
        private IEnumerator ChangeMaterialLoop()
        {
            while (true)
            {
                ChangeRandomPillarMaterial();
                yield return _waitForInterval;
            }
        }

        public void ResetMaterial(Pillar pillar)
        {
            pillar.MeshRenderer.material = pillar.DefaultMaterial;
        }
    }
}