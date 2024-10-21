using System;
using Core.Snake;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Animation
{
    public class SnakeScaleAnimation : MonoBehaviour
    {
        [SerializeField] [Required] private SnakeList _snakeList;
        [SerializeField] private float _interval;
        [SerializeField] private float _duration;
        [SerializeField] private float _scale;
        [SerializeField] private Ease _ease;
        [SerializeField] private bool _changeColors;

        public void Play()
        {
            var sequence = DOTween.Sequence().SetId(this);
            float interval = 0;

            for (var i = _snakeList.Parts.Count - 1; i >= 0; i--)
            {
                var part = _snakeList.Parts[i];
                
                sequence
                    .Join(DOTween.Sequence()
                        .AppendInterval(interval)
                        .AppendCallback(() =>
                        {
                            if (_changeColors)
                                part.GetComponentInChildren<MeshRenderer>().material.color =
                                    Random.ColorHSV(0, 0.7f, 0.8f, 1, 0.8f, 1);
                        })
                        .Append(part.ScaleAndRollback(_scale, _duration, _ease)));

                interval += _interval;
            }
        }
    }
}