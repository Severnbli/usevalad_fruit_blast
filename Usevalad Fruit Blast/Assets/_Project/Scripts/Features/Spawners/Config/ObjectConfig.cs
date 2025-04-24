using UnityEngine;

namespace _Project.Scripts.Features.Spawners.Config
{
    [CreateAssetMenu(fileName = "SpawnObjectConfig", menuName = "Configs/Spawners/Common/Spawn Object Config")]
    public class ObjectConfig : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        
        public GameObject Prefab => _prefab;
    }
}