using System.Collections.Generic;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Field.FieldProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Effects.Objects.EffectObjectsContainer
{
    public class EffectObjectsContainer : BaseFeature, IConfigurableFeature<EffectObjectsContainerConfig>, 
        IUpdatableFeature
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
        
        public void Update()
        {
            for (var i = _effectObjects.Count - 1; i >= 0; i--)
            {
                if (_effectObjects[i] == null)
                {
                    _effectObjects.RemoveAt(i);
                    continue;
                }
                
                if (_fieldProvider.IsObjectOutOfScreen(_effectObjects[i], EffectObjectsContainerConfig.DeleteFieldOffset))
                {
                    return;
                }
                
                Object.Destroy(_effectObjects[i]);
                _effectObjects.RemoveAt(i);
            }
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
    }
}