using System;
using _Project.Scripts.Features.Common;
using UnityEngine;

namespace _Project.Scripts.Features.Lifecycle.Destroyers.ClickObjectDestroyer
{
    [Serializable]
    public class ClickObjectDestroyerConfig : IFeatureConfig
    {
        [SerializeField] private float _clickOffset = 0.1f;
        [SerializeField] private float _infectionDistance = 0.5f;
        [SerializeField] private int _minInfectedObjects = 3;
        [SerializeField] private float _destroyDuration = 1f;
        [SerializeField] private AnimationCurve _destroyCurve;
        
        public float ClickOffset => _clickOffset;
        public float InfectionDistance => _infectionDistance;
        public int MinInfectedObjects => _minInfectedObjects;
        public float DestroyDuration => _destroyDuration;
        public AnimationCurve DestroyCurve => _destroyCurve;
    }
}