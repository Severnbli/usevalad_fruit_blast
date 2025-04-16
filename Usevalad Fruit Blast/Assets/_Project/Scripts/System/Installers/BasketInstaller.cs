using _Project.Configs.Basket;
using _Project.Scripts.Features.Basket.BasketSpawning;
using UnityEngine;

namespace _Project.Scripts.System.Installers
{
    public class BasketInstaller: MonoBehaviour, ISystemInstaller
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private BasketConfig _config;
        
        public void Install()
        {
            IBasketSpawner basketSpawner = new BasketSpawner();
            basketSpawner.SpawnBasket(_prefab, _config.Size, _config.Margin);
        }
    }
}