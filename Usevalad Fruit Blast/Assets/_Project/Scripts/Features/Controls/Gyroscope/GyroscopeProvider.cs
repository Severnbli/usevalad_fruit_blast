using _Project.Scripts.Features.FeatureCore;
using _Project.Scripts.Features.FeatureCore.FeatureContracts.GameLoop;
using UnityEngine;

namespace _Project.Scripts.Features.Controls.Gyroscope
{
    public class GyroscopeProvider : BaseFeature, IDestroyableFeature
    {
        public override void Init()
        {
            base.Init();
            
            Input.gyro.enabled = true;
        }
        
        public void OnDestroy()
        {
            Input.gyro.enabled = false;
        }

        public bool IsGyroscopeAccessible()
        {
            return Input.gyro.enabled;
        }

        public bool TryGetGravity(out Vector3 gravity)
        {
            gravity = default;

            if (!IsGyroscopeAccessible())
            {
                return false;
            }
            
            gravity = Input.gyro.gravity;
            return true;
        }

        public bool TryGetAttitude(out Quaternion attitude)
        {
            attitude = default;

            if (!IsGyroscopeAccessible())
            {
                return false;
            }
            
            attitude = Input.gyro.attitude;
            return true;
        }

        public bool TryGetRotationRate(out Vector3 rotationRate)
        {
            rotationRate = default;

            if (!IsGyroscopeAccessible())
            {
                return false;
            }
            
            rotationRate = Input.gyro.rotationRate;
            return true;
        }

        public bool TryGetUserAcceleration(out Vector3 userAcceleration)
        {
            userAcceleration = default;

            if (!IsGyroscopeAccessible())
            {
                return false;
            }
            
            userAcceleration = Input.gyro.userAcceleration;
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
            var unityAttitude = new Quaternion(quaternion.x, quaternion.y, -quaternion.z, -quaternion.w);
            
            return unityAttitude.eulerAngles;
        }

        public bool TryGetHorizontalAngle(out float angle)
        {
            angle = 0f;

            if (!TryGetEulerAngles(out var eulerAngles))
            {
                return false;
            }

            angle = GetNormalizedAngle(eulerAngles.x);
            return true;
        }

        public bool TryGetVerticalAngle(out float angle)
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
            
            angle = GetNormalizedAngle(eulerAngles.z);
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