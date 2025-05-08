using System.Collections.Generic;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners
{
    [CreateAssetMenu(fileName = "ObjectSpawnerConfig", menuName = "Configs/Lifecycle/Spawners/Common/Base Spawner Config")]
    public class ObjectSpawnerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected List<SpawnGroupsConfig> _spawnGroups;
        [SerializeField] private float _minScale = 1f;
        [SerializeField] private float _maxScale = 1f;
        
        public GameObject Prefab => _prefab;
        public List<SpawnGroupsConfig> SpawnGroups => _spawnGroups;
        public float MinScale => _minScale;
        public float MaxScale => _maxScale;
    }
}