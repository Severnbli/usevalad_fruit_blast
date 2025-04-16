using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Features.Basket.BasketSpawning
{
    public interface IBasketSpawner
    {
        public void SpawnBasket(Vector2Int size, Margin margin);
    }
}