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
        private readonly Dictionary<Sprite, List<(Sprite, Sprite)>> _splitSprites = new();
        
        public SplitSpriteEffectProviderConfig SplitSpriteEffectProviderConfig { get; private set; }

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

            if (!_splitSprites.TryGetValue(spriteRenderer.sprite, out var splitSpritesList))
            {
                splitSpritesList = GetSplitSpritesList(spriteRenderer.sprite);
                _splitSprites.TryAdd(spriteRenderer.sprite, splitSpritesList);
            }
            
            SpawnEffectObjects(emitterObject, splitSpritesList[_randomProvider.Random.Next(splitSpritesList.Count)]);
        }

        public List<(Sprite, Sprite)> GetSplitSpritesList(Sprite sprite)
        {
            var config = SplitSpriteEffectProviderConfig;
            var uniqueSplitsQuantity = config.UniqueSplitsQuantity;
            var splitSpritesList = new List<(Sprite, Sprite)>(uniqueSplitsQuantity);
            
            var texture = sprite.texture;
            var rect = sprite.textureRect;
            var pixelsPerUnit = sprite.pixelsPerUnit;
            var pivot = new Vector2(0.5f, 0.5f);
            
            for (var i = 0; i < uniqueSplitsQuantity; i++)
            {
                Rect leftRect;
                Rect rightRect;

                var isVerticalSplit = _randomProvider.Random.Next(2) != 0;

                if (isVerticalSplit)
                {
                    var minHeightSplit = rect.height * config.SpritesSafeAreaPercentage;
                    var maxHeightSplit = rect.height - minHeightSplit;
                    
                    var randHeightSplit = (float) _randomProvider.Random.NextDouble() 
                        * (maxHeightSplit - minHeightSplit) + minHeightSplit;
                    
                    leftRect = new Rect(rect.x, rect.y + randHeightSplit, rect.width, rect.height - randHeightSplit);
                    rightRect = new Rect(rect.x, rect.y, rect.width, randHeightSplit);
                }
                else
                {
                    var minWidthSplit = rect.width * config.SpritesSafeAreaPercentage;
                    var maxWidthSplit = rect.width - minWidthSplit;
                    
                    var randWidthSplit = (float) _randomProvider.Random.NextDouble() 
                        * (maxWidthSplit - minWidthSplit) + minWidthSplit;
                    
                    leftRect = new Rect(rect.x, rect.y, randWidthSplit, rect.height);
                    rightRect = new Rect(rect.x + randWidthSplit, rect.y, rect.width - randWidthSplit, rect.height);
                }
                
                var leftSprite = Sprite.Create(texture, leftRect, pivot, pixelsPerUnit);
                var rightSprite = Sprite.Create(texture, rightRect, pivot, pixelsPerUnit);
                
                splitSpritesList.Add((leftSprite, rightSprite));
            }

            return splitSpritesList;
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

            var leftSpriteHolder = new GameObject("LeftSprite");
            var rightSpriteHolder = new GameObject("RightSprite");

            leftSpriteHolder.transform.SetParent(leftObject.transform, false);
            rightSpriteHolder.transform.SetParent(rightObject.transform, false);

            leftSpriteHolder.transform.localPosition = Vector3.zero;
            rightSpriteHolder.transform.localPosition = Vector3.zero;

            var leftSpriteRenderer = leftSpriteHolder.AddComponent<SpriteRenderer>();
            var rightSpriteRenderer = rightSpriteHolder.AddComponent<SpriteRenderer>();

            leftSpriteRenderer.sortingLayerName = rightSpriteRenderer.sortingLayerName =
                SplitSpriteEffectProviderConfig.EffectSortingLayerName;
            leftSpriteRenderer.sortingOrder = rightSpriteRenderer.sortingOrder =
                SplitSpriteEffectProviderConfig.EffectSortingLayerOrder;

            leftSpriteRenderer.sprite = sprites.Item1;
            rightSpriteRenderer.sprite = sprites.Item2;

            float leftRotation = (float)_randomProvider.Random.NextDouble() * 360f;
            float rightRotation = (float)_randomProvider.Random.NextDouble() * 360f;

            leftSpriteHolder.transform.localRotation = Quaternion.Euler(0f, 0f, leftRotation);
            rightSpriteHolder.transform.localRotation = Quaternion.Euler(0f, 0f, rightRotation);

            var leftCollider = leftObject.AddComponent<CircleCollider>();
            var rightCollider = rightObject.AddComponent<CircleCollider>();

            leftCollider.IsTrigger = rightCollider.IsTrigger = true;

            var leftDynamicBody = leftObject.AddComponent<DynamicBody>();
            var rightDynamicBody = rightObject.AddComponent<DynamicBody>();

            var minStartVelocity = SplitSpriteEffectProviderConfig.MinStartVelocity;
            var maxStartVelocity = SplitSpriteEffectProviderConfig.MaxStartVelocity;

            var startVelocity = new Vector2(
                (float)_randomProvider.Random.NextDouble() * (maxStartVelocity.x - minStartVelocity.x) + minStartVelocity.x,
                (float)_randomProvider.Random.NextDouble() * (maxStartVelocity.y - minStartVelocity.y) + minStartVelocity.y
            );

            leftDynamicBody.Velocity = new Vector2(-startVelocity.x, startVelocity.y);
            rightDynamicBody.Velocity = startVelocity;
        }
    }
}