using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner
{
    [CreateAssetMenu(fileName = "FieldCatcherSpawnerConfig", menuName = "Configs/Lifecycle/Spawners/Physics Object Spawner/Field Catcher Spawner Config")]
    public class FieldCatcherSpawnerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] private PhysicsObjectSpawnerConfig _physicsObjectSpawnerConfig;
        [SerializeField] private float _timeToFillTheCatcher = 0.2f;
        [SerializeField] private float _maxCorruptedFieldCatcherArea = 0.85f;
        [SerializeField] private float _continuousFillDelay = 0.2f;
        
        public PhysicsObjectSpawnerConfig PhysicsObjectSpawnerConfig => _physicsObjectSpawnerConfig;
        public float TimeToFillTheCatcher => _timeToFillTheCatcher;
        public float MaxCorruptedFieldCatcherArea => _maxCorruptedFieldCatcherArea;
        public float ContinuousFillDelay => _continuousFillDelay;
    }
}