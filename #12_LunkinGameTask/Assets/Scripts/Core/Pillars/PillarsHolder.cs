using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Pillars
{
    public class PillarsHolder : MonoBehaviour
    {
        [SerializeField] [Required] private Pillar[] _pillars;

        public Pillar[] Pillars => _pillars;
    }
}