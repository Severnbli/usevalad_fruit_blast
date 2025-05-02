using _Project.Scripts.Features.Destroyers.ClickObjectDestroyer.Config;
using _Project.Scripts.Features.Destroyers.Services.InfectiousProvider.Config;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Field.FieldProvider;
using _Project.Scripts.Features.Gizmo.GizmoProvider.BaseGizmoProvider;
using _Project.Scripts.Features.Physics.Engine.Config;
using _Project.Scripts.Features.Physics.Forces.GravityForce;
using _Project.Scripts.Features.Random.Config;
using _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner;
using UnityEngine;

namespace _Project.Scripts.Common.Configs
{
    public class CommonConfig : MonoBehaviour
    {
        [SerializeField] private PhysicsEngineConfig _physicsEngineConfig;
        [SerializeField] private GravityForceProviderConfig _gravityForceProviderConfig;
        [SerializeField] private BaseGizmoProviderConfig _baseGizmoProviderConfig;
        [SerializeField] private RandomProviderConfig _randomProviderConfig;
        [SerializeField] private InfectiousProviderConfig _infectiousProviderConfig;
        [SerializeField] private ClickObjectDestroyerConfig _clickObjectDestroyerConfig;
        
        [SerializeField] private FieldCatcher _fieldCatcher;
        [SerializeField] private FieldProvider _fieldProvider;
        [SerializeField] private FieldCatcherSpawner _fieldCatcherSpawner;
        
        public PhysicsEngineConfig PhysicsEngineConfig => _physicsEngineConfig;
        public BaseGizmoProviderConfig BaseGizmoProviderConfig => _baseGizmoProviderConfig;
        public GravityForceProviderConfig GravityForceProviderConfig => _gravityForceProviderConfig;
        public RandomProviderConfig RandomProviderConfig => _randomProviderConfig;
        public InfectiousProviderConfig InfectiousProviderConfig => _infectiousProviderConfig;
        public ClickObjectDestroyerConfig ClickObjectDestroyerConfig => _clickObjectDestroyerConfig;
        
        public FieldCatcher FieldCatcher => _fieldCatcher;
        public FieldProvider FieldProvider => _fieldProvider;
        public FieldCatcherSpawner FieldCatcherSpawner => _fieldCatcherSpawner;
    }
}