using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.Config
{
    [CreateAssetMenu(fileName = "BaseSpawnerConfig", menuName = "Configs/Spawners/Common/Base Spawner Config")]
    public class BaseSpawnerConfig : ScriptableObject
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