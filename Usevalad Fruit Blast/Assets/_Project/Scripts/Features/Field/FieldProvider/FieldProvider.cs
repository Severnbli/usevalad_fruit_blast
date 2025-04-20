using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldProvider
{
    public abstract class FieldProvider : BaseFeature
    {
        public abstract Vector2 GetFieldSize();
        public abstract Vector2 GetFieldPosition();
    }
}