using Core.Materials;
using Core.Character;
using Core.Pillars;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DI
{
    public sealed class GameCompositionRoot : DependencyContainerBase
    {
        [SerializeField] [Required] private PillarsMaterialsHolder pillarsMaterialsHolder;
        [SerializeField] [Required] private PillarsHolder _pillarsHolder;
        [SerializeField] [Required] private CharactersHolder _charactersHolder;
        [SerializeField] [Required] private MaterialsChanger materialsChanger;

        protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
        {
            builder.RegisterIfNotNull(pillarsMaterialsHolder)
                .RegisterIfNotNull(_pillarsHolder)
                .RegisterIfNotNull(_charactersHolder)
                .RegisterIfNotNull(materialsChanger);
        }
    }
}