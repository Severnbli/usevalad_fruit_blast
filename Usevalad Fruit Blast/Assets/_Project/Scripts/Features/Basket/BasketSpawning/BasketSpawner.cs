using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Features.Basket.BasketSpawning
{
    public class BasketSpawner: IBasketSpawner
    {
        public void SpawnBasket(GameObject prefab, Vector2Int size, Margin margin)
        {
            var basket = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            basket.name = "Basket";
            
            var basketController = basket.AddComponent<BasketController>();
            basketController.SetAll(size, margin, Camera.main);
        }
    }
}