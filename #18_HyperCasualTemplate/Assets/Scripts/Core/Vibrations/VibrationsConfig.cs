using MoreMountains.NiceVibrations;
using UnityEngine;

namespace Core.Vibrations
{
    [CreateAssetMenu(fileName = "Vibrations UpgradesConfig", menuName = "ScriptableObjects/Vibrations", order = 1)]
    public class VibrationsConfig : ScriptableObject
    {
        [SerializeField] private HapticTypes _upgradeVibration;
        [SerializeField] private HapticTypes _auctionFinishingVibration;
        [SerializeField] private HapticTypes _tapSpeedUpVibration;

        public HapticTypes UpgradeVibration => _upgradeVibration;
        public HapticTypes AuctionFinishingVibration => _auctionFinishingVibration;
        public HapticTypes TapSpeedUpVibration => _tapSpeedUpVibration;
    }
}