using System;
using Core.Hero;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Core.UI
{
    public class AttackRateDisplay : MonoBehaviour
    {
        [SerializeField] [Required] private TMP_Text _text;
        [SerializeField] [Required] private Transform _model;

        private void Start()
        {
            UpdateValue(1);
        }

        public void UpdateValue(int value)
        {
            _text.SetText("{0:0}", value);

            var offset = _model.localScale.y / 100f;

            transform.localPosition = new Vector3(transform.localPosition.x, _model.localScale.y / 10f - offset,
                transform.localPosition.z);
        }
    }
}