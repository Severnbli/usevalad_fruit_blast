using _Project.Scripts.Features.Field.FieldCatcher;
using UnityEngine;

namespace _Project.Scripts.Features.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner
{
    public class FieldCatcherSpawner : PhysicsObjectSpawner
    {
        [SerializeField] private FieldCatcher _fieldCatcher;
        
        public FieldCatcher FieldCatcher => _fieldCatcher;
        
        public override void Spawn()
        {
            if (!base.TryGetConfiguredObject(out var resultObject))
            {
                return;
            }
            
            resultObject.transform.position = GetNextSpawnPosition(resultObject);
        }

        public Vector2 GetNextSpawnPosition(GameObject ball)
        {
            var position = _fieldCatcher.GetPosition();

            var catcherSize = _fieldCatcher.GetCatcherSize();
            var widthWithSpriteSize = catcherSize.x;

            if (ball.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                widthWithSpriteSize -= spriteRenderer.bounds.size.x;
            }

            position.y += catcherSize.y;
            position.x += widthWithSpriteSize * ((float) _randomProvider.Random.NextDouble() - 1/2f);
            
            return position;
        }
    }
}