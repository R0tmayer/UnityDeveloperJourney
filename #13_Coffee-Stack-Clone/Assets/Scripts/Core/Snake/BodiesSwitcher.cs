using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Snake
{
    public class BodiesSwitcher : MonoBehaviour
    {
        [SerializeField] [Required] private GameObject _firstLevelBody; 
        [SerializeField] [Required] private GameObject _secondLevelBody; 
        
        public void SwitchNextBody()
        {
            _firstLevelBody.SetActive(false);
            _secondLevelBody.SetActive(true);
        }
    }
}