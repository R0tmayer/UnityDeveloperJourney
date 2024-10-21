using System;
using NavySpade.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace NavySpade.Core.Scores
{
    public class BestScoreView : MonoBehaviour, IInitializable
    {
        [SerializeField] private TMP_Text _text;
        private BestScore _bestScore;

        public void Construct(BestScore bestScore)
        {
            _bestScore = bestScore;
        }

        public void Initialize()
        {
            _bestScore.Changed += UpdateText;
        }

        private void UpdateText(int value)
        {
            _text.SetText($"{value}");
        }

        private void OnDisable()
        {
            _bestScore.Changed -= UpdateText;
        }
    }
}