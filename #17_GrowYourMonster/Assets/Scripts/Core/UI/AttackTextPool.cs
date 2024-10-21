using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class AttackTextPool : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _textPool;

        public void Animate(float score)
        {
            var text = _textPool.FirstOrDefault(x => x != x.gameObject.activeInHierarchy);
            text.SetText("{0:0}", score);
            text.gameObject.SetActive(true);

            text.transform.DOLocalMoveY(5, 1)
                .SetRelative(true)
                .OnComplete(() =>
                {
                    text.transform.localPosition = Vector3.zero;
                    text.gameObject.SetActive(false);
                });

        }
    }
}