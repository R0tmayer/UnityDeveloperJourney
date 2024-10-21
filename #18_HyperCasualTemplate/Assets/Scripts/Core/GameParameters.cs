using DG.Tweening;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Game Parameters", menuName = "ScriptableObjects/GameParameters", order = 1)]

    public class GameParameters : ScriptableObject
    {
        [SerializeField] private float _speed;

        [Header("TapToStart")] 
        
        [SerializeField] private Ease _tapToStartEase;
        [SerializeField] private float _tapToStartDuration;
        [SerializeField] private bool _firstTouchInput;
        

        public float Speed => _speed;

        public Ease TapToStartEase => _tapToStartEase;
        public float TapToStartDuration => _tapToStartDuration;
        public bool FirstTouchInput => _firstTouchInput;
    }
}