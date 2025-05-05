using System;
using _Project.Scripts.Features.FeatureCore;
using UnityEngine;

namespace _Project.Scripts.Features.Field.FieldCatcher.ColliderFieldCatcher
{
    [Serializable]
    public class ColliderFieldCatcherConfig : IFeatureConfig
    {
        [SerializeField] private FieldCatcherConfig _fieldCatcherConfig;
        [SerializeField] private GameObject _catcherObject;
        [SerializeField] private float _bordersWidth = 2f;
        
        public FieldCatcherConfig FieldCatcherConfig => _fieldCatcherConfig;
        public GameObject CatcherObject => _catcherObject;
        public float BordersWidth => _bordersWidth;
    }
}