using UnityEngine;

namespace _Project.Scripts.Features.Spawners.BallSpawner.Config
{
    [CreateAssetMenu(fileName = "BallSpawnerConfig", menuName = "Configs/Spawners/Ball Spawner Config")]
    public class BallSpawnerConfig: ScriptableObject
    {
        [SerializeField] private float[] _ballScales;
        [SerializeField] private Color[] _colors;
        
        public float[] BallScales => _ballScales;
        public Color[] Colors => _colors;
    }
}