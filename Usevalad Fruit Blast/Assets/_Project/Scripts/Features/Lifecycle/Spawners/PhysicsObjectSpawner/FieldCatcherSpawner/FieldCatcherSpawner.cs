using System.Threading;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Lifecycle.Objects.ObjectsContainer;
using _Project.Scripts.Features.Physics.Colliders;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Spawners.PhysicsObjectSpawner.FieldCatcherSpawner
{
    public class FieldCatcherSpawner : PhysicsObjectSpawner, IConfigurableFeature<FieldCatcherSpawnerConfig>
    {
        private FieldCatcher _fieldCatcher;
        
        public FieldCatcherSpawnerConfig FieldCatcherSpawnerConfig { get; private set; }

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _fieldCatcher);
        }
        
        public void Configure(FieldCatcherSpawnerConfig fieldCatcherSpawnerConfig)
        {
            base.Configure(fieldCatcherSpawnerConfig.PhysicsObjectSpawnerConfig);
            
            FieldCatcherSpawnerConfig = fieldCatcherSpawnerConfig;
        }
        
        public override GameObject Spawn()
        {
            if (!base.TryGetConfiguredObject(out var resultObject))
            {
                return null;
            }
            
            resultObject.transform.position = GetNextSpawnPosition(resultObject);
            
            return resultObject;
        }

        public float GetCorruptedPercentageFieldCatcherArea()
        {
            return _objectsContainer.GetTotalArea() / _fieldCatcher.GetArea();
        }

        public float GetFieldCatcherMaxCorruptedArea()
        {
            return _fieldCatcher.GetArea() * FieldCatcherSpawnerConfig.MaxCorruptedFieldCatcherArea;
        }

        public async UniTask FillCatcher(CancellationToken cancellationToken)
        {
            var corruptedArea = _objectsContainer.GetTotalArea();
            var maxCorruptedArea = GetFieldCatcherMaxCorruptedArea();

            if (corruptedArea >= maxCorruptedArea)
            {
                return;
            }
            
            var spawnedObject = Spawn();
            var spawnedArea = 0f;
            
            if (spawnedObject.TryGetComponent(out BaseCollider spawnedCollider))
            {
                spawnedArea = spawnedCollider.GetArea();
            }

            var expectedSpawnObjectsCount = (maxCorruptedArea - corruptedArea - spawnedArea) / spawnedArea;
            
            var spawnTimeDelta = FieldCatcherSpawnerConfig.TimeToFillTheCatcher / expectedSpawnObjectsCount;
            spawnTimeDelta = Mathf.Abs(spawnTimeDelta);
            
            while (corruptedArea < maxCorruptedArea && !cancellationToken.IsCancellationRequested)
            {
                Spawn();
                corruptedArea = _objectsContainer.GetTotalArea();
                await UniTask.WaitForSeconds(spawnTimeDelta, cancellationToken: cancellationToken);
            }
        }

        public async UniTask ContinuousFillCatcher(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var fillCts = new CancellationTokenSource();
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, fillCts.Token);

                var fillTask = FillCatcher(linkedCts.Token);

                await UniTask.WaitForSeconds(FieldCatcherSpawnerConfig.ContinuousFillDelay, cancellationToken: cancellationToken);

                fillCts.Cancel();
                
                await fillTask.SuppressCancellationThrow();
            }
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