using Core.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Hero
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] [Required] private AttackRateDisplay _attackRateDisplay;
        public int Rate { get; private set; } = 2;

        public void IncreaseRate(float value)
        {
            Rate += (int) value;
            _attackRateDisplay.UpdateValue(Rate);
        }
    }
}