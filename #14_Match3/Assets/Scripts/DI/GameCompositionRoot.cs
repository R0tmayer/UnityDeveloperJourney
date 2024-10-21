using Core.BallsFolder;
using Core.Board;
using Core.TilesFolder;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using UnityEngine;

namespace DI
{
    public sealed class GameCompositionRoot : DependencyContainerBase
    {
        [SerializeField] private GeneratorConfig _generatorConfig;
        [SerializeField] private BoardGenerator _boardGenerator;
        [SerializeField] private BallsReplacer _ballsReplacer;

        protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
        {
            builder.RegisterIfNotNull(_generatorConfig)
                .RegisterIfNotNull(_boardGenerator)
                .RegisterIfNotNull(_ballsReplacer)
                .Register<MatchBallsSearcher>()
                .RegisterFromMethod(() => new BallsHolder(_generatorConfig.BoardSize))
                .RegisterFromMethod(() => new TilesHolder(_generatorConfig.BoardSize));

        }
    }
}