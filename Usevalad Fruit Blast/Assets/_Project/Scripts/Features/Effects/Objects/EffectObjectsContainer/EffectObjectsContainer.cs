using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer
{
    public class EffectObjectsContainer : BaseFeature, IConfigurableFeature<EffectObjectsContainerConfig>, 
        IFixedUpdatableFeature
    {
        private readonly List<GameObject> _effectObjects = new();
        private FieldProvider _fieldProvider;    
            
        public EffectObjectsContainerConfig EffectObjectsContainerConfig { get; private set; }

        public override void Init()
        {
            base.Init();

            Context.TryGetComponentFromContainer(out _fieldProvider);
        }

        public void Configure(EffectObjectsContainerConfig effectObjectsContainerConfig)
        {
            EffectObjectsContainerConfig = effectObjectsContainerConfig;
        }
        
        public void FixedUpdate()
        {
            RemoveUselessEffects();
        }

        public Transform GetContainerTransform()
        {
            return EffectObjectsContainerConfig.EffectObjectsContainerTransform;
        }

        public void AddToContainer(GameObject effectObject)
        {
            _effectObjects.Add(effectObject);
            effectObject.transform.SetParent(GetContainerTransform());
        }

        public void RemoveUselessEffects()
        {
            var fieldPosition = _fieldProvider.GetFieldPosition();
            var fieldSize = _fieldProvider.GetFieldSize();
            
            var maxAvailableDistance = Mathf.Sqrt(fieldSize.x * fieldSize.x + fieldSize.y * fieldSize.y) 
                                       + EffectObjectsContainerConfig.DeleteFieldOffset;

            for (var i = _effectObjects.Count - 1; i >= 0; i--)
            {
                if (_effectObjects[i] == null)
                {
                    _effectObjects.RemoveAt(i);
                    continue;
                }
                
                if (Vector2.Distance(fieldPosition, _effectObjects[i].transform.position) <= maxAvailableDistance)
                {
                    return;
                }
                
                Object.Destroy(_effectObjects[i]);
                _effectObjects.RemoveAt(i);
            }
        }
    }
}