using System;
using System.Collections;
using Core.Materials;
using Core.Pillars;
using DG.Tweening;
using UnityEngine;

namespace Core.Character
{
    public class TaskFinder : MonoBehaviour
    {
        [SerializeField] private float _interval;
        private PillarsHolder _pillarsHolder;
        private CharactersHolder _charactersHolder;
        
        private WaitForSeconds _waitForInterval;
        private IEnumerator _taskCoroutine;
        private MaterialsChanger _materialsChanger;

        private void Awake()
        {
            _waitForInterval = new WaitForSeconds(_interval);
        }

        private void Start()
        {
            StartFindTaskCoroutine();
        }

        public void Construct(PillarsHolder pillarsHolder, CharactersHolder charactersHolder, MaterialsChanger materialsChanger)
        {
            _materialsChanger = materialsChanger;
            _charactersHolder = charactersHolder;
            _pillarsHolder = pillarsHolder;
        }

        private void StartFindTaskCoroutine()
        {
            if (_taskCoroutine == null)
            {
                _taskCoroutine = FindTaskCoroutine();
                StartCoroutine(_taskCoroutine);
            }
        }

        private void StopFindTaskCoroutine()
        {
            if(_taskCoroutine == null)
                return;
            
            StopCoroutine(_taskCoroutine);
            _taskCoroutine = null;
        }

        private IEnumerator FindTaskCoroutine()
        {
            while (true)
            {
                FindTask();
                yield return _waitForInterval;
            }
        }

        private void FindTask()
        {
            foreach (var pillar in _pillarsHolder.Pillars)
            {
                if (pillar.IsDefault == false)
                {
                    TryGiveTask(pillar);
                }
            }
        }

        private void TryGiveTask(Pillar pillar)
        {
            foreach (var characterData in _charactersHolder.Characters)
            {
                if (pillar.SharedMaterial == characterData.CharacterMaterials.SharedMaterialOne ||
                    pillar.SharedMaterial == characterData.CharacterMaterials.SharedMaterialTwo)
                {
                    MoveToPillarTask(pillar, characterData);
                    return;
                }
            }
        }

        private void MoveToPillarTask(Pillar pillar, CharacterComponents characterData)
        {
            StopFindTaskCoroutine();
            
            characterData.Movement.GetMoveToPillarTween(pillar).OnComplete(() =>
            {
                _materialsChanger.ResetMaterial(pillar);
                characterData.Movement.GetMoveToBaseTween().OnComplete(OnBaseReached);
            });
        }

        private void OnBaseReached()
        {
            StartFindTaskCoroutine();
        }
    }
}