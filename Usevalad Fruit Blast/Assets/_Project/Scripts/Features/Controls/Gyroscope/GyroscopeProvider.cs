using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Gyroscope
{
    public class GyroscopeProvider : BaseFeature, IDestroyableFeature
    {
        public bool IsGyroscopeAccessible { get; private set; }
        public UnityEngine.Gyroscope Gyroscope { get; private set; }
        
        public override void Init()
        {
            base.Init();
            
            SetupGyroscope();
        }
        
        public void OnDestroy()
        {
            DisableGyroscope();
        }

        private bool CheckGyroscopeAccessibility()
        {
            return SystemInfo.supportsGyroscope;
        }

        private void SetupGyroscope()
        {
            IsGyroscopeAccessible = CheckGyroscopeAccessibility();

            if (!IsGyroscopeAccessible)
            {
                return;
            }
                
            Gyroscope = Input.gyro;
            Gyroscope.enabled = true;
        }

        private void DisableGyroscope()
        {
            if (Gyroscope != null)
            {
                Gyroscope.enabled = false;
            }
        }

        public bool TryGetGravity(out Vector3 gravity)
        {
            gravity = default;

            if (!IsGyroscopeAccessible)
            {
                return false;
            }
            
            gravity = Gyroscope.gravity;
            return true;
        }

        public bool TryGetAttitude(out Quaternion attitude)
        {
            attitude = default;

            if (!IsGyroscopeAccessible)
            {
                return false;
            }
            
            attitude = Gyroscope.attitude;
            return true;
        }

        public bool TryGetRotationRate(out Vector3 rotationRate)
        {
            rotationRate = default;

            if (!IsGyroscopeAccessible)
            {
                return false;
            }
            
            rotationRate = Gyroscope.rotationRate;
            return true;
        }

        public bool TryGetUserAcceleration(out Vector3 userAcceleration)
        {
            userAcceleration = default;

            if (!IsGyroscopeAccessible)
            {
                return false;
            }
            
            userAcceleration = Gyroscope.userAcceleration;
            return true;
        }

        public bool TryGetEulerAngles(out Vector3 eulerAngles)
        {
            eulerAngles = default;

            if (!TryGetAttitude(out var attitude))
            {
                return false;
            }

            eulerAngles = GetEulerFromDeviceQuaternion(attitude);
            
            return true;
        }

        private Vector3 GetEulerFromDeviceQuaternion(Quaternion quaternion)
        {
            var unityAttitude = new Quaternion(-quaternion.x, -quaternion.y, quaternion.z, quaternion.w);
            
            return unityAttitude.eulerAngles;
        }

        public bool TryGetRollAngle(out float angle)
        {
            angle = 0f;

            if (!TryGetEulerAngles(out var eulerAngles))
            {
                return false;
            }

            angle = GetNormalizedAngle(eulerAngles.z);
            return true;
        }

        public bool TryGetPitchAngle(out float angle)
        {
            angle = 0f;

            if (!TryGetEulerAngles(out var eulerAngles))
            {
                return false;
            }
            
            angle = GetNormalizedAngle(eulerAngles.y);
            return true;
        }

        public bool TryGetYawAngle(out float angle)
        {
            angle = 0f;

            if (!TryGetEulerAngles(out var eulerAngles))
            {
                return false;
            }
            
            angle = GetNormalizedAngle(eulerAngles.x);
            return true;
        }

        public float GetNormalizedAngle(float angle)
        {
            if (angle > 180f)
            {
                angle -= 360f;
            }

            return angle;
        }
    }
}