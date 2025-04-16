using _Project.Configs.Basket;
using _Project.Scripts.Features.Basket;
using _Project.Scripts.Features.Basket.BasketSpawning;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.System.Installers
{
    public class BasketInstaller: MonoBehaviour, ISystemInstaller
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private BasketConfig config;
        
        public void Install()
        {
            IBasketSpawner basketSpawner = new BasketSpawner();
            basketSpawner.SpawnBasket(prefab, config.size, config.margin);
        }
    }
}