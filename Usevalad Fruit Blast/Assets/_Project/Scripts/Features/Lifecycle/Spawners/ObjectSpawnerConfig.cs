using System.Collections.Generic;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners
{
    [CreateAssetMenu(fileName = "ObjectSpawnerConfig", menuName = "Configs/Spawners/Common/Base Spawner Config")]
    public class ObjectSpawnerConfig : ScriptableObject, IFeatureConfig
    {
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected Transform _objectContainerTransform;
        [SerializeField] protected List<SpawnGroupsConfig> _spawnGroups;
        [SerializeField] private float _minScale = 1f;
        [SerializeField] private float _maxScale = 1f;
        
        public GameObject Prefab => _prefab;
        public Transform ObjectContainerTransform => _objectContainerTransform;
        public List<SpawnGroupsConfig> SpawnGroups => _spawnGroups;
        public float MinScale => _minScale;
        public float MaxScale => _maxScale;
    }
}