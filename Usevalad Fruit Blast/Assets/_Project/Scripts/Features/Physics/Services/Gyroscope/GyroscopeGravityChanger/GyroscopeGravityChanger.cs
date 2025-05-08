using _Project.Scripts.Features.Controls.Gyroscope;
using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using _Project.Scripts.Features.Physics.Forces.GravityForceProvider;
using UnityEngine;

namespace _Project.Scripts.Features.Physics.Services.Gyroscope.GyroscopeGravityChanger
{
    public class GyroscopeGravityChanger : BaseFeature, IConfigurableFeature<GyroscopeGravityChangerConfig>,
        IUpdatableFeature
    {
        private GyroscopeGravityChangerConfig _gyroscopeGravityChangerConfig;
        private GyroscopeProvider _gyroscopeProvider;
        private GravityForceProvider _gravityForceProvider;
        
        public override void Init()
        {
            base.Init();
            
            Context.TryGetComponentFromContainer(out _gyroscopeProvider);
            Context.TryGetComponentFromContainer(out _gravityForceProvider);
        }

        public void Configure(GyroscopeGravityChangerConfig gyroscopeGravityChangerConfig)
        {
            _gyroscopeGravityChangerConfig = gyroscopeGravityChangerConfig;
        }

        public void Update()
        {
            if (!_gyroscopeProvider.TryGetRollAngle(out var angle))
            {
                return;
            }
            
            RotateGravity(angle);
        }

        private void RotateGravity(float angle)
        {
            var angleRad = angle * Mathf.Deg2Rad;
            
            var cos = Mathf.Cos(angleRad);
            var sin = Mathf.Sin(angleRad);

            var gravityVector = _gravityForceProvider.ForceProviderConfig.Direction;

            var rotated = new Vector2(gravityVector.x * cos - gravityVector.y * sin, 
                gravityVector.x * sin + gravityVector.y * cos);
            
            _gravityForceProvider.ForceProviderConfig.Direction = rotated;
        }
    }
}