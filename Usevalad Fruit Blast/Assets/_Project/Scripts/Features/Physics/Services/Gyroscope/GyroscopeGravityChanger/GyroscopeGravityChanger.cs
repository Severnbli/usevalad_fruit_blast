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
            if (!_gyroscopeProvider.TryGetGravity(out var deviceGravity))
            {
                return;
            }

            RotateGameGravity(deviceGravity);
        }

        private void RotateGameGravity(Vector3 deviceGravity)
        {
            var gravityVector = new Vector2(deviceGravity.x, deviceGravity.y).normalized;
            
            var angleToDown = Vector2.SignedAngle(Vector2.down, gravityVector);

            var clampedAngle = Mathf.Clamp(angleToDown, -_gyroscopeGravityChangerConfig.MaxAngle, 
                _gyroscopeGravityChangerConfig.MaxAngle);

            var clampedAngleRad = clampedAngle * Mathf.Deg2Rad;
            var limitedGravityVector = new Vector2(Mathf.Sin(clampedAngleRad), -Mathf.Cos(clampedAngleRad)).normalized;

            _gravityForceProvider.ForceProviderConfig.Direction = limitedGravityVector;
        }
    }
}