using Core;
using Core.Input;
using Core.Obstacles;
using Core.UI;
using Core.Upgrades;
using Core.Vibrations;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DI
{
    public sealed class GameCompositionRoot : DependencyContainerBase
    {
        [SerializeField] [AssetSelector] private VibrationsConfig _vibrationsVibrationsConfig;
        [SerializeField] [AssetSelector] private UpgradesConfig _upgradesUpgradesConfig;
        [SerializeField] [AssetSelector] private GameParameters _gameParameters;
        [SerializeField] [AssetSelector] private HeroScaleConfig _heroScaleConfig;
        [SerializeField] [Required] private AttackTextPool _attackTextPool;
        [SerializeField] [Required] private InputJoystickReceiver _inputJoystickReceiver;

        protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
        {
            builder.Register<VibrationsPlayer>()
                .Register<StatLevelSaver>()
                .RegisterIfNotNull(_vibrationsVibrationsConfig)
                .RegisterIfNotNull(_inputJoystickReceiver)
                .RegisterIfNotNull(_gameParameters)
                .RegisterIfNotNull(_heroScaleConfig)
                .RegisterIfNotNull(_attackTextPool)
                .RegisterIfNotNull(_upgradesUpgradesConfig);
        }
    }
}