using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldCatcher;
using _Project.Scripts.Features.Random;
using _Project.Scripts.Features.Spawners.BallSpawner.Config;
using _Project.Scripts.System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Features.Spawners.BallSpawner
{
    public class BallSpawner : ObjectSpawner
    {
        [SerializeField] private FieldCatcher _fieldCatcher;
        [SerializeField] private BallSpawnerConfig _config;
        
        private RandomProvider _randomProvider;
        
        public FieldCatcher FieldCatcher => _fieldCatcher;
        public BallSpawnerConfig Config => _config;
        
        public override void Spawn()
        {
            if (!TryGetNextBall(out var ball))
            {
                return;
            }

            ball.transform.parent = transform;
            ball.transform.position = GetNextBallPosition(ball);
        }

        public bool TryGetNextBall(out GameObject ball)
        {
            if (_config.Colors.Length == 0 || _config.Colors.Length == 0)
            {
                Debug.LogError("No configs have been specified to spawn ball.");
                ball = null;
                return false;
            }
                
            var scaleFactor = _config.BallScales[_randomProvider.Random.Next(_config.BallScales.Length)];
            var color = _config.Colors[_randomProvider.Random.Next(_config.Colors.Length)];
            
            ball = Instantiate(_prefab, Vector3.zero, Quaternion.identity);

            if (ball.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.color = color;
            }
            
            ball.transform.localScale *= scaleFactor;
            
            return true;
        }

        public Vector2 GetNextBallPosition(GameObject ball)
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
        
        public override void Init(IFeatureConfig config)
        {
            _randomProvider = Context.Container.GetComponent<RandomProvider>();

            if (_randomProvider == null)
            {
                Debug.LogError("Check system priority setup: random provider must be earlier than ball spawner!");
            }
        }
    }
}