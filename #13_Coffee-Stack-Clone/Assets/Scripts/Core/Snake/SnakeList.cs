using System.Collections.Generic;
using Core.Pickups;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Snake
{
    public class SnakeList : MonoBehaviour
    {
        [SerializeField] [Required] private SnakePart _mainSnakePart;

        public List<SnakePart> Parts { get; } = new List<SnakePart>();

        public SnakePart LastPart => Parts[Parts.Count - 1];

        private void Awake()
        {
            Parts.Add(_mainSnakePart);
        }

        public void AddNewPart(Pickup pickup)
        {
            if (pickup.TryGetComponent(out SnakePart part))
                Parts.Add(part);
        }
    }
}