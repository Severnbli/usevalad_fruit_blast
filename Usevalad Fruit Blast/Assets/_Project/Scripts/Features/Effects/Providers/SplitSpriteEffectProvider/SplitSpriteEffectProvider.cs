using System.Collections.Generic;
using _Project.Scripts.Features.Effects.Objects;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.Physics.Colliders;
using _Project.Scripts.Features.Physics.Dynamic;
using _Project.Scripts.Features.Random;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Providers.SplitSpriteEffectProvider
{
    public class SplitSpriteEffectProvider : EffectProvider, IConfigurableFeature<SplitSpriteEffectProviderConfig>
    {
        private RandomProvider _randomProvider;
        private readonly Dictionary<Sprite, (Sprite, Sprite)> _splitSprites = new();
        
        public SplitSpriteEffectProviderConfig SplitSpriteEffectProviderConfig { get; private set; }

        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _randomProvider);
        }

        public void Configure(SplitSpriteEffectProviderConfig splitSpriteEffectProviderConfig)
        {
            SplitSpriteEffectProviderConfig = splitSpriteEffectProviderConfig;
        }
        
        public override void Emit(EffectEmitterObject emitterObject)
        {
            if (!emitterObject.gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                return;
            }

            // if (!_splitSprites.TryGetValue(spriteRenderer.sprite, out var splitSprite))
            // {
            //     splitSprite = GetSplitSprites(spriteRenderer.sprite);
            //     _splitSprites.TryAdd(spriteRenderer.sprite, splitSprite);
            // }
            //
            // SpawnEffectObjects(emitterObject, splitSprite);
        }

        public static (Sprite, Sprite) GetSplitSprites(Sprite sprite)
        {
            var texture = sprite.texture;
            var rect = sprite.textureRect;
            var pixelsPerUnit = sprite.pixelsPerUnit;
            
            var halfWidth = rect.width / 2f;
            
            var leftRect = new Rect(rect.x, rect.y, halfWidth, rect.height);
            var leftPivot = new Vector2(
                Mathf.Clamp01(sprite.pivot.x / rect.width),
                Mathf.Clamp01(sprite.pivot.y / rect.height)
            );
            leftPivot = new Vector2(leftPivot.x * 2f, leftPivot.y);

            var leftSprite = Sprite.Create(texture, leftRect, leftPivot, pixelsPerUnit);

            var rightRect = new Rect(rect.x + halfWidth, rect.y, halfWidth, rect.height);
            var rightPivot = new Vector2(
                Mathf.Clamp01((sprite.pivot.x - halfWidth) / halfWidth),
                Mathf.Clamp01(sprite.pivot.y / rect.height)
            );

            var rightSprite = Sprite.Create(texture, rightRect, rightPivot, pixelsPerUnit);

            return (leftSprite, rightSprite);
        }

        private void SpawnEffectObjects(EffectEmitterObject source, (Sprite, Sprite) sprites)
        {
            var effectName = $"{source.gameObject.name} - SplitSpriteEffect";
            
            var leftObject = new GameObject(effectName);
            var rightObject = new GameObject(effectName);
            
            
            _effectObjectsContainer.AddToContainer(leftObject);
            _effectObjectsContainer.AddToContainer(rightObject);
            
            leftObject.transform.position = rightObject.transform.position = source.transform.position;
            leftObject.transform.localScale = rightObject.transform.localScale = source.transform.localScale;
            
            var leftSpriteRenderer = leftObject.AddComponent<SpriteRenderer>();
            var rightSpriteRenderer = rightObject.AddComponent<SpriteRenderer>();
            
            leftSpriteRenderer.sortingLayerName = rightSpriteRenderer.sortingLayerName = 
                SplitSpriteEffectProviderConfig.EffectSortingLayerName;
            leftSpriteRenderer.sprite = sprites.Item1;
            rightSpriteRenderer.sprite = sprites.Item2;
            
            var leftCollider = leftObject.AddComponent<CircleCollider>();
            var rightCollider = rightObject.AddComponent<CircleCollider>();
            
            leftCollider.IsTrigger = rightCollider.IsTrigger = true;
            
            var leftDynamicBody = leftObject.AddComponent<DynamicBody>();
            var rightDynamicBody = rightObject.AddComponent<DynamicBody>();
            
            var minStartVelocity = SplitSpriteEffectProviderConfig.MinStartVelocity;
            var maxStartVelocity = SplitSpriteEffectProviderConfig.MaxStartVelocity;
            var startVelocity = new Vector2(
                (float) _randomProvider.Random.NextDouble() * (maxStartVelocity.x - minStartVelocity.x) + minStartVelocity.x,
                (float) _randomProvider.Random.NextDouble() * (maxStartVelocity.y - minStartVelocity.y) + minStartVelocity.y
                );
            
            leftDynamicBody.Velocity = new Vector2(-startVelocity.x, startVelocity.y);
            rightDynamicBody.Velocity = startVelocity;
        }
    }
}