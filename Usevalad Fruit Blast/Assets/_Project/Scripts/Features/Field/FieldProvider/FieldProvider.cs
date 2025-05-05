using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider
{
    public abstract class FieldProvider : BaseFeature
    {
        public abstract Vector2 GetFieldSize();
        public abstract Vector2 GetFieldPosition();
        public abstract Vector2 GetConvertedScreenSpacePosition(Vector2 screenSpacePosition);
    }
}