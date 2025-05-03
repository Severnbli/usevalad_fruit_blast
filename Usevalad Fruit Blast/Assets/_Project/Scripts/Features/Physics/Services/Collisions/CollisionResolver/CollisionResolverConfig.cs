using System;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Collisions.CollisionResolver
{
    [Serializable]
    public class CollisionResolverConfig
    {
        [SerializeField] private int _collisionResolvingIterations = 4;
        [SerializeField] private float _positionCorrectionPercent = 0.25f;
        [SerializeField] private float _positionCorrectionSlop = 0.1f;
        
        public int CollisionResolvingIterations => _collisionResolvingIterations;
        public float PositionCorrectionPercent => _positionCorrectionPercent;
        public float PositionCorrectionSlop => _positionCorrectionSlop;
    }
}