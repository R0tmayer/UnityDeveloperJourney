using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Obstacles
{
    public class ObstacleScaler : MonoBehaviour
    {
        private GameParameters _gameParameters;

        public void Construct(GameParameters gameParameters) => _gameParameters = gameParameters;

        public void ScaleToZeroAfterDelay()
        {
            var children = GetComponentsInChildren<Transform>();

            DOTween.Sequence().AppendInterval(_gameParameters.ObstacleScaleDelay)
                .AppendCallback(() =>
                {
                    for (int i = 1; i < children.Length; i++)
                    {
                        children[i].DOScale(0, _gameParameters.ObstacleScaleDuration)
                            .OnComplete(() => gameObject.SetActive(false));
                    }
                });
        }
    }
}