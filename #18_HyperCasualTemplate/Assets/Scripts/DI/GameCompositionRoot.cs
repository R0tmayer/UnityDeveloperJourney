using Core;
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

        protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
        {
            builder.Register<VibrationsPlayer>()
                .Register<StatLevelSaver>()
                .RegisterIfNotNull(_vibrationsVibrationsConfig)
                .RegisterIfNotNull(_upgradesUpgradesConfig)
                .RegisterIfNotNull(_gameParameters);
        }
    }
}