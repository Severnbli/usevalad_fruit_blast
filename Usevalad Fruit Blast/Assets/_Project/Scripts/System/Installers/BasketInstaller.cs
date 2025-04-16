using _Project.Scripts.Features.Basket.BasketSpawning;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.System.Installers
{
    public class BasketInstaller: MonoBehaviour, ISystemInstaller
    {
        [SerializeField] private Vector2Int size;
        [SerializeField] private Margin margin;
        
        public void Install()
        {
            IBasketSpawner basketSpawner = new BasketSpawner();
            basketSpawner.SpawnBasket(size, margin);
        }
    }
}