using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Configs.Basket
{
    [CreateAssetMenu(fileName = "BasketConfig", menuName = "Configs/Basket Config")]
    public class BasketConfig: ScriptableObject
    {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private Margin _margin;
        
        public Vector2Int Size => _size;
        public Margin Margin => _margin;
    }
}