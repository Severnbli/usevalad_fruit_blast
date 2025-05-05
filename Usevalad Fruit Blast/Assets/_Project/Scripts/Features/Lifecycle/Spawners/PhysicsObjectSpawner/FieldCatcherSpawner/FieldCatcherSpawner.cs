using _Project.Scripts.Features.Field.FieldCatcher;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner
{
    public class FieldCatcherSpawner : PhysicsObjectSpawner
    {
        [SerializeField] private FieldCatcher _fieldCatcher;
        
        public FieldCatcher FieldCatcher => _fieldCatcher;

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _fieldCatcher);
        }
        
        public override void Spawn()
        {
            if (!base.TryGetConfiguredObject(out var resultObject))
            {
                return;
            }
            
            resultObject.transform.position = GetNextSpawnPosition(resultObject);
        }

        public Vector2 GetNextSpawnPosition(GameObject prefab)
        {
            var position = _fieldCatcher.GetPosition();

            var widthWithSpriteSize = _fieldCatcher.GetCatcherSize().x;

            if (prefab.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                widthWithSpriteSize -= spriteRenderer.bounds.size.x;
            }

            position.y += _fieldCatcher.GetFieldProvider().GetFieldSize().y / 1.5f;
            position.x += widthWithSpriteSize * ((float) _randomProvider.Random.NextDouble() - 1/2f);
            
            return position;
        }
    }
}