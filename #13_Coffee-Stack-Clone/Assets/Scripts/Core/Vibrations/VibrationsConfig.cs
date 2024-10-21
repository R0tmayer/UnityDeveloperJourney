using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Core.Vibrations
{
    [CreateAssetMenu(fileName = "Vibrations Config", menuName = "Configs/Vibrations", order = 1)]
    public class VibrationsConfig : ScriptableObject
    {
        [SerializeField] private HapticTypes _upgradeHaptic;
        [SerializeField] private HapticTypes _auctionFinishingHaptic;
        [SerializeField] private HapticTypes _tapSpeedUpHaptic;

        public HapticTypes UpgradeVibration => _upgradeHaptic;
        public HapticTypes AuctionFinishingHaptic => _auctionFinishingHaptic;
        public HapticTypes TapSpeedUpHaptic => _tapSpeedUpHaptic;
    }
}