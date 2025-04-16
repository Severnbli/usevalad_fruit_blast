using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Configs.Basket
{
    [CreateAssetMenu(fileName = "BasketConfig", menuName = "Configs/Basket Config")]
    public class BasketConfig: ScriptableObject
    {
        public Vector2Int size;
        public Margin margin;
    }
}