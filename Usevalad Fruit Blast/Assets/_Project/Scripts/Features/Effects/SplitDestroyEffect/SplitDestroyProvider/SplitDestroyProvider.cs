using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.SplitDestroyEffect.SplitDestroyProvider
{
    public class SplitDestroyProvider : BaseFeature, IConfigurableFeature<SplitDestroyProviderConfig>
    {
        public SplitDestroyProviderConfig SplitDestroyProviderConfig { get; private set; }
        
        public void Configure(SplitDestroyProviderConfig splitDestroyProviderConfig)
        {
            SplitDestroyProviderConfig = splitDestroyProviderConfig;
        }

        public void SplitDestroy(SplitDestroyableObject splitDestroyableObject)
        {
            var sprite = splitDestroyableObject.SpriteRenderer.sprite;
            var rect = sprite.textureRect;
            var texture = sprite.texture;

            var splitX = SplitDestroyProviderConfig.SplitX;
            var splitY = SplitDestroyProviderConfig.SplitY;

            var partWidth = rect.width / splitX;
            var partHeight = rect.height / splitY;

            var spritePivot = sprite.pivot / rect.size;

            for (var y = 0; y < splitY; y++)
            {
                for (var x = 0; x < splitX; x++)
                {
                    var subRect = new Rect(
                        rect.x + x * partWidth,
                        rect.y + y * partHeight,
                        partWidth,
                        partHeight
                    );

                    var subPivot = new Vector2(0.5f, 0.5f);

                    var partSprite = Sprite.Create(texture, subRect, subPivot, sprite.pixelsPerUnit);

                    var part = new GameObject("Fragment");
                    var sr = part.AddComponent<SpriteRenderer>();
                    sr.sprite = partSprite;
                    sr.sortingLayerID = splitDestroyableObject.SpriteRenderer.sortingLayerID;
                    sr.sortingOrder = splitDestroyableObject.SpriteRenderer.sortingOrder;

                    part.transform.position = splitDestroyableObject.transform.position;

                    var rb = part.AddComponent<Rigidbody2D>();
                    var dir = (new Vector2(x - (splitX - 1) / 2f, y - (splitY - 1) / 2f)).normalized;
                    rb.AddForce(dir * SplitDestroyProviderConfig.DestroyForce, ForceMode2D.Impulse);

                    part.AddComponent<BoxCollider2D>();
                }
            }
        }
    }
}