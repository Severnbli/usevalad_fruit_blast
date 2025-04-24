using _Project.Scripts.Features.Common;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Touch.TouchProvider
{
    public abstract class TouchProvider : BaseFeature
    {
        [SerializeField] protected FieldProvider _fieldProvider;
        
        public FieldProvider FieldProvider => _fieldProvider;
        
        public void Update()
        {
            CheckTouch();
        }

        private void CheckTouch()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            var touch = Input.GetTouch(0);

            if (touch.phase != TouchPhase.Began)
            {
                return;
            }
            
            ProcessTouch(_fieldProvider.GetConvertedScreenSpacePosition(touch.position));
        }

        public abstract void ProcessTouch(Vector2 position);
        
        public override void Init(IFeatureConfig config) {}
    }
}